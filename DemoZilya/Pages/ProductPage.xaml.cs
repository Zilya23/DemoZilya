using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoZilya.DataBase;
using Microsoft.Win32;

namespace DemoZilya.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Product Product { get; set; }
        public List<Categoriya> Categoriyas { get; set; }
        public List<Unit> Units { get; set; }  
        public List<Manufacture> Manufactures { get; set; }
        public bool isNeew { get; set; }

        public List<Saller> Sallers { get; set; }
        public ProductPage(Product product, bool isNew = true)
        {
            InitializeComponent();
            isNeew = isNew;

            Categoriyas = BDConnection.connection.Categoriya.ToList();
            cbCategoriya.ItemsSource = Categoriyas;
            cbCategoriya.DisplayMemberPath = "Name";

            Units = BDConnection.connection.Unit.ToList();
            cbUnit.ItemsSource = Units;
            cbUnit.DisplayMemberPath = "Name";

            Manufactures = BDConnection.connection.Manufacture.ToList();
            cbManufacture.ItemsSource = Manufactures;
            cbManufacture.DisplayMemberPath = "Name";

            Sallers = BDConnection.connection.Saller.ToList();
            cbSaller.ItemsSource = Sallers;
            cbSaller.DisplayMemberPath = "Name";

            if(isNew)
            {
                btnDel.Visibility = Visibility.Hidden;
                Product = new Product();
            }
            else
                Product = product;

            DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPage());
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите удалить?", "Внимание", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                BDConnection.connection.Product.Remove(Product);
                BDConnection.connection.SaveChanges();
                NavigationService.Navigate(new ListPage());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                Product.Photo = File.ReadAllBytes(openFile.FileName);
                imgProd.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(isNeew)
            {
                BDConnection.connection.Product.Add(Product);
                BDConnection.connection.SaveChanges();
                NavigationService.Navigate(new ListPage());
            }
            else
            {
                BDConnection.connection.SaveChanges();
                NavigationService.Navigate(new ListPage());
            }
        }
    }
}
