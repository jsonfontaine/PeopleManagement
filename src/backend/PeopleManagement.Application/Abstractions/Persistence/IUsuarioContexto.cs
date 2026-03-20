namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta para identificar o usuario responsavel pela alteracao.
/// </summary>
public interface IUsuarioContexto
{
    string UsuarioAtual { get; }
}

