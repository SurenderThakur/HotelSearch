using HotelSearch.Common.Enums;
using System;
using System.Runtime.Serialization;

namespace HotelSearch.Entities.Models.Common
{
    [DataContract]
    public class HotelModel : IComparable<HotelModel>
    {
        private readonly ResponseEnum.SortDirection _sortDirection = ResponseEnum.SortDirection.Asc;

        public HotelModel(ResponseEnum.SortDirection sortDirection = ResponseEnum.SortDirection.Asc)
        {
            _sortDirection = sortDirection;
        }

        [DataMember]
        public string CityId { get; set; }

        [DataMember]
        public int HotelId { get; set; }

        [DataMember]
        public string RoomName { get; set; }

        [DataMember]
        public double Price { get; set; }

        // Compare logic
        public int CompareTo(HotelModel other)
        {
            int result = this.Price.CompareTo(other.Price);
            if (_sortDirection == ResponseEnum.SortDirection.Desc)
            {
                result *= -1;
            }
            return result;
        }
    }
}
