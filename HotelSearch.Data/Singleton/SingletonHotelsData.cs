using HotelSearch.Data.Repository;
using HotelSearch.Entities.Models.Common;
using System.Collections.Generic;

namespace HotelSearch.Data.Singleton
{
    public class SingletonHotelsData
    {

        // declare private member
        private static readonly object _mutex = new object();
        private static SingletonHotelsData _instance = null;
        private IList<HotelModel> _hotelsList { get; set; }

        public SingletonHotelsData()
        {
            _hotelsList = new HotelsRepository().GetAllHotels();
        }

        public static SingletonHotelsData GetInstance()
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonHotelsData();
                    }
                }
            }

            return _instance;

        }

        public IList<HotelModel> GetAllHotels => _hotelsList;

    }
}
