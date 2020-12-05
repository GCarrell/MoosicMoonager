using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            PopulateListBox();
        }
        private void PopulateListBox()
        {
            UserTabList.ItemsSource = crudManager.RetrieveUserTabs();

        }
       
        private void DeleteTabButton_Click(object sender, RoutedEventArgs e)
        {
            crudManager.DeleteTab();
            UserTabList.UnselectAll();
            ResetTabInfoFields();
            PopulateListBox();
        }

        private void TabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetTabInfoFields();
            LikeTabButton.Opacity = 1;
            LikeTabButton.IsHitTestVisible = true;
            DislikeTabButton.Opacity = 1;
            DislikeTabButton.IsHitTestVisible = true;
            DeleteTabButton.IsHitTestVisible = true;
            DeleteTabButton.Opacity = 1;
            DownloadButton.IsHitTestVisible = true;
            DownloadButton.Opacity = 1;
            crudManager.SetTab(UserTabList.SelectedItem);
            PopulateTabDetails();

        }
        private void RateTabButton_Click(object sender, RoutedEventArgs e)
        {
            int rating = int.Parse((sender as Button).Tag.ToString());
            var returnSuccess = crudManager.LikeDislikeTab(rating);
            switch (returnSuccess)
            {
                case "liked":
                    LikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsUp;
                    DislikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsDownOutline;
                    break;
                case "disliked":
                    LikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsUpOutline;
                    DislikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsDown;
                    break;
                case "failed":
                    break;
            }
        }

        private void PopulateTabDetails()
        {
            if (crudManager.CurrentTab != null)
            {
                NameTextBox.Text = crudManager.CurrentTab.TabName;
                ArtistTextBox.Text = $"By {crudManager.CurrentTab.BandName}";
                CreatorTextBox.Text = $"You are this tabs creator";
                RatingTextBox.Text = crudManager.RetrieveTabRating();
            }
        }
        private void ResetTabInfoFields()
        {

            NameTextBox.Text = "";
            ArtistTextBox.Text = "";
            CreatorTextBox.Text = "";
            RatingTextBox.Text = "";
            DislikeTabButton.Opacity = 0;
            DislikeTabButton.IsHitTestVisible = false;
            DislikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsDownOutline;
            LikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsUpOutline;
            LikeTabButton.Opacity = 0;
            LikeTabButton.IsHitTestVisible = false;
            DeleteTabButton.IsHitTestVisible = false;
            DeleteTabButton.Opacity = 0;
            DownloadButton.IsHitTestVisible = false;
            DownloadButton.Opacity = 0;
        }


        private void DownloadTab_Click(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = crudManager.CurrentTab.TabUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
