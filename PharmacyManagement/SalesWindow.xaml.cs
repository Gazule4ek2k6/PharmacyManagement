using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PharmacyManagement
{
    /// <summary>
    /// Логика взаимодействия для SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        PharmacyManagementContext db = new PharmacyManagementContext();
        ObservableCollection<Sale> sales = new ObservableCollection<Sale>();
        public SalesWindow()
        {
            InitializeComponent();

            db.Sales
                .Include(s => s.Customer)
                .Include(s => s.Medicine)
                .Load();

            sales = db.Sales.Local.ToObservableCollection();
            SalesList.ItemsSource = sales;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as StackPanel).DataContext is Sale s)
            {
                AddSaleWindow w = new AddSaleWindow();
                w.CustomerList.ItemsSource = db.Customers.ToList();
                w.MedicineList.ItemsSource = db.Medicines.ToList();

                w.DataContext = s;

                if (w.ShowDialog() == true)
                {
                    sales.Add(s);
                    db.SaveChanges();
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Sale s = new Sale();
            AddSaleWindow w = new AddSaleWindow();
            w.CustomerList.ItemsSource = db.Customers.ToList();
            w.MedicineList.ItemsSource = db.Medicines.ToList();

            w.DataContext = s;

            if (w.ShowDialog() == true)
            {
                sales.Add(s);
                db.SaveChanges();
            }
        }
    }
}
