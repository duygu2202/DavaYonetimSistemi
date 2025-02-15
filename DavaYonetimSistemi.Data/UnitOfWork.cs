using DavaYonetimDB.Core.Interfaces;
using DavaYonetimDB.Data.Context;
using DavaYonetimDB.Data.Repositories;

namespace DavaYonetimDB.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public IRepository<Dava> Davalar => GetRepository<Dava>();
        public IRepository<Icra> Icralar => GetRepository<Icra>();
        public IRepository<Adliye> Adliyeler => GetRepository<Adliye>();
        public IRepository<Sirket> Sirketler => GetRepository<Sirket>();
        public IRepository<Sorumlu> Sorumlular => GetRepository<Sorumlu>();
        public IRepository<DurumDava> DurumDavalar => GetRepository<DurumDava>();
        public IRepository<DurumIcra> DurumIcralar => GetRepository<DurumIcra>();
        public IRepository<DavaSirket> DavaSirketler => GetRepository<DavaSirket>();
        public IRepository<IcraSirket> IcraSirketler => GetRepository<IcraSirket>();

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 