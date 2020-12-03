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

namespace MusicManager_GUI.Pages
{
    /// <summary>
    /// Interaction logic for FavouritePage.xaml
    /// </summary>
    public partial class FavouritePage : Page
    {
        CRUDManager crudManager;
        public FavouritePage(CRUDManager managerOfCrud)
        {
            crudManager = managerOfCrud;
            InitializeComponent(); 
            PopulateListBox();
        }
        private void PopulateListBox()
        {
            TabList.ItemsSource = crudManager.RetrieveAllFavouriteTabs();
        }
        private void TabList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            crudManager.SetTab(TabList.SelectedItem);
            TabInformation tabInformation = new TabInformation(crudManager);
            window.PageDisplayFrame.NavigationService.Navigate(tabInformation);
        }
    }
}
