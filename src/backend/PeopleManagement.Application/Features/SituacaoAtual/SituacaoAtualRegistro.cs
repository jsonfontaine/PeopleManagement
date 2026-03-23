namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed record SituacaoAtualRegistro(Guid LideradoId, DateOnly Data, string Valor);

