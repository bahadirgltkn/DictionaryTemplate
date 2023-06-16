using DictionaryTemplate.Api.Application.Interfaces.Repositories;
using DictionaryTemplate.Api.Domain.Models;
using DictionaryTemplate.Infrastructure.Persistance.Contexts;
namespace DictionaryTemplate.Infrastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DictionaryTemplateContext dbContext) : base(dbContext)
        {
        }
    }
}
