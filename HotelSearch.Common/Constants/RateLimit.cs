using System;
using System.Configuration;

namespace HotelSearch.Common.Constants
{
    public class RateLimit
    {

        // rate  limit settings
        public readonly static double MAX_BUCKET_SIZE = Double.Parse(ConfigurationManager.AppSettings["MaxBucketSize"]);
        public readonly static double MAX_TIMEWINDOW = Double.Parse(ConfigurationManager.AppSettings["MaxTimeWindow"]);
        // message if rate limit is applied
        public const string RATE_LIMIT_APPLIED = "Request exceeded configured RateLimit! Please request as per your Rate Limit";

    }
}
