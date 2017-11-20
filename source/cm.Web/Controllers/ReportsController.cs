using ConferenceManagement.Web.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConferenceManagement.Web.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            var models = this.GetTweets();

            return View(models);
        }

        private IEnumerable<TweetReport> GetTweets()
        {
            var storageAccount = this.GetStorageAccount();
            var table = this.GetTweetsTable(storageAccount);
            var query = new TableQuery<TweetReportData>()
                .Take(50)
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "tweets"));
            var listings = new List<TweetReport>();

            foreach (TweetReportData entity in table.ExecuteQuery(query))
            {
                listings.Add(TweetReport.FromData(entity));
            }

            return listings;
        }

        private CloudStorageAccount GetStorageAccount()
        {
            var connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storage = CloudStorageAccount.Parse(connectionString);

            return storage;
        }

        private CloudTable GetTweetsTable(CloudStorageAccount storage)
        {
            var tableClient = storage.CreateCloudTableClient();
            var table = tableClient.GetTableReference("tweets");

            table.CreateIfNotExists();

            return table;
        }
    }
}