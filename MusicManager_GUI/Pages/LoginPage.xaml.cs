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
        CRUDManager crudManager;
        public LoginPage(CRUDManager managerOfCrud)
        {
            crudManager = managerOfCrud;
            InitializeComponent();
        }

        private void ButtonUserLogIn(object sender, RoutedEventArgs e)
        {
            var loginResult = crudManager.Login(UserNameTextInput.Text as string, PasswordTextInput.Text as string);
            if (loginResult.passOrFail == "pass")
            {
                ReturnMessageTextbox.Text += loginResult.returnMessage;

                var window = (MainWindow)Application.Current.MainWindow;
                window.FavouritesPageButton.Opacity = 1;
                window.FavouritesPageButton.IsHitTestVisible = true;
                window.UploadPageButton.Opacity = 1;
                window.UploadPageButton.IsHitTestVisible = true;
                window.AccountPageButton.Opacity = 1;
                window.AccountPageButton.IsHitTestVisible = true;
                LoginButton.Opacity = 0.5;
                LoginButton.IsHitTestVisible = false;
                CreateAccountButton.Opacity = 0.5;
                CreateAccountButton.IsHitTestVisible = false;
            }
            else
            {
                ReturnMessageTextbox.Text = loginResult.returnMessage;
            }
        }
        private void ButtonUserCreate(object sender, RoutedEventArgs e)
        {
            var creationResult = crudManager.AddUser(UserNameTextInput.Text as string, PasswordTextInput.Text as string);
            if (creationResult.passOrFail == "pass")
            {
                ReturnMessageTextbox.Text = creationResult.returnMessage + " ";
                ButtonUserLogIn(sender, e);
            }
            else
            {
                ReturnMessageTextbox.Text = creationResult.returnMessage;
            }
        }
    }
}
