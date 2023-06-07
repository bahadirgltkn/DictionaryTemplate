using DictionaryTemplate.Api.Domain.Models;
using DictionaryTemplate.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryTemplate.Infrastructure.Persistance.EntityConfigurations.Title
{
    public class TitleVoteEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.TitleVote>   
    {
        public override void Configure(EntityTypeBuilder<TitleVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("titlevote", DictionaryTemplateContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.Title)
                .WithMany(i => i.TitleVotes)
                .HasForeignKey(i => i.TitleId);
        }
    }
}
