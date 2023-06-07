using DictionaryTemplate.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryTemplate.Infrastructure.Persistance.EntityConfigurations.Title
{
    public class TitleEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Title>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Title> builder)
        {
            base.Configure(builder);

            builder.ToTable("title", DictionaryTemplateContext.DEFAULT_SCHEMA);

            // a user can have multiple titles
            builder.HasOne(i => i.CreatedBy).WithMany(i => i.Titles).HasForeignKey(i => i.CreatedById);
        }
    }
}
