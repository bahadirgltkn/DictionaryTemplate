using DictinoaryTemplate.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTemplate.Api.Domain.Models
{
    public class TitleVote : BaseEntity
    {
        public Guid TitleId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedById { get; set; }
        public virtual Title? Title { get; set; }
    }
}
