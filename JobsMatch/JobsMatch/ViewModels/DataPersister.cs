using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace JobsMatch.ViewModels
{
    public class DataPersister
    {
        //Helpers
        public static async Task<StorageFile> GetFileFromFileName(string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file;
            try
            {
                file = localFolder.GetFileAsync(fileName).AsTask().Result;

                //no exception means file exists
            }
            catch (Exception)
            {
                StorageFolder storageFolder = Package.Current.InstalledLocation;
                StorageFile storageFile = storageFolder.GetFileAsync(fileName).AsTask().Result;
                var x = storageFile.CopyAsync(localFolder, fileName).AsTask().Result;
                //XmlTextBlock.Text = await FileIO.ReadTextAsync(storageFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }

            StorageFile finalFile = await localFolder.GetFileAsync(fileName);

            return finalFile;
        }

        public static async Task<XElement> GetXmlDocFromFile(string fileName)
        {
            var file = await GetFileFromFileName(fileName);
            XElement rootDocumentRoot;
            using (var readStream = await file.OpenAsync(FileAccessMode.Read))
            {
                rootDocumentRoot = XDocument.Load(readStream.AsStreamForRead()).Root;
            }
            return rootDocumentRoot;

        }

        //Readers

        public static async Task<IEnumerable<JobViewModel>> GetJobs(string fileName)
        {
            var jobOffersDocumentRoot = await GetXmlDocFromFile(fileName);

            var jobsVMs =
                           from jobElement in jobOffersDocumentRoot.Elements("job")
                           select new JobViewModel()
                           {
                               JobID = jobElement.Element("jobid").Value,
                               JobTitle = jobElement.Element("jobtitle").Value,
                               JobDescription = (jobElement.Element("jobdescription").Value),
                               JobDate = (jobElement.Element("jobdate").Value),
                               JobCity = (jobElement.Element("jobcity").Value),
                               Company = new CompanyViewModel()
                               {
                                   CompanyID = jobElement.Element("company").Element("companyid").Value,
                                   CompanyName = jobElement.Element("company").Element("companyname").Value,
                                   CompanyLogoURL = jobElement.Element("company").Element("companylogourl").Value,
                               }
                           };
            return jobsVMs;
        }

        public static async Task<IEnumerable<SkillViewModel>> GetSkills(string fileName)
        {
            var skillsDocumentRoot = await GetXmlDocFromFile(fileName);

            var skillsVMs =
                           from skillElement in skillsDocumentRoot.Elements("skill")
                           select new SkillViewModel()
                           {
                               SkillName = skillElement.Element("skillname").Value,
                               SkillColorName = skillElement.Element("skillcolorname").Value,
                           };
            return skillsVMs;
        }

        //Update
        //public static async Task<IEnumerable<JobViewModel>> UpdateJobs(string fileName)
        //{
        //    var jobOffersDocumentRoot = await GetXmlDocFromFile(fileName);

        //    var jobsVMs =
        //                   from jobElement in jobOffersDocumentRoot.Elements("job")
        //                   select new JobViewModel()
        //                   {
        //                       JobID = jobElement.Element("jobid").Value,
        //                       JobTitle = jobElement.Element("jobtitle").Value,
        //                       JobDescription = (jobElement.Element("jobdescription").Value),
        //                       JobDate = (jobElement.Element("jobdate").Value),
        //                       JobCity = (jobElement.Element("jobcity").Value),
        //                       Company = new CompanyViewModel()
        //                       {
        //                           CompanyID = jobElement.Element("company").Element("companyid").Value,
        //                           CompanyName = jobElement.Element("company").Element("companyname").Value,
        //                           CompanyLogoURL = jobElement.Element("company").Element("companylogourl").Value,
        //                       }
        //                   };
        //    return jobsVMs;
        //}



        //Add / Remove
 
        internal static async void AddSkill(string fileName, SkillViewModel skill)
        {
            var file = await GetFileFromFileName(fileName);

            using (var readStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var xmldoc = XDocument.Load(readStream.AsStreamForRead());

                var root = xmldoc.Root;
                root.Add(new XElement("skill",
                   new XElement("skillname", skill.SkillName),
                   new XElement("skillcolorname", skill.SkillColorName)
                   ));
                //await readStream.FlushAsync();
                readStream.Size = 0;
                readStream.Seek(0);
                root.Document.Save(readStream.AsStreamForWrite());
                //root.Document.Save(readStream.AsStreamForWrite());
            }

        }

        internal static async void RemoveSkills(string fileName, ICollection<SkillViewModel>skillNameColl)
        {
            StorageFile file = await GetFileFromFileName(fileName);

            using (var readStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var xmldoc = XDocument.Load(readStream.AsStreamForRead());

                var root = xmldoc.Root;
                foreach (var skillName in skillNameColl)
                {
                    var skillEl = (root.Elements("skill").FirstOrDefault(x => (x.Element("skillname").Value == skillName.SkillName)));
                    skillEl.Remove();
                    
                }
                readStream.Size = 0;
                readStream.Seek(0);
                root.Document.Save(readStream.AsStreamForWrite());
                //root.Document.Save(readStream.AsStreamForWrite());
            }

        }


        internal static async void RemoveSkill(string fileName, string skillName)
        {
            StorageFile file = await GetFileFromFileName(fileName);

            using (var readStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var xmldoc = XDocument.Load(readStream.AsStreamForRead());

                var root = xmldoc.Root;
                var skillEl = (root.Elements("skill").FirstOrDefault(x => (x.Element("skillname").Value == skillName)));

                skillEl.Remove();
                readStream.Size = 0;
                readStream.Seek(0);
                root.Document.Save(readStream.AsStreamForWrite());
                //root.Document.Save(readStream.AsStreamForWrite());
            }

        }

        internal static async void AddJob(string fileName, JobViewModel job)
        {
            var file = await GetFileFromFileName(fileName);

            using (var readStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var xmldoc = XDocument.Load(readStream.AsStreamForRead());

                var root = xmldoc.Root;
                root.Add(new XElement("job",
                   new XElement("jobid", job.JobID),
                   new XElement("jobtitle", job.JobTitle),
                   new XElement("jobdescription", job.JobDescription),
                   new XElement("jobdate", job.JobDate),
                   new XElement("jobcity", job.JobCity),
                   new XElement("company",
                           new XElement("companyid", job.Company.CompanyID),
                           new XElement("companyname", job.Company.CompanyName),
                           new XElement("companylogourl", job.Company.CompanyLogoURL)
                           )));
                //await readStream.FlushAsync();
                readStream.Size = 0;
                readStream.Seek(0);
                root.Document.Save(readStream.AsStreamForWrite());
                //root.Document.Save(readStream.AsStreamForWrite());
            }

        }

        internal static async void RemoveJob(string fileName, string jobID)
        {
            StorageFile file = await GetFileFromFileName(fileName);

            using (var readStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var xmldoc = XDocument.Load(readStream.AsStreamForRead());

                var root = xmldoc.Root;
                var jobidEl = (root.Elements("job").FirstOrDefault(x => (x.Element("jobid").Value == jobID)));

                jobidEl.Remove();
                readStream.Size = 0;
                readStream.Seek(0);
                root.Document.Save(readStream.AsStreamForWrite());
                //root.Document.Save(readStream.AsStreamForWrite());
            }

        }

        //public static async Task<IEnumerable<PublishDateViewModel>> GetPublishDateOptions(string fileName)
        //{
        //    var publishDateDocumentRoot = await GetXmlDocFromFile(fileName);

        //    var publishDateVMs =
        //                   from skillElement in publishDateDocumentRoot.Elements("publishdate")
        //                   select new PublishDateViewModel()
        //                   {
        //                       PublishDateName = skillElement.Element("publishdatename").Value,
        //                       PublishDateValue = int.Parse(skillElement.Element("publishdatevalue").Value)
        //                   };
        //    return publishDateVMs;
        //}
    }


}