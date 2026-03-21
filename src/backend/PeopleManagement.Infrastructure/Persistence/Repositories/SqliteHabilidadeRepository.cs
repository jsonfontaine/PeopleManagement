using System.Data;
using Dapper;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

public sealed class SqliteHabilidadeRepository : IHabilidadeRepository
{
    private readonly IDbConnection _connection;

    public SqliteHabilidadeRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AdicionarAsync(HabilidadeRegistro registro, CancellationToken cancellationToken)
    {
        const string sql = @"INSERT INTO Habilidade (IdLiderado, Valor, Data) VALUES (@LideradoId, @Valor, @Data);";
        await _connection.ExecuteAsync(sql, new { LideradoId = registro.LideradoId.ToString(), registro.Valor, Data = registro.Data.ToString("yyyy-MM-dd") });
    }

    public async Task<IReadOnlyCollection<HabilidadeRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT IdLiderado, Valor, Data FROM Habilidade WHERE IdLiderado = @LideradoId ORDER BY Data DESC;";
        var result = await _connection.QueryAsync(sql, new { LideradoId = lideradoId.ToString() });
        return result.Select(r => new HabilidadeRegistro(Guid.Parse(r.IdLiderado), DateOnly.Parse(r.Data), r.Valor)).ToList();
    }

    public async Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        const string sql = @"DELETE FROM Habilidade WHERE IdLiderado = @LideradoId AND Data = @Data;";
        await _connection.ExecuteAsync(sql, new { LideradoId = lideradoId.ToString(), Data = data.ToString("yyyy-MM-dd") });
    }
}

