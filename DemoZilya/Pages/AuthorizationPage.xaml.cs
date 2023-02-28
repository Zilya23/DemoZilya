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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static User user { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void btnAuthoriz_Click(object sender, RoutedEventArgs e)
        {
            string log = tbLog.Text.Trim();
            string pas = pbPass.Password.Trim();

            if (log.Length != 0 && pas.Length != 0)
            {
                user = BDConnection.connection.User.Where(x => x.Login == log && x.Password == pas).FirstOrDefault();
                if (user != null)
                {
                    NavigationService.Navigate(new ListPage());
                }
                else
                    MessageBox.Show("Неверный логин или пароль");
            }
            else
                MessageBox.Show("Заполните все поля");
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            user = null;
            NavigationService.Navigate(new ListPage());
        }
    }
}
