   M S B A M L   `   `   `  ����K  GComputer_Serivce, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Computer_Serivce.View.ControlsComputer_Serivce.View.Controls  5      G(clr-namespace:Computer_Serivce.ViewModelComputer_Serivce.ViewModel  5      2    ,Computer_Serivce.View.Controls.SearchControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:Computer_Serivce.View.Controls     5      7vm(clr-namespace:Computer_Serivce.ViewModel     5        � DataContext  5
      0   *Computer_Serivce.ViewModel.SearchViewModel  5   
   .��5      �    Content#��[  6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  
 Header#��[ 6   $��Computer Service��64   $��0��6L   $��Center=�6Y   $��Center=�6t   �� 5   
     NameRow#��[ 6   $��1��66    �� Orientation$ 
Horizontal=�6C   .+�5      �� $�� Year ��6   $��Center=�6%   �� 5      $�� Brand��6   $��Center=�6%   �� 5      $�� Model��6   $��Center=�6%   5      �� 5   
     InputRow#��[ 6   $��2��67   $ 
Horizontal=�6D   $��10q�6]   $��Center=�6i   .+�5       �� -   5        ��5    A   �� *RepairedYear+ �� Mode$ TwoWay=��� 5!      ��6   �� *Brand+�� 5"      ��6   �� *Model+5#      �� 5%   
     
ButtonsRow#��[ 6   $��3��69   $ 
Horizontal=�6F   .+�5&      ��   ButtonStyle#��[ 5'      $��Search��5(      $��5q�5*      ��5)      �� *SearchRepairsCommand+5*      �� 5+      #��[ 5,      $��Clear��5-      $��5q�5/      ��5.      �� *ClearCommand+5/      50      �� 52   
   #��[ 6   $��Results��64   $��4��6C   $��Center=�6P   $��Center=�6k   &   System.Windows.Controls.DataGrid  54   
     Datagrid#��[ 55        DatagridHeader   ColumnHeaderStyle# [ 56        DataGridCellStyle   	CellStyle# [ 57      $��5��58         AutoGenerateColumns$ False��6   $��30,10,30,100q�5:         
IsReadOnly$
 True��5;      ��59      �� *	Repairs+   Columns 5=      0  *System.Windows.Controls.DataGridTextColumn  5>      ,  &System.Windows.Controls.DataGridColumn	   Header$	 RepairID��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   �� *Id+  5?      $	 Year of service��6%   
 6>   �� *YearOfService+  5@      $	 Repair Descripion��6%   
 6@   �� *Description+  5A      $	 
ComputerID��6%   
 69   �� *Computer.Id+  5B      $	 Brand��6%   
 64   �� *Computer.Brand+  5C      $	 Model��6%   
 64   �� *Computer.Model+  5D      $	 Serial Number��6%   
 6<   �� *Computer.SerialNumber+5F      5G      5H      