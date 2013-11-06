using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public class CompanyViewModel : Common.BindableBase
    {
        public string CompanyID { get; set; }

        public string CompanyName { get; set; }

        //public string CompanyURL { get; set; }

        public string CompanyLogoURL { get; set; }
    }
}
