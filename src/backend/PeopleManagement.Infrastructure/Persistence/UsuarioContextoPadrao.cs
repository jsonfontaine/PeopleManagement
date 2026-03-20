using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Infrastructure.Persistence;

/// <summary>
/// Implementacao padrao do usuario responsavel em ambiente local.
/// </summary>
public sealed class UsuarioContextoPadrao : IUsuarioContexto
{
    public string UsuarioAtual => "gestor-local";
}

