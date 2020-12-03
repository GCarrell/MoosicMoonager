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
using MusicManager_GUI;

namespace MusicManager_GUI.Pages
{
    /// <summary>
    /// Interaction logic for UploadPage.xaml
    /// </summary>
    public partial class UploadPage : Page
    {
        CRUDManager crudManager;
        public UploadPage(CRUDManager managerOfCrud)
        {
            crudManager = managerOfCrud;
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var addTabResult = crudManager.AddTab(TabNameTextInput.Text, ArtistTextInput.Text, InstrumentTextInput.Text, DownloadLinkTextInput.Text, crudManager.User);
            ReturnMessageTextbox.Text = addTabResult.returnMessage;
        }
    }
}
