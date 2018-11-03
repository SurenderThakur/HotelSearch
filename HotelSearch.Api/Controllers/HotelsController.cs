using HotelSearch.Business.Provider;
using HotelSearch.Common.Constants;
using HotelSearch.Entities.Models.Hotels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using static HotelSearch.Common.Enums.ResponseEnum;

namespace HotelSearch.Api.Controllers
{

    [RoutePrefix("api/hotels")]
    public class HotelsController : BaseApiController
    {

        public HotelsController()
        {
            Debug.WriteLine("Hello" + _identity.Name + " Role:" + string.Join(",", _roles.ToList()));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("gethotelsbycity")]
        public IHttpActionResult GetHotelsByCity(RequestModel request)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // if rate limit applied then return status forbidden 
                // with rate limit message.
                if (_isRateLimitApplied)
                    return Content(HttpStatusCode.Forbidden, RateLimit.RATE_LIMIT_APPLIED);

                // if modelstate is invalid then return status badrequest
                // with modalstate
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, ModelState);

                var hotelsByCity = HotelsProvider.GetHotelsByCityId(request, out int total);
                response.HotelResponse = hotelsByCity;
                response.Total = total;
                response.Successful = total > 0 ? true : false;
                response.Code = total > 0 ? ResponseCode.Success : ResponseCode.Failed;

                return Ok(response);
            }
            catch (Exception ex )
            {

                Debug.WriteLine(string.Format("Error {0}", ex.Message));
                return Content(HttpStatusCode.InternalServerError, ModelState);
            }

        }


        [HttpPost]
        [Authorize]
        [Route("gethotelsbycityGuestUser")]
        public IHttpActionResult GetAuthorizedHotelsByCity(RequestModel request)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // if rate limit applied then return status forbidden 
                // with rate limit message.
                if (_isRateLimitApplied)
                    return Content(HttpStatusCode.Forbidden, RateLimit.RATE_LIMIT_APPLIED);

                // if modelstate is invalid then return status badrequest
                // with modalstate
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, ModelState);

                var hotelsByCity = HotelsProvider.GetHotelsByCityId(request, out int total);
                response.HotelResponse = hotelsByCity;
                response.Total = total;
                response.Successful = total > 0 ? true : false;
                response.Code = total > 0 ? ResponseCode.Success : ResponseCode.Failed;

                return Ok(response);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(string.Format("Error {0}", ex.Message));
                return Content(HttpStatusCode.InternalServerError, ModelState);
            }

        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("gethotelsbycityAdmin")]
        public IHttpActionResult GetAdminHotelsByCity(RequestModel request)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // if rate limit applied then return status forbidden 
                // with rate limit message.
                if (_isRateLimitApplied)
                    return Content(HttpStatusCode.Forbidden, RateLimit.RATE_LIMIT_APPLIED);

                // if modelstate is invalid then return status badrequest
                // with modalstate
                if (!ModelState.IsValid)
                    return Content(HttpStatusCode.BadRequest, ModelState);

                var hotelsByCity = HotelsProvider.GetHotelsByCityId(request, out int total);
                response.HotelResponse = hotelsByCity;
                response.Total = total;
                response.Successful = total > 0 ? true : false;
                response.Code = total > 0 ? ResponseCode.Success : ResponseCode.Failed;

                return Ok(response);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(string.Format("Error {0}", ex.Message));
                return Content(HttpStatusCode.InternalServerError, ModelState);
            }

        }


    }
}
