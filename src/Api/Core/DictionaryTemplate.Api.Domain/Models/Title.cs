using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTemplate.Api.Domain.Models
{
    public class Title : BaseEntity
    {
        public string Subject { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public Guid CreatedById { get; set; }

        public virtual User? CreatedBy { get; set; }

        public virtual ICollection<Entry>? Entries { get; set; }

        public virtual ICollection<TitleVote>? TitleVotes { get; set; }

        public virtual ICollection<TitleFavorite>? TitleFavorites { get; set; }

    }
}
