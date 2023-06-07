using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTemplate.Api.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } = false;
        public virtual ICollection<Title>? Titles { get; set; }
        public virtual ICollection<TitleFavorite>? TitleFavorites { get; set; }
        public virtual ICollection<Entry>? Entries { get; set; }
        public virtual ICollection<EntryFavorite>? EntryFavorites { get; set; }

    }
}
