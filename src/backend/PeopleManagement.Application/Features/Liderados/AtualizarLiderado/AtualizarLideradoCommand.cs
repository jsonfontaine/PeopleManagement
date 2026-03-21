using System;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Application.Features.Liderados.AtualizarLiderado;

public sealed record AtualizarLideradoCommand(Guid Id, string Nome);

