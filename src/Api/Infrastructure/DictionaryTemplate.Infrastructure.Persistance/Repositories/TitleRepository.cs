using DictionaryTemplate.Api.Application.Interfaces.Repositories;
using DictionaryTemplate.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DictionaryTemplate.Infrastructure.Persistance.Repositories
{
    public class TitleRepository : GenericRepository<Title>, ITitleRepository
    {
        public TitleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
