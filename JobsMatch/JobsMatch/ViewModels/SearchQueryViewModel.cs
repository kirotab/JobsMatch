using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public class SearchQueryViewModel : Common.BindableBase
    {
        public string Keyword { get; set; }

        public PublishDateViewModel PublishDate { get; set;}

        public ICollection<SkillViewModel> LookupSkills { get; set; }

        public ICollection<SkillViewModel> NotMandatoryLookupSkills { get; set; }

        public int SkipPages { get; set; }

        public SearchQueryViewModel(
            string keyword,
            PublishDateViewModel publishDate,
            ICollection<SkillViewModel> lookupSkills,
            ICollection<SkillViewModel> notMandatoryLookupSkills,
            int skipPages = 0
            )
        {
            this.Keyword = keyword;
            this.PublishDate = publishDate;
            this.LookupSkills = lookupSkills;
            this.NotMandatoryLookupSkills = notMandatoryLookupSkills;
            this.SkipPages = SkipPages;
        }
    }
}
