using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Opcoes;

public sealed record RemoverOpcoesCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

