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
            ResetAllButtonBorders();
            switch ((sender as Button).Name)
            {
                case "TabPageButton":
                    TabPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.FileMusic;
                    LandingPage landingPage = new LandingPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(landingPage);
                    break;
                case "LoginPageButton":
                    LoginPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountKey;
                    LoginPage loginPage = new LoginPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(loginPage);
                    break;
                case "FavouritesPageButton":
                    FavouritesPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.Star;
                    FavouritePage favouritePage = new FavouritePage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(favouritePage);
                    break;
                case "AccountPageButton":
                    AccountPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.Account;
                    AccountPage accountPage = new AccountPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(accountPage);
                    break;
                case "UploadPageButton":
                    UploadPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.Upload;
                    UploadPage uploadPage = new UploadPage(crudManager);
                    PageDisplayFrame.NavigationService.Navigate(uploadPage);
                    break;
            }
        }
        private void ResetAllButtonBorders()
        {
            LoginPageButtonStyle.Kind  = MaterialDesignThemes.Wpf.PackIconKind.AccountKeyOutline;
            TabPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.FileMusicOutline;
            FavouritesPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.StarOutline;
            AccountPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountOutline;
            UploadPageButtonStyle.Kind = MaterialDesignThemes.Wpf.PackIconKind.UploadOutline;
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
