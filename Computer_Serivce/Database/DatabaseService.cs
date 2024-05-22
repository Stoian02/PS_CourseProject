using Computer_Serivce.Model;
using Computer_Serivce.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Serivce.Database
{
    public class DatabaseService
    {
        public DatabaseService()
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureDeleted();
                var exists = context.Database.CanConnect();
                if (!exists)
                {
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
            }
        }

        public List<T> GetAll<T>() where T : class
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Add<T>(T entity) where T : class
        {
            using (var context = new DatabaseContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public List<Computer> GetAllComputers()
        {
            using (var context = new DatabaseContext())
            {
                return context.Computers.Include(c => c.Repairs).ToList();
            }
        }

        public List<Repair> GetAllRepairs()
        {
            return GetAll<Repair>().ToList();
        }

        public List<Repair> GetAllRepairsWIthComputers()
        {
            using (var context = new DatabaseContext())
            {
                return context.Repairs.Include(r => r.Computer).ToList();
            }
        }

        public void AddRepair(Repair repair, Computer computer)
        {
            using (var context = new DatabaseContext())
            {
                var existingComputer = context.Computers.FirstOrDefault(c => c.SerialNumber == computer.SerialNumber);
                if (existingComputer == null)
                {
                    context.Computers.Add(computer);
                    context.SaveChanges();
                }
                else
                {
                    computer = existingComputer;
                }

                // repair.Computer = computer;
                repair.ComputerId = computer.Id;
                context.Repairs.Add(repair);
                context.SaveChanges();
            }
        }

        public List<Computer> SearchComputers(string year, string brand, string model)
        {
            using (var context = new DatabaseContext())
            {
                var query = context.Computers.AsQueryable();
                query = query.Include(c => c.Repairs);

                if (!string.IsNullOrWhiteSpace(year))
                {
                    query = query.Where(c => c.Repairs.Any(r => r.YearOfService == int.Parse(year)));
                }

                if (!string.IsNullOrWhiteSpace(brand))
                {
                    query = query.Where(c => EF.Functions.Like(c.Brand, $"%{brand}%"));
                }

                if (!string.IsNullOrWhiteSpace(model))
                {
                    query = query.Where(c => EF.Functions.Like(c.Model, $"%{model}%"));
                }

                return query.ToList();
            }
        }

        public IEnumerable<Repair> SearchRepairs(string year, string brand, string model)
        {
            using (var context = new DatabaseContext())
            {
                var query = context.Repairs.AsQueryable();
                query = query.Include(c => c.Computer);

                if (!string.IsNullOrWhiteSpace(year))
                {
                    query = query.Where(c => c.YearOfService == int.Parse(year));
                }

                if (!string.IsNullOrWhiteSpace(brand))
                {
                    query = query.Where(c => EF.Functions.Like(c.Computer.Brand, $"%{brand}%"));
                }

                if (!string.IsNullOrWhiteSpace(model))
                {
                    query = query.Where(c => EF.Functions.Like(c.Computer.Model, $"%{model}%"));
                }

                return query.ToList();
            }
        }

        public IEnumerable<T> Search<T>(
                List<Expression<Func<T, bool>>> filters, 
                Expression<Func<T, int>> yearExpression, 
                int? searchYear,
                Func<IQueryable<T>, IIncludableQueryable<T, object>>? includesExression = null
        ) where T : class
        {
            using (var context = new DatabaseContext())
            {
                var query = context.Set<T>().AsQueryable();

                // Apply includes if provided
                if (includesExression != null)
                {
                    query = includesExression(query);
                }
                // Apply filters to the query
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
                // Search for entities based on the provided year
                if (searchYear.HasValue)
                {
                    var yearQuery = query.Select(yearExpression);
                    var closestYear = yearQuery.Where(y => y <= searchYear.Value).OrderByDescending(y => y).FirstOrDefault();

                    if(closestYear == 0)
                    {
                        return Enumerable.Empty<T>();
                    }

                    var parametar = yearExpression.Parameters[0];
                    var equalsClosestYear = Expression.Equal(yearExpression.Body, Expression.Constant(closestYear, typeof(int)));
                    var lambda = Expression.Lambda<Func<T, bool>>(equalsClosestYear, parametar);
                    query = query.Where(lambda);
                }
                return query.ToList();
            }
        }
    }
}
