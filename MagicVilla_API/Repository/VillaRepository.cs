using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Villa villa)
        {
            await _db.Villas.AddAsync(villa);
            await SaveAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool track = true)
        {
            IQueryable<Villa> query = _db.Villas;

            if (!track)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            var query = _db.Villas;

            if(filter != null)
                query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Villa villa)
        {
            _db.Villas.Remove(villa);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa villa)
        {
            _db.Villas.Update(villa);
            await SaveAsync();
        }
    }
}
