namespace HotelSearch.Entities.Models.RateLimit
{
    public class RateLimitModel
    {

        public string Token { get; set; }
        public double FillRate { get; set; }
        public double CurrentBucketSize { get; set; }
        public double MaxBucketSize { get; set; }
        public long LastUpdatedTime { get; set; }
        
    }
}
