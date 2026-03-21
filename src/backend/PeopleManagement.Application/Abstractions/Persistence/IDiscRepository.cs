using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Application.Abstractions.Persistence
{
    // interface obsoleta, não utilizar mais
    public interface IDiscRepository
    {
        Task<DiscEntity> GetByIdLideradoAsync(Guid idLiderado);
        Task<List<DiscEntity>> ListByIdLideradoAsync(Guid idLiderado);
        Task SaveAsync(DiscEntity disc);
    }
}
