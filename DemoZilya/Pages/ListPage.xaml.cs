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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public static User user { get; set; }
        public static List<Product> Products { get; set; }
        public ListPage()
        {
            InitializeComponent();
            user = AuthorizationPage.user;
            Products = BDConnection.connection.Product.ToList();

            List<string> sort = new List<string>()
            { "Все", "По возрастанию", "По убыванию"};
            cbSort.ItemsSource = sort;

            List<string> filtr = new List<string>()
            {"Все", "0-10%", "11-14%", "15% и более" };
            cbFiltr.ItemsSource = filtr;

            if (user != null)
            {
                tbName.Text = user.Lastname + " " + user.Name + " " + user.Patronymic;
            }
            else
                tbName.Text = "Гость";

            if(user == null || user.IDRole != 3)
            {
                btnAdd.Visibility = Visibility.Hidden;
            }

            DataContext = this;
        }

        private void lvProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(user != null && lvProd.SelectedItem != null && AuthorizationPage.user.IDRole == 3)
            {
                var select = lvProd.SelectedItem as Product;
                NavigationService.Navigate(new ProductPage(select, false));
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        public void Filter()
        {
            List<Product> products = BDConnection.connection.Product.ToList();

            if (cbFiltr.SelectedIndex == 1)
                products = products.Where(x => x.Sale <= 10).ToList();
            else if (cbFiltr.SelectedIndex == 2)
                products = products.Where(x => x.Sale >= 11 && x.Sale <= 14).ToList();
            else if (cbFiltr.SelectedIndex == 3)
                products = products.Where(x => x.Sale > 14).ToList();

            if(tbSearch.Text.Trim().Length != 0)
                products = products.Where(x => x.Name.Contains(tbSearch.Text.Trim())).ToList();

            if (cbSort.SelectedIndex == 1)
                products = products.OrderBy(x => x.Price).ToList();
            else if (cbSort.SelectedIndex == 2)
                products = products.OrderByDescending(x => x.Price).ToList();

            lvProd.ItemsSource = products;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(null));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
