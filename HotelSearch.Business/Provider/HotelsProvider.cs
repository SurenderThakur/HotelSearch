using HotelSearch.Data.Repository;
using HotelSearch.Entities.Models.Common;
using HotelSearch.Entities.Models.Hotels;
using System;
using System.Collections.Generic;

namespace HotelSearch.Business.Provider
{
    public class HotelsProvider
    {

        public static IList<HotelModel> GetHotelsByCityId(RequestModel request, out int total)
        {
            total = 0;
            try
            {
                return new HotelsRepository().GetHotelsByCityId(request, out total);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
