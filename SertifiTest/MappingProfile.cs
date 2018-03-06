using System;
using AutoMapper;
using dto = SertifiTest.Models.DTO;
using dm = SertifiTest.Models.Domain;
using System.Linq;

namespace SertifiTest
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Ignore the YearlyGrades list we generate on the fly
            CreateMap<dto.Student, dm.Student>().ForMember(dest => dest.YearlyGrades, src => src.Ignore());
        }
    }
}
