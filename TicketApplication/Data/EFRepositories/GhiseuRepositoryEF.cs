using Microsoft.EntityFrameworkCore;
using TicketApplication.Data.Entities;
using TicketApplication.Data.EFRepositories;
using TicketApplication.Data.Context;

namespace TicketApplication.Data.EFRepositories
{
    public class GhiseuRepositoryEF : IGhiseuRepositoryEF
    {
        private readonly BonContext _context;

        public GhiseuRepositoryEF(BonContext context)
        {
            _context = context;
        }

        public async Task<Ghiseu?> GetGhiseuById(int id)
        {
            return await _context.Ghiseu
                .FindAsync(id);
        }

        public async Task<IEnumerable<Ghiseu>> GetAllGhiseu()
        {
            return await _context.Ghiseu
                .OrderBy(g => g.Activ)
                .ThenBy(g => g.Denumire)
                .ToListAsync();
        }

        public async Task<Ghiseu> AddGhiseu(Ghiseu ghiseu)
        {
           
            await _context.Ghiseu.AddAsync(ghiseu);
            await _context.SaveChangesAsync();
            return ghiseu;
        }

        public async Task EditGhiseu(Ghiseu ghiseu)
        {
            var existingGhiseu = await _context.Ghiseu.FindAsync(ghiseu.Id);
            if (existingGhiseu != null)
            {
                existingGhiseu.Cod = ghiseu.Cod;
                existingGhiseu.Denumire = ghiseu.Denumire;
                existingGhiseu.Descriere = ghiseu.Descriere;
                existingGhiseu.Icon = ghiseu.Icon;

                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsActive(int id)
        {
            var ghiseu = await _context.Ghiseu.FindAsync(id);
            if (ghiseu != null)
            {
                ghiseu.Activ = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsInactive(int id)
        {
            var ghiseu = await _context.Ghiseu.FindAsync(id);
            if (ghiseu != null)
            {
                ghiseu.Activ = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGhiseu(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Delete related Bon records first
                var relatedBons = await _context.Bon
                    .Where(b => b.IdGhiseu == id)
                    .ToListAsync();

                _context.Bon.RemoveRange(relatedBons);

                // Delete the Ghiseu
                var ghiseu = await _context.Ghiseu.FindAsync(id);
                if (ghiseu != null)
                {
                    _context.Ghiseu.Remove(ghiseu);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}