using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Expectativas;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class SalvarExpectativasHandler : IStorageCommandHandler<SalvarExpectativasCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public SalvarExpectativasHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(SalvarExpectativasCommand command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.Registro.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Registro.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Expectativas
            .FirstOrDefaultAsync(
                x => x.IdLiderado.ToLower() == lideradoIdStr
                     && x.Data == dataStr,
                cancellationToken);

        if (existente is null)
        {
            _dbContext.Expectativas.Add(new ExpectativaEntity
            {
                IdLiderado = lideradoIdStr,
                Data = dataStr,
                Valor = command.Registro.Valor
            });
        }
        else
        {
            existente.Valor = command.Registro.Valor;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

