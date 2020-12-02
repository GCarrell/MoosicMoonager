using System;
using System.Collections.Generic;
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
using MusicManagerBusiness;

namespace MusicManager_GUI
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        CRUDManager _crudManager = new CRUDManager();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonUserLogIn(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonUserCreate(object sender, RoutedEventArgs e)
        {
            string creationDisplayMessage = _crudManager.AddUser(UserNameTextInput.Text as string, PasswordTextInput.Text as string);
        }
    }
}
