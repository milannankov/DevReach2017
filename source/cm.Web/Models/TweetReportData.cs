using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceManagement.Web.Models
{
    public class TweetReportData : TableEntity
    {
        public TweetReportData(string id)
        {
            this.PartitionKey = "tweets";
            this.RowKey = id;
        }

        public TweetReportData()
        {

        }

        public string Text
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public string Link
        {
            get;
            set;
        }

        public double Sentiment
        {
            get;
            set;
        }
    }
}