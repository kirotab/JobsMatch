using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public static class SearchManager
    {
        public static IEnumerable<JobViewModel> SearchForJobsInCollection(
            SearchQueryViewModel searchQuery, IEnumerable<JobViewModel> Jobs)
        {
            var result = new LinkedList<JobViewModel>();
            if (Jobs != null)//&& Jobs.Count > 0
            {
                foreach (var item in Jobs)
                {
                    item.CurrJobMatch = JobViewModel.ChechMatchings(item, searchQuery.LookupSkills, searchQuery.NotMandatoryLookupSkills);
                    if (item.CurrJobMatch.Count() >= searchQuery.LookupSkills.Count)
                    {
                        result.AddLast(item);
                    }
                }
            }
            return result;
        }
    }
}
