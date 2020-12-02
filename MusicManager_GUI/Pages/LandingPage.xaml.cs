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
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page
    {
        CRUDManager _crudManager = new CRUDManager();
        public LandingPage()
        {
            InitializeComponent();
            PopulateListBox();
        }
        private void PopulateListBox()
        {
            TabList.ItemsSource = _crudManager.RetrieveAllTabs();
        }
        private void ListSorter(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Tag)
            {
                case "Bass":
                    TabList.ItemsSource = _crudManager.RetrieveBassTabs();
                    break;
                case "Drums":
                    TabList.ItemsSource = _crudManager.RetrieveDrumTabs();
                    break;
                case "Guitar":
                    TabList.ItemsSource = _crudManager.RetrieveGuitarTabs();
                    break;
                case "Reset":
                    PopulateListBox();
                    break;
            }
        }

        private void TabList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
