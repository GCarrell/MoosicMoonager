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

namespace MusicManager_GUI
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page
    {
        CRUDManager crudManager;
        public LandingPage( CRUDManager managerOfCrud)
        {
            crudManager = managerOfCrud;            
            InitializeComponent();            
            PopulateListBox();
        }
        private void CheckUserLoggedOn()
        {
            if (crudManager.User != null)
            {
                DislikeTabButton.IsHitTestVisible = true;
                LikeTabButton.IsHitTestVisible = true;
                DislikeTabButton.Opacity = 1;
                LikeTabButton.Opacity = 1;
                AddToFavouritesButton.IsHitTestVisible = true;
                AddToFavouritesButton.Opacity = 1;
            }
            else
            {
                DislikeTabButton.IsHitTestVisible = false;
                LikeTabButton.IsHitTestVisible = false;
                DislikeTabButton.Opacity = 0.5;
                LikeTabButton.Opacity = 0.5;
                AddToFavouritesButton.IsHitTestVisible = false;
                AddToFavouritesButton.Opacity = 0.5;
            }
        }
        private void PopulateListBox()
        {
            TabList.ItemsSource = crudManager.RetrieveAllTabs();
        }

        private void ListSorter(object sender, RoutedEventArgs e)
        {
            TabList.UnselectAll();
            var text = (sender as Button).Content;
            ResetAllButtonBorders();
            ResetTabInfoFields();
            switch (text)
            {
                case "Bass":
                    TabList.ItemsSource = crudManager.RetrieveBassTabs();
                    BassButton.BorderBrush = Brushes.AliceBlue;
                    break;
                case "Drums":
                    TabList.ItemsSource = crudManager.RetrieveDrumTabs();
                    DrumsButton.BorderBrush = Brushes.AliceBlue;
                    break;
                case "Guitar":
                    TabList.ItemsSource = crudManager.RetrieveGuitarTabs();
                    GuitarButton.BorderBrush = Brushes.AliceBlue;
                    break;
                case "All":
                    PopulateListBox();                   
                    ResetButton.BorderBrush = Brushes.AliceBlue;
                    break;
            }
        }
        private void ResetTabInfoFields()
        {
             
            NameTextBox.Text = "";
            ArtistTextBox.Text = "";
            CreatorTextBox.Text = "";
            DownloadLinkTextBox.Text = "";
            RatingTextBox.Text = "";
            DislikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsDownOutline;
            LikeButton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ThumbsUpOutline;
            DislikeTabButton.Opacity = 0;
            LikeTabButton.Opacity = 0;
            DislikeTabButton.IsHitTestVisible = false;
            LikeTabButton.IsHitTestVisible = false;
            DownloadButton.Opacity = 0;
            DownloadButton.IsHitTestVisible = false;
            AddToFavouritesButton.Opacity = 0;
            AddToFavouritesButton.IsHitTestVisible = false;
        }
        private void ResetAllButtonBorders()
        {
            ResetButton.BorderBrush = Brushes.DimGray;
            GuitarButton.BorderBrush = Brushes.DimGray;
            DrumsButton.BorderBrush = Brushes.DimGray;
            BassButton.BorderBrush = Brushes.DimGray;
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
            crudManager.SetTab(TabList.SelectedItem);
            ResetTabInfoFields();
            CheckUserLoggedOn();
            ReturnTextDisplay.Text = "";
            PopulateTabDetails();
        }
        private void PopulateTabDetails()
        {
            if (crudManager.CurrentTab != null)
            {
                NameTextBox.Text = crudManager.CurrentTab.TabName;
                ArtistTextBox.Text = $"{crudManager.CurrentTab.BandName}";
                CreatorTextBox.Text = $"Tab Creator: {crudManager.DisplayTabCreator()}";
                RatingTextBox.Text = crudManager.RetrieveTabRating();
                CheckUserLoggedOn();
                DownloadButton.Opacity = 1;
                DownloadButton.IsHitTestVisible = true;
            }
            
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
