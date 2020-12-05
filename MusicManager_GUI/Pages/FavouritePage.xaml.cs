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
            UserFavouriteList.ItemsSource = crudManager.RetrieveAllFavouriteTabs();
        }
        private void AddToFavouritesButton_Click(object sender, RoutedEventArgs e)
        {
            var returnMessage = crudManager.AddTabToFavourites();
            ReturnTextDisplay.Text = returnMessage.Item1;
            if (returnMessage.Item2 == "pass")
            {
                ReturnTextDisplay.Foreground = Brushes.Green;
            }
            else
            {
                ReturnTextDisplay.Foreground = Brushes.Red;
            }
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


        private void TabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetTabInfoFields();
            DownloadButton.IsHitTestVisible = true;
            RemoveFromFavouritesButton.IsHitTestVisible = true;
            LikeTabButton.Opacity = 1;
            DislikeTabButton.Opacity = 1;
            DownloadButton.Opacity = 1;
            RemoveFromFavouritesButton.Opacity = 1;
            DislikeTabButton.IsHitTestVisible = true;
            LikeTabButton.IsHitTestVisible = true;
            crudManager.SetTab(UserFavouriteList.SelectedItem);
            PopulateTabDetails();
        }
        private void PopulateTabDetails()
        {
            if (crudManager.CurrentTab != null)
            {
                NameTextBox.Text = crudManager.CurrentTab.TabName;
                ArtistTextBox.Text = $"By {crudManager.CurrentTab.BandName}";
                CreatorTextBox.Text = $"Tab Creator: {crudManager.DisplayTabCreator()}";
                RatingTextBox.Text = crudManager.RetrieveTabRating();
            }
        }
        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = crudManager.CurrentTab.TabUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        private void RemoveFromFavouritesButton_Click(object sender, RoutedEventArgs e)
        {
            crudManager.DeleteFavouriteTab();
            UserFavouriteList.UnselectAll();
            ResetTabInfoFields();
            PopulateListBox();
        }

        private void ResetTabInfoFields()
        {

            NameTextBox.Text = "";
            ArtistTextBox.Text = "";
            CreatorTextBox.Text = "";
            RatingTextBox.Text = "";
            DislikeTabButton.Opacity = 0;
            LikeTabButton.Opacity = 0;
            DislikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsDownOutline;
            LikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsUpOutline;
            DownloadButton.Opacity = 0;
            DownloadButton.IsHitTestVisible = false;
            RemoveFromFavouritesButton.Opacity = 0;
            RemoveFromFavouritesButton.IsHitTestVisible = false;
        }

    }
}
