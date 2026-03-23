using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed record SalvarSituacaoAtualCommand(SituacaoAtualRegistro Registro) : IStorageCommand<StorageUnit>;

