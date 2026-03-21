using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Infrastructure.Persistence.Repositories
{
    // Classe obsoleta, não utilizar mais
    public class SqliteDiscRepository : IDiscRepository
    {
        private readonly PeopleManagementDbContext _context;

        public SqliteDiscRepository(PeopleManagementDbContext context)
        {
            _context = context;
        }

        public async Task<DiscEntity> GetByIdLideradoAsync(Guid idLiderado)
        {
            // Retorna o registro mais recente para o liderado
            var idLideradoStr = idLiderado.ToString();
            return await _context.Set<DiscEntity>()
                .Where(d => d.IdLiderado == idLideradoStr)
                .OrderByDescending(d => d.Data)
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync(DiscEntity disc)
        {
            // Usa IdLiderado+Data como chave composta
            var existing = await _context.Set<DiscEntity>()
                .FirstOrDefaultAsync(d => d.IdLiderado == disc.IdLiderado && d.Data == disc.Data);
            if (existing != null)
            {
                existing.Valor = disc.Valor;
                _context.Update(existing);
            }
            else
            {
                await _context.AddAsync(disc);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<DiscEntity>> ListByIdLideradoAsync(Guid idLiderado)
        {
            var idLideradoStr = idLiderado.ToString();
            return await _context.Set<DiscEntity>()
                .Where(d => d.IdLiderado == idLideradoStr)
                .OrderByDescending(d => d.Data)
                .ToListAsync();
        }
    }
}
