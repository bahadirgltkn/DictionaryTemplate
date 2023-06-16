using DictionaryTemplate.Api.Application.Interfaces.Repositories;
using DictionaryTemplate.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DictionaryTemplate.Infrastructure.Persistance.Repositories
{
    public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
    {
        public EmailConfirmationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
