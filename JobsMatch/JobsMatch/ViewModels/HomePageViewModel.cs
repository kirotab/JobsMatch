using JobsMatch.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JobsMatch.ViewModels
{
    public class HomePageViewModel : Common.BindableBase
    {
        //Consts
        const string favoriteJobsFilename = "FavoriteJobs.xml";
        const string personalSkillestFilename = "PersonalSkillset.xml";
        //Fields
        private static StringBuilder errorLog = new StringBuilder();
        private static SearchQueryViewModel newQuery;
        private static SearchQueryViewModel currentQuery;
        //private SearchQueryViewModel debugQuery;
        private SkillViewModel newSkill;
        private JobViewModel selectedJob;

        //Collection Fields
        private static ObservableCollection<JobViewModel> displayJobsCollection = new ObservableCollection<JobViewModel>();
        private ObservableCollection<JobViewModel> favoriteJobsCollection;
        private static ObservableCollection<JobViewModel> resultJobsCollection;
        private static ObservableCollection<SkillViewModel> personalSkillset;
        private static ObservableCollection<PublishDateViewModel> publishDateOptions;

        //Command Fields
        private ICommand searchQueryCommand;
        private ICommand moreResultsQueryCommand;
        private ICommand addSkillCommand;
        private ICommand removeSkillsCommand;
        private ICommand showFavoritesCommand;
        private ICommand addJobToFavoritesCommand;
        private ICommand removeJobCommand;

        //Properties

        public int ResultCount
        {
            get
            {
                if (DisplayJobsCollection != null)
                {
                    return DisplayJobsCollection.Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        public SearchQueryViewModel NewQuery
        {
            get
            {
                return newQuery;
            }
            set
            {
                if (newQuery != value)
                {
                    newQuery = value;
                    this.OnPropertyChanged("NewQuery");
                }
            }
        }

        public SearchQueryViewModel CurrentQuery
        {
            get
            {
                return currentQuery;
            }
            set
            {
                if (currentQuery != value)
                {
                    currentQuery = value;
                    this.OnPropertyChanged("CurrentQuery");
                }
            }
        }

        public SkillViewModel NewSkill
        {
            get
            {
                return this.newSkill;
            }
            set
            {
                if (this.newSkill != value)
                {
                    this.newSkill = value;
                    this.OnPropertyChanged("NewSkill");
                }
            }
        }

        public JobViewModel SelectedJob
        {
            get
            {
                return this.selectedJob;
            }
            set
            {
                if (this.selectedJob != value)
                {
                    this.selectedJob = value;
                    this.OnPropertyChanged("SelectedJob");
                }
            }
        }

        //Collection Properties

        public static IEnumerable<JobViewModel> DisplayJobsCollection
        {
            get
            {
                if (displayJobsCollection == null)
                {
                    displayJobsCollection = new ObservableCollection<JobViewModel>();
                }

                return displayJobsCollection;
            }
            set
            {
                if (displayJobsCollection == null)
                {
                    displayJobsCollection = new ObservableCollection<JobViewModel>();
                }
                SetObservableValues(displayJobsCollection, value);
            }
        }

        public IEnumerable<JobViewModel> FavoriteJobsCollection
        {
            get
            {
                if (this.favoriteJobsCollection == null)
                {
                    this.favoriteJobsCollection = new ObservableCollection<JobViewModel>();
                }

                return this.favoriteJobsCollection;
            }
            set
            {
                if (this.favoriteJobsCollection == null)
                {
                    this.favoriteJobsCollection = new ObservableCollection<JobViewModel>();
                }
                SetObservableValues(this.favoriteJobsCollection, value);
            }
        }

        public IEnumerable<JobViewModel> ResultJobsCollection
        {
            get
            {
                if (resultJobsCollection == null)
                {
                    resultJobsCollection = new ObservableCollection<JobViewModel>();
                }

                return resultJobsCollection;
            }
            set
            {
                if (resultJobsCollection == null)
                {
                    resultJobsCollection = new ObservableCollection<JobViewModel>();
                }
                SetObservableValues(resultJobsCollection, value);
            }
        }

        public static IEnumerable<SkillViewModel> PersonalSkillset
        {
            get
            {
                if (personalSkillset == null)
                {
                    personalSkillset = new ObservableCollection<SkillViewModel>();
                }

                return personalSkillset;
            }
            set
            {
                if (personalSkillset == null)
                {
                    personalSkillset = new ObservableCollection<SkillViewModel>();
                }
                SetObservableValues(personalSkillset, value);

            }
        }

        public IEnumerable<PublishDateViewModel> PublishDateOptions
        {
            get
            {
                if (publishDateOptions == null)
                {
                    publishDateOptions = new ObservableCollection<PublishDateViewModel>();
                }

                return publishDateOptions;
            }
            set
            {
                if (publishDateOptions == null)
                {
                    publishDateOptions = new ObservableCollection<PublishDateViewModel>();
                }
                SetObservableValues(publishDateOptions, value);
            }
        }

        //private void SetObservableValues<T>(ObservableCollection<T> observableCollection, IEnumerable<T> values)
        //{
        //    if (observableCollection != values)
        //    {
        //        observableCollection.Clear();
        //        foreach (var item in values)
        //        {
        //            observableCollection.Add(item);
        //        }
        //    }
        //}
        private static void SetObservableValues<T>(ObservableCollection<T> observableCollection, IEnumerable<T> values)
        {
            if (observableCollection != values)
            {
                observableCollection.Clear();
                foreach (var item in values)
                {
                    observableCollection.Add(item);
                }
            }
        }

        //Commands
        public ICommand SearchQuery
        {
            get
            {
                if (this.searchQueryCommand == null)
                {
                    this.searchQueryCommand = new DelegateCommand<SearchQueryViewModel>(this.HandleSearchQuerryCommand);
                }
                return this.searchQueryCommand;
            }
        }

        public ICommand SearchQueryMore
        {
            get
            {
                if (this.moreResultsQueryCommand == null)
                {
                    this.moreResultsQueryCommand = new DelegateCommand<SearchQueryViewModel>(this.HandleMoreResultsQueryCommand);
                }
                return this.moreResultsQueryCommand;
            }
        }

        public ICommand AddSkill
        {
            get
            {
                if (this.addSkillCommand == null)
                {
                    this.addSkillCommand = new DelegateCommand<SkillViewModel>(this.HandleAddSkillCommand);
                }
                return this.addSkillCommand;
            }
        }

        public ICommand RemoveSkills
        {
            get
            {
                if (this.removeSkillsCommand == null)
                {
                    this.removeSkillsCommand = new DelegateCommand<ICollection<SkillViewModel>>(this.HandleRemoveSkillsCommand);
                }
                return this.removeSkillsCommand;
            }
        }

        public ICommand ShowFavorites
        {
            get
            {
                if (this.showFavoritesCommand == null)
                {
                    this.showFavoritesCommand = new DelegateCommand<ICollection<JobViewModel>>(this.HandleShowFavoritesCommand);
                }
                return this.showFavoritesCommand;
            }
        }

        public ICommand AddJobToFavorites
        {
            get
            {
                if (this.addJobToFavoritesCommand == null)
                {
                    this.addJobToFavoritesCommand = new DelegateCommand<JobViewModel>(this.HandleAddJobToFavoritesCommand);
                }
                return this.addJobToFavoritesCommand;
            }
        }

        public ICommand RemoveJob
        {
            get
            {
                if (this.removeJobCommand == null)
                {
                    this.removeJobCommand = new DelegateCommand<JobViewModel>(this.HandleRemoveJobCommand);
                }
                return this.removeJobCommand;
            }
        }

        //Constructor
        public HomePageViewModel()
        {
            this.FavoriteJobsCollection = new ObservableCollection<JobViewModel>();
            //this.ResultJobsCollection = new ObservableCollection<JobViewModel>();
            //DisplayJobsCollection = new ObservableCollection<JobViewModel>();
            PersonalSkillset = new ObservableCollection<SkillViewModel>();
            this.PublishDateOptions = new ObservableCollection<PublishDateViewModel>();

            this.LoadFavoriteJobs(favoriteJobsFilename);
            this.LoadPersonalSkillset(personalSkillestFilename);
            this.LoadPublishDateOption(); //"PublishDateSearchOptions.xml"

            //if (newQuery == null)
            //{
                NewQuery = new SearchQueryViewModel(
                    string.Empty,
                    this.PublishDateOptions.First(),
                    new ObservableCollection<SkillViewModel>(),
                    personalSkillset
                    );
            //}
            
           
            this.NewSkill = new SkillViewModel();
            this.SelectedJob = null;
            
        }

        //Loaders
        private void LoadPublishDateOption()
        {
            //this.PublishDateOptions = await DataPersister.GetPublishDateOptions(fileName);
            this.PublishDateOptions = PublishDateViewModel.AvailablePublishDateOption;

        }

        private async void LoadFavoriteJobs(string fileName)
        {
            this.FavoriteJobsCollection = await DataPersister.GetJobs(fileName);
        }

        private async void LoadPersonalSkillset(string fileName)
        {
            PersonalSkillset = await DataPersister.GetSkills(fileName);
        }

        
        //Command Handlers
        internal async void HandleSearchQuerryCommand(SearchQueryViewModel parameter)
        {
            //this.debugQuery = (parameter);
            this.CurrentQuery = new SearchQueryViewModel(
                parameter.Keyword,
                parameter.PublishDate,
                parameter.LookupSkills,
                parameter.NotMandatoryLookupSkills
                );

            parameter.SkipPages = -50000;

            try
            {
               var results = await RemoteDataManager.SearchForJobs(parameter);
               this.ResultJobsCollection = results;

            }
            catch (Exception e)
            {
                errorLog.AppendLine(e.ToString());
            }

            DisplayJobsCollection = SearchManager.SearchForJobsInCollection(NewQuery, this.ResultJobsCollection);
            parameter.SkipPages = 0;

            //foreach (var item in this.ResultJobsCollection)
            //{
                //item.CurrJobMatch = item.ChechMatchings(parameter.LookupSkills, parameter.NotMandatoryLookupSkills);
            //}
            this.OnPropertyChanged("ResultCount");
        }

        internal async void HandleMoreResultsQueryCommand(SearchQueryViewModel parameter)
        {
            //this.debugQuery = (parameter);
            if (parameter != null)
            {
                //DisplayJobsCollection = this.ResultJobsCollection;
                //for (int i = 0; i < 5; i++)
                //{
                    parameter.SkipPages = 20;
                    try
                    {
                        var newResults = await RemoteDataManager.SearchForJobs(parameter);
                        foreach (var item in newResults)
                        {
                            resultJobsCollection.Add(item);
                        }
                    }
                    catch (Exception e)
                    {
                        //dont break the app :)
                        errorLog.AppendLine(e.ToString());
                    }
                   
                //}
                DisplayJobsCollection = 
                    SearchManager.SearchForJobsInCollection(CurrentQuery, this.ResultJobsCollection);
                this.OnPropertyChanged("ResultCount");
                //foreach (var item in this.ResultJobsCollection)
                //{
                //item.CurrJobMatch = item.ChechMatchings(parameter.LookupSkills, parameter.NotMandatoryLookupSkills);
                //}
            }
        }

        internal void HandleAddSkillCommand(SkillViewModel parameter)
        {
            if (parameter != null &&
                !String.IsNullOrEmpty(parameter.SkillName) &&
                !personalSkillset.Any(x => (x.SkillName == parameter.SkillName)))
            {
                personalSkillset.Add(parameter);
                this.NewSkill = new SkillViewModel();
                DataPersister.AddSkill(personalSkillestFilename, parameter);
            }
        }

        internal void HandleRemoveSkillsCommand(ICollection<SkillViewModel> parameter)
        { 
            if (parameter != null && parameter.Count > 0)
            {
                var listToDelete = new List<SkillViewModel>();
                foreach (var item in parameter)
                {
                    if (personalSkillset.Any(x => (x.SkillName == item.SkillName)))
                    {
                        listToDelete.Add(item);
                    }
                }
                if (listToDelete.Count > 0)
                {
                    
                    foreach (var item in listToDelete)
                    {
                        personalSkillset.Remove(item);
                        //DataPersister.RemoveSkill(personalSkillestFilename,item.SkillName);
                    }
                    DataPersister.RemoveSkills(personalSkillestFilename,listToDelete);

                }
            }
        }
        
        internal void HandleShowFavoritesCommand(ICollection<JobViewModel> parameter)
        {
            var favJobs = parameter;
            var param = SearchManager.SearchForJobsInCollection(NewQuery, favJobs);
            DisplayJobsCollection = param;
            this.OnPropertyChanged("ResultCount");
        }

        internal void HandleAddJobToFavoritesCommand(JobViewModel parameter)
        {
            //if (parameter != null && !this.favoriteJobsCollection.Contains(parameter,EqualityComparer<JobViewModel>.Default))
            if (parameter != null && !this.favoriteJobsCollection.Any(x=>(x.JobID == parameter.JobID)))
            {
                //if(this.favoriteJobsCollection.Any(x=>(x.JobID == parameter.JobID));
                this.favoriteJobsCollection.Add(parameter);
                DataPersister.AddJob(favoriteJobsFilename, parameter);
            }
        }

        internal void HandleRemoveJobCommand(JobViewModel parameter)
        {
            //if (parameter != null && this.favoriteJobsCollection.Contains(parameter))

            if (parameter != null && this.favoriteJobsCollection.Any(x => (x.JobID == parameter.JobID)))
            {
                this.favoriteJobsCollection.Remove(parameter);
                DataPersister.RemoveJob(favoriteJobsFilename, parameter.JobID);
                DisplayJobsCollection = this.favoriteJobsCollection;
                //this.OnPropertyChanged("FavoriteJobsCollection");
                //this.OnPropertyChanged("ResultJobsCollection");
                //this.OnPropertyChanged("DisplayJobsCollection");
            }
        }

    }
}
