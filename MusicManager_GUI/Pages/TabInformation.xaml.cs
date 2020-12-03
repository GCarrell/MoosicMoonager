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
    /// Interaction logic for TabInformation.xaml
    /// </summary>
    public partial class TabInformation : Page
    {
        CRUDManager crudManager;
        public TabInformation(CRUDManager managerOfCrud)
        {
            

            crudManager = managerOfCrud;
            InitializeComponent();


            NameTextBox.Text = crudManager.CurrentTab.TabName;
            ArtistTextBox.Text = crudManager.CurrentTab.BandName;
            CreatorTextBox.Text = crudManager.DisplayTabCreator(); 
            DownloadLinkTextBox.Text = crudManager.CurrentTab.TabUrl;
        }

        private void AddToFavouritesButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnTextDisplay.Text = crudManager.AddTabToFavourites();
        }
    }
}
