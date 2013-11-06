using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsMatch.ViewModels
{
    public class SkillViewModel : Common.BindableBase
    {
        //private ObservableCollection<string> availableColors;

        //public IEnumerable<string> AvailableColors
        //{
        //    get
        //    {
        //        if (this.availableColors == null)
        //        {
        //            this.availableColors = new ObservableCollection<string>();
        //        }

        //        return this.availableColors;
        //    }
        //    set
        //    {
        //        if (this.availableColors == null)
        //        {
        //            this.availableColors = new ObservableCollection<string>();
        //        }
        //        this.SetObservableValues(this.availableColors, value);
        //    }
        //}

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


        private string[] availableColors = new string[] { 
                "Crimson",
                "DarkViolet",
                "DarkRed",
                "DarkOrange",
                "DeepSkyBlue",
                "LightBlue",
                "Green",
                "Yellow",
                "Pink" 
            };

        public IEnumerable<string> AvailableColors
        {
            get { return availableColors; }
            set
            {
                availableColors = value.ToArray();
                //this.OnPropertyChanged("AvailableColors");
            }
        }

        private string skillColorName;

        public string SkillColorName
        {
            get { return skillColorName; }
            set 
            {
                skillColorName = value;

                OnPropertyChanged("SkillColorName");
            }
        }


        public string SkillName { get; set; }

        //public string SkillColorName { get; set; }

        public SkillViewModel()
            : this(string.Empty, "Crimson")
        {
        }


        public SkillViewModel(string skillName, string skillColorName)
        {
            this.SkillName = skillName;
            this.SkillColorName = skillColorName;
            //this.AvailableColors = new ObservableCollection<string>();
            //this.AvailableColors = new ObservableCollection<String> { 
            //    "Crimson",
            //    "DarkViolet",
            //    "DarkRed",
            //    "DarkOrange",
            //    "DeepSkyBlue",
            //    "LightBlue",
            //    "Green",
            //    "Yellow",
            //    "Pink" 
            //};
        }
    }
}
