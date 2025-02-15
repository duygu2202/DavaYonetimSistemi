namespace DavaYonetimDB.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Dava> Davalar { get; }
        IRepository<Icra> Icralar { get; }
        IRepository<Adliye> Adliyeler { get; }
        IRepository<Sirket> Sirketler { get; }
        IRepository<Sorumlu> Sorumlular { get; }
        IRepository<DurumDava> DurumDavalar { get; }
        IRepository<DurumIcra> DurumIcralar { get; }
        IRepository<DavaSirket> DavaSirketler { get; }
        IRepository<IcraSirket> IcraSirketler { get; }
        Task<int> SaveChangesAsync();
    }
} 