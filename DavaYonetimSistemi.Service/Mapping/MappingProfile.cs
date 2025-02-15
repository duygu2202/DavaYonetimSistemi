using AutoMapper;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Entities;

namespace DavaYonetimDB.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dava, DavaDto>()
                .ForMember(dest => dest.Durum, opt => opt.MapFrom(src => src.DurumDava.Name))
                .ForMember(dest => dest.Sorumlu, opt => opt.MapFrom(src => $"{src.Sorumlu.Ad} {src.Sorumlu.Soyad}"))
                .ForMember(dest => dest.DavaEdenler, opt => opt.MapFrom(src => 
                    src.DavaSirketleri.Where(ds => ds.IsDavaEden).Select(ds => ds.Sirket.Name).ToList()))
                .ForMember(dest => dest.DavaEdilenler, opt => opt.MapFrom(src => 
                    src.DavaSirketleri.Where(ds => !ds.IsDavaEden).Select(ds => ds.Sirket.Name).ToList()));

            CreateMap<Icra, IcraDto>()
                .ForMember(dest => dest.Durum, opt => opt.MapFrom(src => src.DurumIcra.Name))
                .ForMember(dest => dest.Sorumlu, opt => opt.MapFrom(src => $"{src.Sorumlu.Ad} {src.Sorumlu.Soyad}"))
                .ForMember(dest => dest.Alacaklilar, opt => opt.MapFrom(src => 
                    src.IcraSirketleri.Where(ıs => ıs.IsAlacakli).Select(ıs => ıs.Sirket.Name).ToList()))
                .ForMember(dest => dest.Borclular, opt => opt.MapFrom(src => 
                    src.IcraSirketleri.Where(ıs => !ıs.IsAlacakli).Select(ıs => ıs.Sirket.Name).ToList()));
        }
    }
} 