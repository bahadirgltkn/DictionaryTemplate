using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTemplate.Api.Domain.Models
{
    public class TitleFavorite : BaseEntity
    {
        public Guid TitleId { get; set; }
        public Guid CreatedById { get; set; }
        public virtual Title? Title { get; set; }
        public virtual User? CreatedUser { get; set; }
    }
}
