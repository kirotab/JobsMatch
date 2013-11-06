using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace JobsMatch.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class JobOpenedPage : JobsMatch.Common.LayoutAwarePage
    {
        ViewModels.JobOpenedViewModel currentViewModel = null;
        Windows.Storage.ApplicationDataContainer roamingSetting = null;

        public JobOpenedPage()
        {
            this.InitializeComponent();

            this.currentViewModel = new ViewModels.JobOpenedViewModel();

            this.DataContext = this.currentViewModel;
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
            //pageRoot.DataContext = (navigationParameter as object[])[0] as ViewModels.JobViewModel;
            //this.currentViewModel.Job = 
            var navParams = navigationParameter as object[];
            currentViewModel.Job = navParams[0] as ViewModels.JobViewModel;
            currentViewModel.Skillset = (navParams[1] as IEnumerable<ViewModels.SkillViewModel>).ToList();
            pageRoot.DataContext = currentViewModel.Job;
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

        

        private void JobOpenedPageLoaded(object sender, RoutedEventArgs e)
        {
            //var theElement = DisplayJobDescriptionElement;
            
            //string text = currentViewModel.Job.JobDescription;
            //List<ViewModels.SkillViewModel> skills = currentViewModel.Skillset;

            //Run runTemp = new Run();
            //runTemp.Text = text;

            //theElement.Inlines.Add(runTemp);


            //var separators = new List<string>();
            //foreach (var skill in skills)
            //{
            //    separators.Add(skill.SkillName.ToLower());
            //}
            //separators.Add(" ");

            //var textSplit = text.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            //var result = new StringBuilder();

            //Run runTemp = new Run();
            //foreach (var word in textSplit)
            //{
            //    //ProcessItem(item, runTemp, theElement, result);
            //    foreach (var skill in currentViewModel.Skillset)
            //    {
            //        if (skill.SkillName.ToLower() == word.ToLower())
            //        {
            //            theElement.Inlines.Add(runTemp);
            //            //runTemp.Foreground = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
            //            runTemp = new Run();
            //            runTemp.Text = result.ToString();
            //            result = new StringBuilder();
            //            //runTemp.Foreground = SolidColorBrush(Common.ColorToSolidColorBrushValueConverter.FromKnownColor(skill.SkillColorName));
            //            runTemp.Foreground = new SolidColorBrush(Common.ColorToSolidColorBrushValueConverter.FromKnownColor(skill.SkillColorName));
            //            runTemp.Text = word;
            //            theElement.Inlines.Add(runTemp);
            //            runTemp = new Run();
            //        }
            //        else
            //        {
            //            result.Append(word);

            //        }
            //    }
            //    runTemp.Text = result.ToString();
            //    theElement.Inlines.Add(runTemp);
            //}

            //Run runTemp = new Run();
            //runTemp.FontStyle = theElement.Inlines[0].FontStyle;
            //runTemp.FontWeight = theElement.Inlines[0].FontWeight;
            ////runTemp.Text = ((Run)theElement.Inlines[0]).Text.TrimStart();
            ////runTemp.TextDecorations = theElement.Inlines[0].TextDecorations;
            //runTemp.Foreground = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
            //theElement.Inlines.Add(runTemp);
        }


        //private string ProcessItem(string word, Run runTemp, TextBlock theElement, StringBuilder result)
        //{
        //    foreach (var skill in currentViewModel.Skillset)
        //    {
        //        if (skill.SkillName.ToLower() == word.ToLower())
        //        {
        //            theElement.Inlines.Add(runTemp);
        //            //runTemp.Foreground = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
        //            runTemp = new Run();
        //            runTemp.Text = result.ToString();
        //            result = new StringBuilder();
        //            //runTemp.Foreground = SolidColorBrush(Common.ColorToSolidColorBrushValueConverter.FromKnownColor(skill.SkillColorName));
        //            runTemp.Foreground = new SolidColorBrush(Common.ColorToSolidColorBrushValueConverter.FromKnownColor(skill.SkillColorName));
        //            runTemp.Text = word;
        //            theElement.Inlines.Add(runTemp);
        //            runTemp = new Run();
        //        }
        //        else
        //        {
        //            result.Append(word);
        //        }
        //    }
        //    theElement.Inlines.Add(runTemp);
        //}

    }
}
