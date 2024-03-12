using AutoMapper;
using SMSVendor.DTOs;

namespace SMSVendor.Mappers
{
    public class SMSRequestMapper : Profile
    {
        public SMSRequestMapper() 
        {
            CreateMap<RequestDTO,InnerDTO>().ReverseMap();
        }
    }
}
