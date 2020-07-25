using AutoMapper;
using CoreApi.Dtos;
using CoreApi.Models;

namespace CoreApi.Profiles
{
    public class JobProfile : Profile
    {
        /// <summary>
        /// AutoMapper profile for <see cref="Job"/>.
        /// </summary>
        public JobProfile()
        {
            CreateMap<Job, JobDto>();
            CreateMap<Job, JobUpdateDto>();
        }
    }
}
