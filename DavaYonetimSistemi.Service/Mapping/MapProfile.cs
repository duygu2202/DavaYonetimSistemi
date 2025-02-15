using AutoMapper;
using System.Linq;

public class MapProfile : Profile
{
    public MapProfile()
    {
        // Dava mappingleri
        CreateMap<Dava, DavaDto>().ReverseMap();
        CreateMap<Dava, DavaListDto>()
            .ForMember(dest => dest.DavaEdenler, opt => opt.MapFrom(src => 
                src.DavaSirketleri.Where(ds => ds.IsDavaEden)
                    .Select(ds => ds.Sirket.Ad)))
            .ForMember(dest => dest.DavaEdilenler, opt => opt.MapFrom(src => 
                src.DavaSirketleri.Where(ds => !ds.IsDavaEden)
                    .Select(ds => ds.Sirket.Ad)))
            .ForMember(dest => dest.Durum, opt => opt.MapFrom(src => src.DurumDava.Name))
            .ForMember(dest => dest.Sorumlu, opt => opt.MapFrom(src => src.Sorumlu.AdSoyad));

        // İcra mappingleri
        CreateMap<Icra, IcraDto>().ReverseMap();
        CreateMap<Icra, IcraListDto>()
            .ForMember(dest => dest.Alacaklilar, opt => opt.MapFrom(src => 
                src.IcraSirketleri.Where(ic => ic.IsAlacakli)
                    .Select(ic => ic.Sirket.Ad)))
            .ForMember(dest => dest.Borclular, opt => opt.MapFrom(src => 
                src.IcraSirketleri.Where(ic => !ic.IsAlacakli)
                    .Select(ic => ic.Sirket.Ad)))
            .ForMember(dest => dest.Durum, opt => opt.MapFrom(src => src.DurumIcra.Name))
            .ForMember(dest => dest.Sorumlu, opt => opt.MapFrom(src => src.Sorumlu.AdSoyad));

        // RaporLog mappingleri
        CreateMap<RaporLog, RaporLogDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
    }
} 