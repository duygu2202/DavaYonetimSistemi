using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Entities;
using DavaYonetimDB.Core.Interfaces;

namespace DavaYonetimDB.Service.Services
{
    public class DavaService : IDavaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DavaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DavaDto> GetByIdAsync(int id)
        {
            var dava = await _unitOfWork.Davalar.Where(d => d.Id == id)
                .Include(d => d.DurumDava)
                .Include(d => d.Sorumlu)
                .Include(d => d.DavaSirketleri)
                    .ThenInclude(ds => ds.Sirket)
                .FirstOrDefaultAsync();

            return _mapper.Map<DavaDto>(dava);
        }

        public async Task<IEnumerable<DavaDto>> GetAllAsync()
        {
            var davalar = await _unitOfWork.Davalar.GetAll()
                .Include(d => d.DurumDava)
                .Include(d => d.Sorumlu)
                .Include(d => d.DavaSirketleri)
                    .ThenInclude(ds => ds.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DavaDto>>(davalar);
        }

        public async Task<DavaDto> AddAsync(DavaDto davaDto)
        {
            var dava = _mapper.Map<Dava>(davaDto);
            await _unitOfWork.Davalar.AddAsync(dava);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DavaDto>(dava);
        }

        public async Task UpdateAsync(DavaDto davaDto)
        {
            var dava = _mapper.Map<Dava>(davaDto);
            _unitOfWork.Davalar.Update(dava);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var dava = await _unitOfWork.Davalar.GetByIdAsync(id);
            _unitOfWork.Davalar.Remove(dava);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<DavaDto>> GetDavasByAdliyeAsync(int adliyeId)
        {
            var davalar = await _unitOfWork.Davalar.Where(d => d.AdliyeId == adliyeId)
                .Include(d => d.DurumDava)
                .Include(d => d.Sorumlu)
                .Include(d => d.DavaSirketleri)
                    .ThenInclude(ds => ds.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DavaDto>>(davalar);
        }

        public async Task<IEnumerable<DavaDto>> GetDavasBySorumluAsync(int sorumluId)
        {
            var davalar = await _unitOfWork.Davalar.Where(d => d.SorumluId == sorumluId)
                .Include(d => d.DurumDava)
                .Include(d => d.Sorumlu)
                .Include(d => d.DavaSirketleri)
                    .ThenInclude(ds => ds.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DavaDto>>(davalar);
        }

        public async Task<IEnumerable<DavaDto>> GetDavasBySirketAsync(int sirketId, bool isDavaEden)
        {
            var davalar = await _unitOfWork.Davalar
                .Where(d => d.DavaSirketleri.Any(ds => ds.SirketId == sirketId && ds.IsDavaEden == isDavaEden))
                .Include(d => d.DurumDava)
                .Include(d => d.Sorumlu)
                .Include(d => d.DavaSirketleri)
                    .ThenInclude(ds => ds.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DavaDto>>(davalar);
        }

        public async Task<DataTableResponse<DavaListDto>> GetDavaListAsync(DataTableParameters parameters, int sirketId, string durum = null)
        {
            var query = _unitOfWork.Davalar
                .Where(d => d.DavaSirketleri.Any(ds => ds.SirketId == sirketId));

            // Durum filtresi
            if (!string.IsNullOrEmpty(durum))
            {
                query = query.Where(d => d.DurumDava.Name == durum);
            }

            // Diğer filtreler
            if (!string.IsNullOrEmpty(parameters.UyapBirimi))
                query = query.Where(d => d.UyapBirimi.Contains(parameters.UyapBirimi));

            if (!string.IsNullOrEmpty(parameters.EsasNo))
                query = query.Where(d => d.EsasNo.Contains(parameters.EsasNo));

            if (!string.IsNullOrEmpty(parameters.Sorumlu))
                query = query.Where(d => d.Sorumlu.Contains(parameters.Sorumlu));

            if (parameters.BaslangicTarihi.HasValue)
                query = query.Where(d => d.CreatedDate >= parameters.BaslangicTarihi);

            if (parameters.BitisTarihi.HasValue)
                query = query.Where(d => d.CreatedDate <= parameters.BitisTarihi);

            if (!string.IsNullOrEmpty(parameters.DavaEdenler))
                query = query.Where(d => d.DavaEdenler.Any(de => de.Ad.Contains(parameters.DavaEdenler)));

            if (!string.IsNullOrEmpty(parameters.DavaEdilenler))
                query = query.Where(d => d.DavaEdilenler.Any(de => de.Ad.Contains(parameters.DavaEdilenler)));

            // Toplam kayıt sayısı
            var totalCount = await query.CountAsync();

            // Sıralama
            if (parameters.Order != null && parameters.Order.Any())
            {
                var order = parameters.Order[0];
                var column = parameters.Columns[order.Column];
                var propertyName = column.Data;

                query = order.Dir == "asc" 
                    ? query.OrderBy(d => EF.Property<object>(d, propertyName))
                    : query.OrderByDescending(d => EF.Property<object>(d, propertyName));
            }

            // Sayfalama
            query = query.Skip(parameters.Start).Take(parameters.Length);

            var data = await query.ToListAsync();
            var mappedData = _mapper.Map<IEnumerable<DavaListDto>>(data);

            return new DataTableResponse<DavaListDto>
            {
                Data = mappedData,
                TotalCount = totalCount,
                FilteredCount = totalCount // Filtrelenmiş kayıt sayısı
            };
        }

        public async Task<IEnumerable<DavaListDto>> GetDavaExportListAsync(int sirketId, string durum = null)
        {
            var query = _unitOfWork.Davalar
                .Where(d => d.DavaSirketleri.Any(ds => ds.SirketId == sirketId));

            if (!string.IsNullOrEmpty(durum))
            {
                query = query.Where(d => d.DurumDava.Name == durum);
            }

            var data = await query.ToListAsync();
            return _mapper.Map<IEnumerable<DavaListDto>>(data);
        }
    }
} 