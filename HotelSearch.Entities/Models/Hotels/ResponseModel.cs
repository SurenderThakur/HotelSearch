using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HotelSearch.Entities.Models.Hotels
{
    [DataContract]
    public class ResponseModel: Common.ResponseModel
    {
        [DataMember]
        public IList<Common.HotelModel> HotelResponse { get; set; }
        [DataMember]
        public int Total { get; set; }
    }
}
