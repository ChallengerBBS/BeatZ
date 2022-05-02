using AutoMapper;
using BeatZ.Domain.Dtos;
using BeatZ.Domain.Entities;

namespace BeatZ.Application.Common.Profiles
{
    public class TrackProfile : Profile
    {
        public TrackProfile()
        {
            CreateMap<TrackListDto, Track>()
                .ForMember(
                    dest => dest.TrackId,
                    opt => opt.MapFrom(src => $"{src.TrackId}")
                )
                .ForMember(
                    dest => dest.TrackName,
                    opt => opt.MapFrom(src => $"{src.TrackName}")
                ).ReverseMap();
        }
    }
}
