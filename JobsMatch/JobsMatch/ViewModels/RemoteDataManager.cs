using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using Windows.Data.Xml.Dom;

namespace JobsMatch.ViewModels
{
   

    public class RemoteDataManager
    {
        public static int lastSkipResults = 0;
        //private static SearchQueryViewModel lastSearchQuery = null;
        //public static List<JobViewModel> lastResult = null;
        
        //last=0&str_regions=&str_locations=&tab=jobs&old_country=&country=-1&region=0&l_category%5B%5D=0&keyword=
        public static async Task<ICollection<JobViewModel>> SearchForJobs(SearchQueryViewModel searchQuery)//ICollection<JobViewModel> prevResults = null 
        {
            //if (lastSearchQuery == null || !(lastSearchQuery.Equals(searchQuery)))//|| (lastResult == null || lastResult.Count() == 0)
            //{
            //if (lastSearchQuery != null)
            //{
            //    var debug = (lastSearchQuery== searchQuery);
            //}
            List<JobViewModel> testResult = new List<JobViewModel>();


            var skipResults = searchQuery.SkipPages;
            var keyword = searchQuery.Keyword;
            var dateFilter = searchQuery.PublishDate;
            var lookupSkills = searchQuery.LookupSkills;
            var notMandatoryLookupSkills = searchQuery.NotMandatoryLookupSkills;
            var baseAddress = "http://www.jobs.bg/front_job_search.php?";


            //lastSearchQuery = searchQuery;
            //if (lastSearchQuery == null)
            //{
            //    lastSearchQuery = new SearchQueryViewModel(
            //        keyword,
            //        dateFilter,
            //        lookupSkills,
            //        notMandatoryLookupSkills,
            //        skipResults
            //        );
            //}
            //else
            //{
            //    lastSearchQuery.Keyword = keyword;
            //    lastSearchQuery.PublishDate = dateFilter;
            //    lastSearchQuery.LookupSkills = lookupSkills;
            //    lastSearchQuery.NotMandatoryLookupSkills = notMandatoryLookupSkills;
            //    lastSearchQuery.SkipPages = skipResults;
            //}

            lastSkipResults += skipResults;
            if (lastSkipResults < 0)
            {
                lastSkipResults = 0;
            }


            var fullUri = String.Format("{0}frompage={3}&last={1}&keyword={2}", baseAddress, dateFilter.PublishDateValue, keyword, lastSkipResults);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(fullUri);
            var response = await client.GetAsync("");

            //var responseTextAsStream = await response.Content.ReadAsStreamAsync();
            var responseText = await response.Content.ReadAsStringAsync();


            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = false;

            // filePath is a path to a file containing the html
            //htmlDoc.Load(responseTextAsStream);
            htmlDoc.LoadHtml(responseText);

            // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)

            // ParseErrors is an ArrayList containing any errors from the Load statement
            //if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            //{
            //    // Handle any parse errors as required

            //}
            //else
            if (htmlDoc.DocumentNode != null)
            {
                //List<HtmlAgilityPack.HtmlNode> bodyNode = htmlDoc.DocumentNode.Descendants("table").Where(x=>((x.Descendants("table").Count()) > 2)).ToList();// ("//body");


                if (htmlDoc.DocumentNode.Descendants("td") != null)
                {
                    var tableNode = (htmlDoc.DocumentNode.Descendants("td").Where(
                        d => (d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("listTitle"))
                        ).FirstOrDefault());
                    if (tableNode != null && tableNode.ParentNode != null && tableNode.ParentNode.ParentNode != null)
                    {
                        tableNode = tableNode.ParentNode.ParentNode;
                    }
                    if (tableNode != null)
                    {
                        var jobOfferRows = tableNode.Descendants("tr");

                        foreach (var row in jobOfferRows)
                        {

                            var cells = row.Elements("td").Where(d => (d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("offerslistRow"))).ToList();
                            if (cells.Count() == 5)
                            {
                                JobViewModel newOffer = null;
                                try
                                {
                                    newOffer = await CreateJobOfferFromRow(cells);
                                }
                                catch (Exception e)
                                {
                                    throw new InvalidOperationException("Parsing Job Offer Failed", e);
                                }

                                //item.CurrJobMatch = item.ChechMatchings(parameter.LookupSkills, parameter.NotMandatoryLookupSkills);
                                //if (newOffer != null)
                                //{
                                //    newOffer.CurrJobMatch = JobViewModel.ChechMatchings(newOffer, lookupSkills, notMandatoryLookupSkills);
                                //    if (newOffer.CurrJobMatch.Count() >= lookupSkills.Count)
                                //    {
                                //        testResult.Add(newOffer);
                                //    }
                                //}
                                if (newOffer != null)
                                {
                                    testResult.Add(newOffer);

                                }
                            }
                        }
                    }
                }
            }

            //lastResult = testResult;
            return testResult;
            //}
            //else
            //{
            //    return lastResult;
            //}
        }

        private static async Task<JobViewModel> CreateJobOfferFromRow(List<HtmlNode> cells)
        {

            try
            {

            
            //var tempRest = cells[0].InnerHtml;
            //var tempRest1 = cells[1].InnerHtml;
            //var tempRest2 = cells[2].InnerHtml;
            //var tempRest3 = cells[3].InnerHtml;
            //var tempRest4 = cells[4].InnerHtml;

            var date = cells[0].InnerText;
            if(date == "днес")
            {
                date = (DateTime.Today).Date.ToString("dd.MM.yy");
            }
            else if (date == "вчера")
            {
                date = (DateTime.Today.AddDays(-1)).Date.ToString("dd.MM.yy");
            }
            var jobTitle = PurifyString(cells[2].Element("a").InnerText);
            var city = PurifyString(cells[2].LastChild.InnerText);
            var jobID = cells[2].Element("a").Attributes["href"].Value;

            string companyLogo;
            if (cells[3].Element("a") != null)
            {
                if (cells[3].Element("a").Element("img") != null)
                {
                    if (cells[3].Element("a").Element("img").Attributes["src"].Value != null)
                    {
                        companyLogo = cells[3].Element("a").Element("img").Attributes["src"].Value;
                    }
                    else
                        companyLogo = "anonymousLogo";
                }
                else
                    companyLogo = "anonymousLogo";
            }
            else
                companyLogo = "anonymousLogo";

            string companyID;
            string companyName;
            if (cells[4].Descendants("a") != null)
            {
                var tmpElement = cells[4].Descendants("a").FirstOrDefault();
                if (tmpElement != null)
                {
                    companyID = tmpElement.Attributes["href"].Value;
                    companyName = PurifyString(tmpElement.InnerText);
                }
                companyID = "anonymousID";
                companyName = PurifyString(cells[4].InnerText);
            }
            else
            {
                companyID = "anonymousID";
                companyName = PurifyString(cells[4].InnerText);
            }

            JobViewModel newOffer = new JobViewModel();
            newOffer.JobID = jobID;
            newOffer.JobTitle = jobTitle;
            newOffer.JobCity = city;
            newOffer.JobDate = date;
            newOffer.JobDescription = await GetJobDescription(jobID);
           

            newOffer.Company = new CompanyViewModel();
            newOffer.Company.CompanyID = companyID;
            newOffer.Company.CompanyName = companyName;
            newOffer.Company.CompanyLogoURL = companyLogo;

            return newOffer;
            }
            catch (Exception e)
            {

                throw new InvalidOperationException("Error while creating JobModel from Row",e);
            }
        }



        public static string PurifyString(string str)
        {
            var tmp = System.Net.WebUtility.HtmlDecode(str);
            tmp = tmp.Replace("\n", String.Empty);
            tmp = tmp.Replace("\r", String.Empty);
            tmp = tmp.Replace("\t", String.Empty);
            tmp = tmp.Trim();
            return tmp;
        }

        public static string PurifyStringParameters(string str,  params string[] par)
        {
            var tmp = System.Net.WebUtility.HtmlDecode(str);
            foreach (var item in par)
            {
                tmp = tmp.Replace(item, string.Empty);
            }
            tmp = tmp.Trim();
            return tmp;
        }

        public static async Task<string> GetJobDescription(string jobID)
        {

            string result = string.Empty;
            var baseAddress = "http://www.jobs.bg/";

            var fullUri = baseAddress + jobID;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(fullUri);
            var response = await client.GetAsync("");

            //var responseTextAsStream = await response.Content.ReadAsStreamAsync();
            var responseText = await response.Content.ReadAsStringAsync();

            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = false;

            // filePath is a path to a file containing the html
            //htmlDoc.Load(responseTextAsStream);
            htmlDoc.LoadHtml(responseText);

            // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)

            // ParseErrors is an ArrayList containing any errors from the Load statement
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required

            }
            //else
            try
            {


                if (htmlDoc.DocumentNode != null)
                {
                    //List<HtmlAgilityPack.HtmlNode> bodyNode = htmlDoc.DocumentNode.Descendants("table").Where(x=>((x.Descendants("table").Count()) > 2)).ToList();// ("//body");



                    //var jobDataView = (htmlDoc.DocumentNode.Descendants("td").Where(
                    //    d => (d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("jobDataView"))));
                    //if (jobDataView != null)
                    //{
                    //    foreach (var item in jobDataView)
                    //    {
                    //        result += item.InnerText;
                    //    }

                    //}
                    var bodyelement = htmlDoc.DocumentNode.Element("html").Element("body");
                    var tableElements = bodyelement.Elements("table");


                    if (tableElements != null && tableElements.Count() >= 2)
                    {
                        var tableElement = tableElements.Skip(1).FirstOrDefault();
                        if (tableElement != null)
                        {
                            var innerTableRow =
                                tableElement.Elements("tr").Skip(1).FirstOrDefault();
                            if (innerTableRow != null)
                            {
                                var innerTableElement =
                                    innerTableRow.Element("td").Elements("table").Skip(1).FirstOrDefault();
                                //var innerTableElement = tableElement.Descendants("table").Skip(1).FirstOrDefault();
                                if (innerTableElement != null)
                                {
                                    var JobOfferElement = innerTableElement.Element("tr").Element("td").Element("table");//.Elements("tr").Skip(1).FirstOrDefault().Element("td").Element("table");
                                    if (JobOfferElement != null)
                                    {
                                        try
                                        {
                                            JobOfferElement = ClearNodes(JobOfferElement);

                                        }
                                        catch (Exception e)
                                        {

                                            throw new InvalidOperationException("Problem in clearing nodes in job offer", e);
                                        }

                                        //result = JobOfferElement.InnerText;
                                        //result = JobOfferElement.InnerHtml;
                                        result = PurifyStringParameters(JobOfferElement.InnerText, new string[] { "\t", "\r" });
                                    }
                                }
                            }

                        }
                    }


                    else
                    {
                        result = PurifyStringParameters(htmlDoc.DocumentNode.InnerText, new string[] { "\t", "\r" });
                    }
                    //result = PurifyStringParameters( htmlDoc.DocumentNode.InnerText, new string[]{"\t", "\r"});  
                    //result = htmlDoc.DocumentNode.InnerText;
                    //result = htmlDoc.DocumentNode.InnerHtml;
                }
            }
            catch (Exception e)
            {

                throw new InvalidOperationException("Error reading the job description of job offer",e);
            }
            return result;
        }

        private static HtmlNode ClearNodes(HtmlNode JobOfferElement)
        {
            //var trsToRemove = JobOfferElement.Elements("tr").ToList();

            //JobOfferElement.RemoveChild(trsToRemove[0]);
            //JobOfferElement.RemoveChild(trsToRemove[1]);
            //JobOfferElement.RemoveChild(trsToRemove[2]);

            JobOfferElement = RemoveDescendants(JobOfferElement, new string[] { "a", "img", "script", "style" });
            JobOfferElement.RemoveChild(JobOfferElement.Element("tr"));

            var trS = JobOfferElement.Elements("tr").ToList();

            bool removeNext = false;
            foreach (var item in trS)
            {
                if (removeNext == false)
                {
                    if (item.Descendants().Where(
                        d => (d.Attributes.Contains("class") &&
                            d.Attributes["class"].Value.Contains("button_new"))
                            ).Count() > 0)
                    {
                        removeNext = true;
                    }
                }
                if (removeNext == true)
                {
                    JobOfferElement.RemoveChild(item);
                }
            }

            return JobOfferElement;
        }

        private static HtmlNode RemoveDescendants(HtmlNode JobOfferElement, string[] elementTypesToRemove)
        {
            foreach (var elementToRemove in elementTypesToRemove)
            {
                var Child = JobOfferElement.Descendants(elementToRemove).FirstOrDefault();
                while (Child != null)
                {
                    Child.Remove();
                    Child = JobOfferElement.Descendants(elementToRemove).FirstOrDefault();
                }
            }
            return JobOfferElement;
        }
    }
}
