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
using MusicManager_GUI.Pages;
using MusicManagerBusiness;

namespace MusicManager_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CRUDManager crudManager = new CRUDManager();
        public MainWindow()
        {
            InitializeComponent();
            LandingPage landingPage = new LandingPage(crudManager);
            PageDisplayFrame.NavigationService.Navigate(landingPage);
        }


        private void ChangePage(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "TabPageButton":
                    LandingPage landingPage = new LandingPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(landingPage);
                    break;
                case "LoginPageButton":
                    LoginPage loginPage = new LoginPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(loginPage);
                    break;
                case "FavouritesPageButton":
                    FavouritePage favouritePage = new FavouritePage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(favouritePage);
                    break;
                case "AccountPageButton":
                    AccountPage accountPage = new AccountPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(accountPage);
                    break;
                case "UploadPageButton":
                    UploadPage uploadPage = new UploadPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(uploadPage);
                    break;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void WindowMinus_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
