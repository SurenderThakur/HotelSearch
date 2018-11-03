using HotelSearch.Common.Constants;
using HotelSearch.Data.Singleton;
using HotelSearch.Entities.Models.RateLimit;
using System;

namespace HotelSearch.Business.RateLimiter
{
    public class RateLimiter
    {
        private readonly DictionaryRateLimiter _instance = null;
        private readonly double _amount = 0;
        private readonly double _maxBucketSize = 0;
        private readonly double _maxTimeWindow = 0;

        public RateLimiter()
        {

            _instance = DictionaryRateLimiter.GetInstance();
            _amount = 1;
            _maxBucketSize = RateLimit.MAX_BUCKET_SIZE;
            _maxTimeWindow = RateLimit.MAX_TIMEWINDOW;

        }

        public bool ApplyRateLimit(string key, double bucketSize)
        {
            bool resourceConsumed = false;
            var rateLimit = _instance.GetRateLimitByKey(key);
            if (rateLimit != null)
            {
                resourceConsumed = ConsumeResource(rateLimit,out RateLimitModel updatedRateLimit);
                _instance.UpdateRateLimit(updatedRateLimit);
            }
            else
            {
                DateTimeOffset currentTime = DateTimeOffset.Now;
                long maxTimeWindow = currentTime.AddSeconds(_maxTimeWindow).ToUnixTimeMilliseconds();

                var newrateLimit = new RateLimitModel
                {
                    Token = key,
                    FillRate = bucketSize / maxTimeWindow,
                    CurrentBucketSize = bucketSize,
                    MaxBucketSize = bucketSize,
                    LastUpdatedTime = currentTime.ToUnixTimeMilliseconds()
                };
                _instance.UpdateRateLimit(newrateLimit);
                resourceConsumed = true;
            }

            return resourceConsumed;

        }

        public bool ConsumeResource(RateLimitModel rateLimit, out RateLimitModel updatedRateLimit)
        {
            bool resourceConsumed = false;
            double currentBucket = rateLimit.CurrentBucketSize;
            double maxBucket = rateLimit.MaxBucketSize;
            long lastUpdatedTime = rateLimit.LastUpdatedTime;
            double fillRate = rateLimit.FillRate;

            updatedRateLimit = new RateLimitModel();

            // check if key is suspended, if yes return false
            if (lastUpdatedTime <= DateTimeOffset.Now.ToUnixTimeMilliseconds())
            {
                // get the time in milliseconds , since the lastupdatedtime
                long timeSinceLastUpdatedTime = 
                    DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastUpdatedTime;

                // get Current bucket
                currentBucket = Math.Min(maxBucket,
                                    currentBucket + timeSinceLastUpdatedTime * fillRate);

                // get new lastUpdatedTime
                lastUpdatedTime += timeSinceLastUpdatedTime;

                // uppdate rateLimit
                updatedRateLimit.Token = rateLimit.Token;
                updatedRateLimit.MaxBucketSize = rateLimit.MaxBucketSize;
                updatedRateLimit.FillRate = rateLimit.FillRate;

                if (currentBucket >= _amount)
                {
                    currentBucket -= _amount;
                    updatedRateLimit.CurrentBucketSize = currentBucket;
                    updatedRateLimit.MaxBucketSize = rateLimit.MaxBucketSize;
                    updatedRateLimit.LastUpdatedTime = lastUpdatedTime;
                    resourceConsumed = true;
                }
                

            }
            return resourceConsumed;
        }

    }
}
