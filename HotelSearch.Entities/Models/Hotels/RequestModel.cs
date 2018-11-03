using HotelSearch.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace HotelSearch.Entities.Models.Hotels
{

    public class RequestModel
    {

        // CityId for filtering hotels  by city
        [Required]
        public string CityId { get; set; }

        // SortDirection for sorting hotels in response
        [EnumDataType(typeof(ResponseEnum.SortDirection))]
        public ResponseEnum.SortDirection SortDirection { get; set; }

    }
}

