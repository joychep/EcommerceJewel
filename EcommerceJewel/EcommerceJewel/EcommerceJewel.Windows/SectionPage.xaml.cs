using EcommerceJewel.Data;
using EcommerceJewel.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EcommerceJewel
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class SectionPage : Page
    {
        public static Popup settingsPopup = null;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public SectionPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
        }

        /// <summary>
        /// Gets the NavigationHelper used to aid in navigation and process lifetime management.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the DefaultViewModel. This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter is JewelryDataGroup)
            {
                var jewelryDataGroup = (JewelryDataGroup)e.NavigationParameter;
                var group = await JewelryDataSource.GetGroupAsync(jewelryDataGroup.UniqueId);
                this.DefaultViewModel["Groups"] = group;
            }
            else if (e.NavigationParameter is JewelryType)
            {
                var jewelryType = (JewelryType)e.NavigationParameter;
                var group = await JewelryDataSource.GetGroupByTypeAsync(jewelryType);
                this.DefaultViewModel["Groups"] = group;
            }
            pageTitle.DataContext = sender;
            EnableLiveTile.CreateLiveTile.ShowliveTile(false, "My Jewellery Shop");

        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(SectionPage), group);

        }

        /// <summary>
        /// Invoked when an item is clicked.
        /// </summary>
        /// <param name="sender">The GridView displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((JewelryDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);

        }

        private void btnMyCart_Click_1(object sender, RoutedEventArgs e)
        {
            settingsPopup = new Popup();
            Rect windowsBounds = Window.Current.Bounds;
            settingsPopup.Closed += settingsPopup_Closed;
            Window.Current.Activated += Current_Activated;

            settingsPopup.IsLightDismissEnabled = true;
            settingsPopup.Height = windowsBounds.Height;
            MyCartPage cartPage = new MyCartPage();

            cartPage.Height = windowsBounds.Height;

            settingsPopup.Child = cartPage;
            settingsPopup.SetValue(Canvas.LeftProperty, windowsBounds.Width - 425);
            settingsPopup.SetValue(Canvas.TopProperty, 0);

            settingsPopup.IsOpen = true;

        }

        void Current_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                settingsPopup.IsOpen = false;
            }
        }

        void settingsPopup_Closed(object sender, object e)
        {
            Window.Current.Activated -= Current_Activated;
        }
        private void btnHome_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HubPage), "AllGroups");
        }
        private void btnNecklaces_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SectionPage), JewelryType.Necklace);
        }

        private void btnRing_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SectionPage), JewelryType.Ring);
        }

        private void btnEarring_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SectionPage), JewelryType.Earring);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            navigationHelper.GoBack();
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}