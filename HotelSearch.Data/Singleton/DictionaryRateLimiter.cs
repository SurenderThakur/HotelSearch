using HotelSearch.Entities.Models.RateLimit;
using System;
using System.Collections.Generic;

namespace HotelSearch.Data.Singleton
{
    public class DictionaryRateLimiter
    {
        private static readonly object _mutex = new object();
        private static DictionaryRateLimiter _instance = null;
        private static Dictionary<string,RateLimitModel> _dictionaryRateLimit { get; set; }

        public DictionaryRateLimiter()
        {
            _dictionaryRateLimit = new Dictionary<string, RateLimitModel>();
        }

        public static DictionaryRateLimiter GetInstance()
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null )
                    {
                        _instance = new DictionaryRateLimiter();
                    }
                }
            }

            return _instance;
        }

        // Get rate Limit
        public RateLimitModel GetRateLimitByKey(string key)
        {
            RateLimitModel rateLimit = null;
            try
            {
                if (!string.IsNullOrEmpty(key) && _dictionaryRateLimit.ContainsKey(key))
                {
                    rateLimit = _dictionaryRateLimit[key];
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rateLimit;
        }

        // Update rate Limit
        public void UpdateRateLimit(RateLimitModel rateLimit)
        {
            try
            {
                if (!string.IsNullOrEmpty(rateLimit.Token))
                {
                    if (_dictionaryRateLimit.ContainsKey(rateLimit.Token))
                    {
                        _dictionaryRateLimit.Remove(rateLimit.Token);
                    }
                    _dictionaryRateLimit.Add(rateLimit.Token, rateLimit);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
