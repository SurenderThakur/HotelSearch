using HotelSearch.Common.Enums;
using System.Runtime.Serialization;
namespace HotelSearch.Entities.Models.Common
{

    [DataContract]
    public class ResponseModel
    {
        [DataMember]
        public bool Successful { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]    
        public ResponseEnum.ResponseCode Code { get; set; }
    }
}
