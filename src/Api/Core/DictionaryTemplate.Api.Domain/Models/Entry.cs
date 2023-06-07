using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTemplate.Api.Domain.Models
{
    public class Entry : BaseEntity
    {
        public string Content { get; set; } = string.Empty;

        public Guid CreatedById { get; set; }

        public Guid TitleId { get; set; }

        public virtual Title? Title { get; set; }

        public virtual User? CreatedBy { get; set; }

        public virtual ICollection<EntryVote>? EntryVotes { get; set; }

        public virtual ICollection<EntryFavorite>? EntryFavorites { get; set; }
    }
}
