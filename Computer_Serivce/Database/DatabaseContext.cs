using Computer_Serivce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Serivce.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Computer> Computers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFile = "ComputerRepairs.db";
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\.."));
            string databaseDir = Path.Combine(projectRoot, "db");

            if (!Directory.Exists(databaseDir))
            {
                Directory.CreateDirectory(databaseDir);
            }

            string databasePath = databaseDir + $"\\{databaseFile}";
            optionsBuilder.UseSqlite($"Data Source={databasePath}")
            .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repair>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Computer>().Property(e => e.Id).ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship
            modelBuilder.Entity<Repair>()
                        .HasOne(r => r.Computer)
                        .WithMany(v => v.Repairs)
                        .HasForeignKey("ComputerId");

            modelBuilder.Entity<Repair>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Computer>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Seed data for Computer
            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 1,  Brand = "Lenovo",    Model = "Yoga X1", YearMade = "2018", SerialNumber = "LE2018y" },
                new Computer { Id = 2,  Brand = "MacBook",   Model = "Pro 14", YearMade = "2019", SerialNumber = "MA2023p" },
                new Computer { Id = 3,  Brand = "Lenovo",    Model = "IdeaPad 3", YearMade = "2022", SerialNumber = "LE2022i" },
                new Computer { Id = 4,  Brand = "DELL",      Model = "XPS 13", YearMade = "2024", SerialNumber = "DE2024x" },
                new Computer { Id = 6,  Brand = "HP",        Model = "Spectre x360", YearMade = "2022", SerialNumber = "HP2022s" },
                new Computer { Id = 5,  Brand = "Acer",      Model = "Nitro 5", YearMade = "2024", SerialNumber = "AC2024n" },
                new Computer { Id = 7,  Brand = "Apple",     Model = "MacBook Air", YearMade = "2020", SerialNumber = "MA2020a" },
                new Computer { Id = 8,  Brand = "Asus",      Model = "ZenBook 14", YearMade = "2021", SerialNumber = "AS2021z" },
                new Computer { Id = 9,  Brand = "Microsoft", Model = "Surface Laptop 4", YearMade = "2023", SerialNumber = "MI2023s" },
                new Computer { Id = 10, Brand = "Acer",      Model = "Swift 3", YearMade = "2020", SerialNumber = "AC2020s" },
                new Computer { Id = 11, Brand = "HP",        Model = "Pavilion 15", YearMade = "2019", SerialNumber = "HP2019p" },
                new Computer { Id = 12, Brand = "Dell",      Model = "G5 15", YearMade = "2021", SerialNumber = "DE2021g" },
                new Computer { Id = 13, Brand = "Lenovo",    Model = "ThinkPad X1 Carbon", YearMade = "2022", SerialNumber = "LE2022c" },
                new Computer { Id = 14, Brand = "MSI",       Model = "GF63 Thin", YearMade = "2021", SerialNumber = "MS2021f" },
                new Computer { Id = 15, Brand = "Apple",     Model = "MacBook Pro 16", YearMade = "2019", SerialNumber = "MA2019p" },
                new Computer { Id = 16, Brand = "Asus",      Model = "ROG Zephyrus G14", YearMade = "2022", SerialNumber = "AS2022r" },
                new Computer { Id = 17, Brand = "Samsung",   Model = "Galaxy Book Pro", YearMade = "2021", SerialNumber = "SA2021g" },
                new Computer { Id = 18, Brand = "Microsoft", Model = "Surface Pro 7", YearMade = "2020", SerialNumber = "MI2020p" },
                new Computer { Id = 19, Brand = "Lenovo",    Model = "Legion 5", YearMade = "2022", SerialNumber = "LE2022l" },
                new Computer { Id = 20, Brand = "HP",        Model = "Elite Dragonfly", YearMade = "2023", SerialNumber = "HP2023e" },
                new Computer { Id = 21, Brand = "Dell",      Model = "Inspiron 14", YearMade = "2023", SerialNumber = "DE2023i" },
                new Computer { Id = 22, Brand = "Acer",      Model = "Aspire 5", YearMade = "2019", SerialNumber = "AC2019a" },
                new Computer { Id = 23, Brand = "Asus",      Model = "VivoBook 15", YearMade = "2020", SerialNumber = "AS2020v" },
                new Computer { Id = 24, Brand = "Apple",     Model = "MacBook Air M1", YearMade = "2021", SerialNumber = "MA2021m" },
                new Computer { Id = 25, Brand = "MSI",       Model = "Prestige 14", YearMade = "2022", SerialNumber = "MS2022p" }

            );

            // Seed data for Repair
            modelBuilder.Entity<Repair>().HasData(
                new Repair { Id = 1, ComputerId = 1, YearOfService = 2023, ServiceType = "Hardware Maintenance", Description = "Full system diagnostics and cleaning" },
                new Repair { Id = 2, ComputerId = 1, YearOfService = 2023, ServiceType = "Software Installation", Description = "Operating system installation and updates" },
                new Repair { Id = 3, ComputerId = 1, YearOfService = 2022, ServiceType = "Hardware Repair", Description = "RAM and SSD upgrade" },
                new Repair { Id = 4, ComputerId = 2, YearOfService = 2023, ServiceType = "Battery Replacement", Description = "Laptop battery replacement" },
                new Repair { Id = 5, ComputerId = 2, YearOfService = 2021, ServiceType = "System Cleanup", Description = "Virus removal and system optimization" },
                new Repair { Id = 6, ComputerId = 1, YearOfService = 2024, ServiceType = "Keyboard Replacement", Description = "Mechanical keyboard switches replacement" },
                new Repair { Id = 7, ComputerId = 3, YearOfService = 2024, ServiceType = "Screen Repair", Description = "LCD screen replacement" },
                new Repair { Id = 8, ComputerId = 6, YearOfService = 2023, ServiceType = "Screen Replacement", Description = "Full screen replacement due to damage" },
                new Repair { Id = 9, ComputerId = 7, YearOfService = 2021, ServiceType = "Battery Service", Description = "Battery capacity enhancement and replacement" },
                new Repair { Id = 10, ComputerId = 8, YearOfService = 2022, ServiceType = "Keyboard Service", Description = "Keyboard cleaning and key replacement" },
                new Repair { Id = 11, ComputerId = 9, YearOfService = 2024, ServiceType = "Hardware Upgrade", Description = "SSD and RAM upgrade" },
                new Repair { Id = 12, ComputerId = 10, YearOfService = 2020, ServiceType = "Software Troubleshooting", Description = "Operating system troubleshooting and repair" },
                new Repair { Id = 13, ComputerId = 11, YearOfService = 2023, ServiceType = "Cooling System Repair", Description = "Cooling system maintenance and fan replacement" },
                new Repair { Id = 14, ComputerId = 12, YearOfService = 2021, ServiceType = "Motherboard Repair", Description = "Motherboard replacement due to failure" },
                new Repair { Id = 15, ComputerId = 13, YearOfService = 2022, ServiceType = "Data Recovery", Description = "Hard drive failure and data recovery services" },
                new Repair { Id = 16, ComputerId = 14, YearOfService = 2023, ServiceType = "Hardware Diagnostics", Description = "Comprehensive hardware diagnostics" },
                new Repair { Id = 17, ComputerId = 15, YearOfService = 2024, ServiceType = "System Cleanup", Description = "Virus removal and system performance optimization" },
                new Repair { Id = 18, ComputerId = 16, YearOfService = 2021, ServiceType = "Battery Replacement", Description = "Battery replacement and disposal of old battery" },
                new Repair { Id = 19, ComputerId = 17, YearOfService = 2022, ServiceType = "Screen Repair", Description = "LCD screen replacement" },
                new Repair { Id = 20, ComputerId = 18, YearOfService = 2023, ServiceType = "Software Upgrade", Description = "Software upgrade and security enhancement" },
                new Repair { Id = 21, ComputerId = 19, YearOfService = 2024, ServiceType = "Component Repair", Description = "GPU and CPU component repairs" },
                new Repair { Id = 22, ComputerId = 20, YearOfService = 2020, ServiceType = "Hardware Maintenance", Description = "Regular hardware checkup and cleaning" },
                new Repair { Id = 23, ComputerId = 21, YearOfService = 2021, ServiceType = "Software Installation", Description = "New software installation and configuration" },
                new Repair { Id = 24, ComputerId = 22, YearOfService = 2022, ServiceType = "Hardware Repair", Description = "Motherboard and graphics card repair" },
                new Repair { Id = 25, ComputerId = 23, YearOfService = 2023, ServiceType = "Battery Service", Description = "Battery health check and replacement" },
                new Repair { Id = 26, ComputerId = 24, YearOfService = 2024, ServiceType = "Screen Replacement", Description = "Touchscreen replacement" },
                new Repair { Id = 27, ComputerId = 25, YearOfService = 2021, ServiceType = "System Optimization", Description = "System speed optimization and cleanup" }

            );
        }
    }
}
