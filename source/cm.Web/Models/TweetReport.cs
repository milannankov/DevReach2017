using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceManagement.Web.Models
{
    public class TweetReport
    {
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

        public string SentimentImageUrl
        {
            get;
            set;
        }

        public double SentimentValue
        {
            get;
            set;
        }

        public static TweetReport FromData(TweetReportData data)
        {
            var tweetReport = new TweetReport();
            tweetReport.Author = data.Author;
            tweetReport.Link = data.Link;
            tweetReport.SentimentImageUrl = data.Sentiment >= 0.5 ? "/Content/Images/up.png" : "/Content/Images/down.png";
            tweetReport.Text = data.Text;
            tweetReport.SentimentValue = Math.Round(data.Sentiment, 2);

            return tweetReport;
        }
    }
}