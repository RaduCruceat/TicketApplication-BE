using TicketApplication.Common;
using TicketApplication.Data.Entities;
using TicketApplication.Data.Context;

using Microsoft.EntityFrameworkCore;
using TicketApplication.Data.EFRepositories;


namespace TicketApplication.Data.EFRepositories
{
    public class BonRepositoryEF : IBonRepositoryEF
    {
        private readonly BonContext _context;

        public BonRepositoryEF(BonContext context)
        {
            _context = context;
        }

        public async Task<Bon?> GetBonById(int id)
        {
            return await _context.Bon
                .FindAsync(id);
        }

        public async Task<IEnumerable<Bon>> GetAllBonByGhiseuId(int ghiseuId)
        {
            return await _context.Bon
                .Include(b => b.Ghiseu)
                .Where(b => b.IdGhiseu == ghiseuId && b.Ghiseu.Activ)
                .OrderBy(b => b.Stare)
                .ThenBy(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bon>> GetAllBon()
        {
            return await _context.Bon
                .Include(b => b.Ghiseu)
                .Where(b => b.Ghiseu.Activ)
                .OrderBy(b => b.Stare)
                .ThenBy(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Bon> AddBon(Bon bon)
        {
            await _context.Bon.AddAsync(bon);
            await _context.SaveChangesAsync();
            return bon;
        }

        public async Task MarkAsInProgress(int id)
        {
            var bon = await _context.Bon.FindAsync(id);
            if (bon != null)
            {
                bon.Stare = StareEnum.InCursDePreluare;
                bon.ModifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsReceived(int id)
        {
            var bon = await _context.Bon.FindAsync(id);
            if (bon != null)
            {
                bon.Stare = StareEnum.Preluat;
                bon.ModifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsClose(int id)
        {
            var bon = await _context.Bon.FindAsync(id);
            if (bon != null)
            {
                bon.Stare = StareEnum.Inchis;
                bon.ModifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}