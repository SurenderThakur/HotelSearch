using HotelSearch.Business.RateLimiter;
using HotelSearch.Common.Constants;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace HotelSearch.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public readonly string _roles = string.Empty;
        public bool _isRateLimitApplied = false;
        public readonly ClaimsIdentity _identity = null;

        public BaseApiController()
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    _identity = (ClaimsIdentity)User.Identity;

                    var key = _identity.Claims.Where(c => c.Type == ClaimTypes.SerialNumber)
                                .Select(c => c.Value)
                                .SingleOrDefault();

                    var bucketSize = _identity.Claims.Where(c => c.Type == ClaimTypes.UserData)
                                    .Select(c => c.Value)
                                    .SingleOrDefault();

                    var roles = _identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value);

                    if (!string.IsNullOrEmpty(key))
                    {
                        bucketSize = string.IsNullOrEmpty(bucketSize) ? RateLimit.MAX_BUCKET_SIZE.ToString() : bucketSize;
                        double.TryParse(bucketSize, out double outVal);

                        
                        _isRateLimitApplied = !(new RateLimiter().ApplyRateLimit(key, outVal));
                    }

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(string.Format("Error {0}", ex.Message));
            }
        }
    }
}
