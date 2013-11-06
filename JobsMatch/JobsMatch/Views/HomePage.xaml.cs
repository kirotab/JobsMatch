using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace JobsMatch.Views
{   

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class HomePage : JobsMatch.Common.LayoutAwarePage
    {
        static bool isPrivacySet = false;

        ViewModels.HomePageViewModel currentViewModel = null;
        Windows.Storage.ApplicationDataContainer roamingSetting = null;

        public HomePage()
        {
            this.InitializeComponent();

            this.currentViewModel = new ViewModels.HomePageViewModel();
            
            this.DataContext = this.currentViewModel;
            this.roamingSetting = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (!isPrivacySet)
            {
                isPrivacySet = true;
                SettingsPane.GetForCurrentView().CommandsRequested += ShowPrivacyPolicy;
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //var queryText = navigationParameter as String;
            //this.currentViewModel.CurrentQuery.Keyword = navigationParameter as ViewModels.SearchQueryViewModel;
            //this.currentViewModel.NewQuery = navigationParameter as ViewModels.SearchQueryViewModel;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {

        }

        private void NavigateToJobEvent(object sender, ItemClickEventArgs e)
        {
            //new Tuple<ItemClickEventArgs,IEnumerable<ViewModels.SkillViewModel>>(e.ClickedItem, ViewModels.HomePageViewModel.PersonalSkillset)
            this.Frame.Navigate(typeof(JobOpenedPage), new object[] {e.ClickedItem,ViewModels.HomePageViewModel.PersonalSkillset } );
        }

        private void HomePageLoaded(object sender, RoutedEventArgs e)
        {
            if (roamingSetting.Values.ContainsKey("searchKeyword"))
            {
                currentViewModel.NewQuery.Keyword = (string)roamingSetting.Values["searchKeyword"];
            }
            
        }


        //private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        //{
        //    var privacy = new SettingsCommand("PrivacyPolicy", "PrivacyPolicy", (handler) =>
        //    {
        //        var settings = new SettingsPane();

        //        settings.Content = new PrivacyPolicyControl();
        //        //settings.HeaderBrush = new SolidColorBrush(_background);
        //        //settings.Background = new SolidColorBrush(_background);
        //        settings.HeaderBrush = _Hbackground;
        //        settings.Background = _background;
        //        settings.HeaderText = "Privacy Policy";
        //        settings.IsOpen = true;
        //    });

        //    args.Request.ApplicationCommands.Add(privacy);

        //    UICommandInvokedHandler handler1 = new UICommandInvokedHandler(onSettingsCommand);

        //    //  throw new NotImplementedException();
        //}

        //void onSettingsCommand(IUICommand command)
        //{
        //    SettingsCommand settingsCommand = (SettingsCommand)command;
        //    ((Frame)Window.Current.Content).Navigate(typeof(HomePage), "");
        //}
        


// Method to add the privacy policy to the settings charm
private void ShowPrivacyPolicy(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
{
    SettingsCommand privacyPolicyCommand = new SettingsCommand("privacyPolicy", "Privacy Policy", (uiCommand) => { ShowPrivacyPolicy(); });
    args.Request.ApplicationCommands.Add(privacyPolicyCommand);
}

 //Method to launch the url of the privacy policy
void ShowPrivacyPolicy()
{
    this.Frame.Navigate(typeof(PrivacyPolicy));
}

//static async void LaunchPrivacyPolicyUrl()
//{
//    Uri privacyPolicyUrl = new Uri("http://www.yoursite.com/privacypolicy");
//    var result = await Windows.System.Launcher.LaunchUriAsync(privacyPolicyUrl);
//}
    }
}
