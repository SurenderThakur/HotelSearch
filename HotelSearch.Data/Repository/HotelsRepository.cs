using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using HotelSearch.Data.Singleton;
using HotelSearch.Entities.Models.Common;
using HotelSearch.Entities.Models.Hotels;

namespace HotelSearch.Data.Repository
{
    public class HotelsRepository : IHotelsRepository
    {

        public IList<HotelModel> GetAllHotels()
        {
            List<HotelModel> hotelsList = new List<HotelModel>();

            try
            {
                var folderPath = HttpContext.Current.Server.MapPath("~/App_Data/HotelsData/");
                string filePath = Path.Combine(folderPath, "hoteldb.csv");
                hotelsList = (from line in File.ReadAllLines(filePath)
                                                    .Skip(1)
                              let columns = line.Split(',')
                              select new HotelModel
                              {
                                  CityId = columns[0],
                                  HotelId = int.Parse(columns[1]),
                                  RoomName = columns[2],
                                  Price = double.Parse(columns[3])
                              }).ToList();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(string.Format("Error {0}", ex.Message));
            }

            return hotelsList;

        }

        public IList<HotelModel> GetHotelsByCityId(RequestModel request,out int total)
        {
            List<HotelModel> hotelsListByCityId = new List<HotelModel>();
            total = 0;
            try
            {
                var allHotels = SingletonHotelsData.GetInstance().GetAllHotels;
                if (allHotels.Any())
                {
                    hotelsListByCityId = allHotels
                                            .Where(h => h.CityId.Equals(request.CityId, StringComparison.OrdinalIgnoreCase))
                                            .Select(h => new HotelModel(request.SortDirection)
                                            {
                                                CityId = h.CityId,
                                                HotelId = h.HotelId,
                                                RoomName = h.RoomName,
                                                Price = h.Price
                                            }).ToList();

                    hotelsListByCityId.Sort();

                    total = hotelsListByCityId.Count;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(string.Format("Error {0}", ex.Message));
            };

            return hotelsListByCityId;

        }

    }
}
