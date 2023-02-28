using System;
using System.Collections.Generic;
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

namespace DemoZilya.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Product Product { get; set; }
        public List<Categoriya> Categoriyas { get; set; }
        public ProductPage(Product product, bool isNew = true)
        {
            InitializeComponent();
            Product = product;
            Categoriyas = BDConnection.connection.Categoriya.ToList();
            cbCategoriya.ItemsSource = Categoriyas;
            cbCategoriya.DisplayMemberPath = "Name";

            DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPage());
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
