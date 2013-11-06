using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public class JobViewModel : Common.BindableBase
    {
        private ObservableCollection<SkillViewModel> currJobMatch;
        public IEnumerable<SkillViewModel> CurrJobMatch
        {
            get
            {
                if (this.currJobMatch == null)
                {
                    this.currJobMatch = new ObservableCollection<SkillViewModel>();
                }

                return this.currJobMatch;
            }
            set
            {
                if (this.currJobMatch == null)
                {
                    this.currJobMatch = new ObservableCollection<SkillViewModel>();
                }
                this.SetObservableValues(this.currJobMatch, value);
            }
        }

        private void SetObservableValues<T>(ObservableCollection<T> observableCollection, IEnumerable<T> values)
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

        public string JobID { get; set; }

        //public string JobURL { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        //public  ICollection<SkillViewModel> CurrJobMatch { get; set; }

        public string JobDate { get; set; }

        public string JobCity { get; set; }

        public CompanyViewModel Company { get; set; }

        public JobViewModel()
        {
            this.CurrJobMatch = new ObservableCollection<SkillViewModel>();
        }

        public static ICollection<SkillViewModel> ChechMatchings(
            JobViewModel jobToCheck,
            ICollection<SkillViewModel> lookup,
            ICollection<SkillViewModel> notMandLookup)
        {
            var MatchingList = new List<SkillViewModel>();
            if (!String.IsNullOrEmpty(jobToCheck.JobDescription))
            {
                string textToSearch = jobToCheck.JobDescription.ToLower();
                foreach (var item in lookup)
                {
                    if (textToSearch.Contains(item.SkillName.ToLower()))
                    {
                        MatchingList.Add(item);
                    }
                }

                if (MatchingList.Count == lookup.Count)
                {
                    IEnumerable<SkillViewModel> differenceQuery =
                        notMandLookup.Except(lookup);
                    foreach (var item in differenceQuery)
                    {
                        if (textToSearch.Contains(item.SkillName.ToLower()))
                        {
                            MatchingList.Add(item);
                        }
                    }
                }
                else
                {
                    return new List<SkillViewModel>();
                }
            }

            return MatchingList;
        }


    }
}
