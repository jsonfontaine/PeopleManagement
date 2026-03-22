using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio SQLite da feature DISC.
/// </summary>
public sealed class SqliteDiscRepository : IDiscRepository
{
    private readonly PeopleManagementDbContext _context;

    public SqliteDiscRepository(PeopleManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<DiscRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var lideradoIdStr = lideradoId.ToString().ToLowerInvariant();

        var registros = await _context.Discs
            .AsNoTracking()
            .Where(d => d.IdLiderado.ToLower() == lideradoIdStr)
            .OrderByDescending(d => d.Data)
            .ToListAsync(cancellationToken);

        return registros
            .Select(d => new DiscRegistro(Guid.Parse(d.IdLiderado), DateOnly.Parse(d.Data), d.Valor))
            .ToArray();
    }

    public async Task SalvarAsync(DiscRegistro registro, CancellationToken cancellationToken)
    {
        var lideradoId = registro.LideradoId.ToString().ToLowerInvariant();
        var data = registro.Data.ToString("yyyy-MM-dd");

        var existente = await _context.Discs
            .FirstOrDefaultAsync(d => d.IdLiderado.ToLower() == lideradoId && d.Data == data, cancellationToken);

        if (existente is null)
        {
            _context.Discs.Add(new DiscEntity
            {
                IdLiderado = lideradoId,
                Data = data,
                Valor = registro.Valor
            });
        }
        else
        {
            existente.Valor = registro.Valor;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        var lideradoIdStr = lideradoId.ToString().ToLowerInvariant();
        var dataStr = data.ToString("yyyy-MM-dd");

        var existente = await _context.Discs
            .FirstOrDefaultAsync(d => d.IdLiderado.ToLower() == lideradoIdStr && d.Data == dataStr, cancellationToken);

        if (existente is null)
        {
            return;
        }

        _context.Discs.Remove(existente);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
