using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public class PublishDateViewModel : Common.BindableBase
    {
        private static PublishDateViewModel[] availablePublishDateOption = new PublishDateViewModel[] { 
                new PublishDateViewModel("All",0),
                new PublishDateViewModel("Today",2),
                new PublishDateViewModel("Yesterday",3),
                new PublishDateViewModel("Before 3 days",4),
                new PublishDateViewModel("Before 7 days",5),
                new PublishDateViewModel("Before 14 days",6)
            };

        public static IEnumerable<PublishDateViewModel> AvailablePublishDateOption
        {
            get { return availablePublishDateOption; }
            set
            {
                availablePublishDateOption = value.ToArray();
                //this.OnPropertyChanged("AvailableColors");
            }
        }

        public string PublishDateName { get; set; }

        public int PublishDateValue { get; set; }

        public PublishDateViewModel(string publishDateName, int publishDateValue)
        {
            this.PublishDateName = publishDateName;
            this.PublishDateValue = publishDateValue;
        }
    }
}
