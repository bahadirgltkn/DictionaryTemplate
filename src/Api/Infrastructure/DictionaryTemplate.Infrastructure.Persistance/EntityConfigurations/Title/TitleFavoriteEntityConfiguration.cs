using DictionaryTemplate.Api.Domain.Models;
using DictionaryTemplate.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryTemplate.Infrastructure.Persistance.EntityConfigurations.Title
{
    public class TitleFavoriteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.TitleFavorite>
    {
        public override void Configure(EntityTypeBuilder<TitleFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("titlefavorite", DictionaryTemplateContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.Title)
                .WithMany(i => i.TitleFavorites)
                .HasForeignKey(i => i.TitleId);

            builder.HasOne(i => i.CreatedUser)
                .WithMany(i => i.TitleFavorites)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
