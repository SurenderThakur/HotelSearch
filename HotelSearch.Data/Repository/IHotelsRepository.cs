using HotelSearch.Entities.Models.Common;
using HotelSearch.Entities.Models.Hotels;
using System.Collections.Generic;

namespace HotelSearch.Data.Repository
{
    public interface IHotelsRepository
    {

        IList<HotelModel> GetAllHotels();

        IList<HotelModel> GetHotelsByCityId(RequestModel request, out int total);

    }
}
