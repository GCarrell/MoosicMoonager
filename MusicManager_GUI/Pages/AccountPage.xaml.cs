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
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        CRUDManager crudManager;
        public AccountPage(CRUDManager managerOfCrud)
        {
            crudManager = managerOfCrud;
            InitializeComponent();
        }
    }
}
