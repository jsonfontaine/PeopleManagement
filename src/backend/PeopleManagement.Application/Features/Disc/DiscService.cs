// Este serviço está obsoleto e não deve mais ser utilizado.
// Toda manipulação de DISC deve ser feita via ILideradoRepository e Guid.
// Arquivo mantido apenas para referência histórica.

using System.Threading.Tasks;
using PeopleManagement.Domain.Liderados;
using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Disc
{
    public class DiscService
    {
        private readonly IDiscRepository _repository;

        public DiscService(IDiscRepository repository)
        {
            _repository = repository;
        }

        public async Task<DiscEntity> GetDiscAsync(Guid idLiderado)
        {
            return await _repository.GetByIdLideradoAsync(idLiderado);
        }

        public async Task SaveDiscAsync(Guid idLiderado, string valor, string data)
        {
            var entity = new DiscEntity
            {
                IdLiderado = idLiderado.ToString(),
                Valor = valor,
                Data = data
            };
            await _repository.SaveAsync(entity);
        }

        public async Task<List<DiscEntity>> ListDiscsAsync(Guid idLiderado)
        {
            return await _repository.ListByIdLideradoAsync(idLiderado);
        }
    }
}
