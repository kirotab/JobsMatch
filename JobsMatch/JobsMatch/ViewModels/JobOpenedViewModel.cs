using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public class JobOpenedViewModel
    {
        private JobViewModel job;

        public JobViewModel Job
        {
            get { return job; }
            set { job = value; }
        }

        private List<SkillViewModel> skillset;

        public List<SkillViewModel> Skillset
        {
            get { return skillset; }
            set { skillset = value; }
        }

        
    }
}
