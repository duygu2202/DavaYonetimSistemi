using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Entities;
using DavaYonetimDB.Core.Interfaces;

namespace DavaYonetimDB.Service.Services
{
    public class IcraService : IIcraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IcraService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IcraDto> GetByIdAsync(int id)
        {
            var icra = await _unitOfWork.Icralar.Where(i => i.Id == id)
                .Include(i => i.DurumIcra)
                .Include(i => i.Sorumlu)
                .Include(i => i.IcraSirketleri)
                    .ThenInclude(ıs => ıs.Sirket)
                .FirstOrDefaultAsync();

            return _mapper.Map<IcraDto>(icra);
        }

        public async Task<IEnumerable<IcraDto>> GetAllAsync()
        {
            var icralar = await _unitOfWork.Icralar.GetAll()
                .Include(i => i.DurumIcra)
                .Include(i => i.Sorumlu)
                .Include(i => i.IcraSirketleri)
                    .ThenInclude(ıs => ıs.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<IcraDto>>(icralar);
        }

        public async Task<IcraDto> AddAsync(IcraDto icraDto)
        {
            var icra = _mapper.Map<Icra>(icraDto);
            await _unitOfWork.Icralar.AddAsync(icra);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<IcraDto>(icra);
        }

        public async Task UpdateAsync(IcraDto icraDto)
        {
            var icra = _mapper.Map<Icra>(icraDto);
            _unitOfWork.Icralar.Update(icra);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var icra = await _unitOfWork.Icralar.GetByIdAsync(id);
            _unitOfWork.Icralar.Remove(icra);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<IcraDto>> GetIcrasByAdliyeAsync(int adliyeId)
        {
            var icralar = await _unitOfWork.Icralar.Where(i => i.AdliyeId == adliyeId)
                .Include(i => i.DurumIcra)
                .Include(i => i.Sorumlu)
                .Include(i => i.IcraSirketleri)
                    .ThenInclude(ıs => ıs.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<IcraDto>>(icralar);
        }

        public async Task<IEnumerable<IcraDto>> GetIcrasBySorumluAsync(int sorumluId)
        {
            var icralar = await _unitOfWork.Icralar.Where(i => i.SorumluId == sorumluId)
                .Include(i => i.DurumIcra)
                .Include(i => i.Sorumlu)
                .Include(i => i.IcraSirketleri)
                    .ThenInclude(ıs => ıs.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<IcraDto>>(icralar);
        }

        public async Task<IEnumerable<IcraDto>> GetIcrasBySirketAsync(int sirketId, bool isAlacakli)
        {
            var icralar = await _unitOfWork.Icralar
                .Where(i => i.IcraSirketleri.Any(ıs => ıs.SirketId == sirketId && ıs.IsAlacakli == isAlacakli))
                .Include(i => i.DurumIcra)
                .Include(i => i.Sorumlu)
                .Include(i => i.IcraSirketleri)
                    .ThenInclude(ıs => ıs.Sirket)
                .ToListAsync();

            return _mapper.Map<IEnumerable<IcraDto>>(icralar);
        }

        public async Task<DataTableResponse<IcraListDto>> GetIcraListAsync(DataTableParameters parameters, int sirketId, string durum = null)
        {
            var query = _unitOfWork.Icralar
                .Where(i => i.IcraSirketleri.Any(ic => ic.SirketId == sirketId));

            // Durum filtresi
            if (!string.IsNullOrEmpty(durum))
            {
                query = query.Where(i => i.DurumIcra.Name == durum);
            }

            // Diğer filtreler
            if (!string.IsNullOrEmpty(parameters.UyapBirimi))
                query = query.Where(i => i.UyapBirimi.Contains(parameters.UyapBirimi));

            if (!string.IsNullOrEmpty(parameters.EsasNo))
                query = query.Where(i => i.EsasNo.Contains(parameters.EsasNo));

            if (!string.IsNullOrEmpty(parameters.Sorumlu))
                query = query.Where(i => i.Sorumlu.Contains(parameters.Sorumlu));

            if (parameters.BaslangicTarihi.HasValue)
                query = query.Where(i => i.CreatedDate >= parameters.BaslangicTarihi);

            if (parameters.BitisTarihi.HasValue)
                query = query.Where(i => i.CreatedDate <= parameters.BitisTarihi);

            if (!string.IsNullOrEmpty(parameters.Alacaklilar))
                query = query.Where(i => i.Alacaklilar.Any(a => a.Ad.Contains(parameters.Alacaklilar)));

            if (!string.IsNullOrEmpty(parameters.Borclular))
                query = query.Where(i => i.Borclular.Any(b => b.Ad.Contains(parameters.Borclular)));

            // Toplam kayıt sayısı
            var totalCount = await query.CountAsync();

            // Sıralama
            if (parameters.Order != null && parameters.Order.Any())
            {
                var order = parameters.Order[0];
                var column = parameters.Columns[order.Column];
                var propertyName = column.Data;

                query = order.Dir == "asc" 
                    ? query.OrderBy(i => EF.Property<object>(i, propertyName))
                    : query.OrderByDescending(i => EF.Property<object>(i, propertyName));
            }

            // Sayfalama
            query = query.Skip(parameters.Start).Take(parameters.Length);

            var data = await query.ToListAsync();
            var mappedData = _mapper.Map<IEnumerable<IcraListDto>>(data);

            return new DataTableResponse<IcraListDto>
            {
                Data = mappedData,
                TotalCount = totalCount,
                FilteredCount = totalCount
            };
        }

        public async Task<IEnumerable<IcraListDto>> GetIcraExportListAsync(int sirketId, string durum = null)
        {
            var query = _unitOfWork.Icralar
                .Where(i => i.IcraSirketleri.Any(ic => ic.SirketId == sirketId));

            if (!string.IsNullOrEmpty(durum))
            {
                query = query.Where(i => i.DurumIcra.Name == durum);
            }

            var data = await query.ToListAsync();
            return _mapper.Map<IEnumerable<IcraListDto>>(data);
        }
    }
} 