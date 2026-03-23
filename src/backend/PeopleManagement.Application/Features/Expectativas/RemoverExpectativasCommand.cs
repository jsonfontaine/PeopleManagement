using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Expectativas;

public sealed record RemoverExpectativasCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

