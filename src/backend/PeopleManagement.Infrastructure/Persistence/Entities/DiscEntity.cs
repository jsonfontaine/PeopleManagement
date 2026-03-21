using System;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Infrastructure.Persistence.Entities
{
    public class DiscEntity
    {
        public string IdLiderado { get; set; }
        public string Valor { get; set; }
        public string Data { get; set; } // formato yyyy-MM-dd
    }
}
