using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PharmacyManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PharmacyManagementContext db = new PharmacyManagementContext();
        ObservableCollection<Medicine> medicines = new ObservableCollection<Medicine>();
        public MainWindow()
        {
            InitializeComponent();

            db.Medicines
                .Include(m => m.Supplier)
                .Load();

            medicines = db.Medicines.Local.ToObservableCollection();
            MedicinesList.ItemsSource = medicines;

            List<string> cat = new List<string>();
            cat.Add("Все категории");
            cat.Add("По рецепту");
            cat.Add("Без рецепта");

            ListCategory.ItemsSource = cat;
            ListCategory.SelectedIndex = 0;

            txtUser.Text = LoginUser.name;

            if (LoginUser.role == "1")
            {
                NoClient.Visibility = Visibility.Collapsed;
                this.Height = 500;
            }
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LoginUser.role == "1")
                return;

            if ((sender as StackPanel).DataContext is Medicine m)
            {
                AddMedicineWindow w = new AddMedicineWindow();

                w.SuppliersList.ItemsSource = db.Suppliers.ToList();

                w.DataContext = m;

                w.ShowDialog();

                db.SaveChanges();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MedicinesList.ItemsSource = medicines;
            }

            var list = medicines.Where(
                p => p.MedicineName.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) ||
                     p.Manufacturer.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)
                ).ToList();

            MedicinesList.ItemsSource = list;
        }

        private void Asc_Click(object sender, RoutedEventArgs e)
        {
            medicines = new ObservableCollection<Medicine>(
               medicines.OrderBy(p => p.Price)
           );

            MedicinesList.ItemsSource = medicines;
        }

        private void Desc_Click(object sender, RoutedEventArgs e)
        {
            medicines = new ObservableCollection<Medicine>(
               medicines.OrderByDescending(p => p.Price)
           );

            MedicinesList.ItemsSource = medicines;
        }

        private void ListCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFilter = ListCategory.SelectedItem.ToString();

            switch (selectedFilter)
            {
                case "Все категории":
                    MedicinesList.ItemsSource = medicines;
                    break;
                case "По рецепту":
                    MedicinesList.ItemsSource = medicines
                        .Where(m => m.PrescriptionRequired == true)
                        .ToList();
                    break;
                case "Без рецепта":
                    MedicinesList.ItemsSource = medicines
                        .Where(m => m.PrescriptionRequired == false)
                        .ToList();
                    break;
                default:
                    MedicinesList.ItemsSource = medicines;
                    break;
            }
        }

        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {
            Medicine m = new Medicine();
            AddMedicineWindow w = new AddMedicineWindow();

            w.SuppliersList.ItemsSource = db.Suppliers.ToList();

            w.DataContext = m;
            w.ShowDialog();

            if (w.DialogResult == true)
            {
                medicines.Add(m);

                db.SaveChanges();
            }
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow w = new SalesWindow();
            w.ShowDialog();
        }
    }
}