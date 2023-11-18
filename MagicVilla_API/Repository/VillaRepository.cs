using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa villa)
        {
            villa.UpdatedDate = DateTime.Now;
            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
            return villa;
        }
    }
}
