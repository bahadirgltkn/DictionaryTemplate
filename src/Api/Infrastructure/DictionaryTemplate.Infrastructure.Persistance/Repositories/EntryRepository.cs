using DictionaryTemplate.Api.Application.Interfaces.Repositories;
using DictionaryTemplate.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DictionaryTemplate.Infrastructure.Persistance.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
