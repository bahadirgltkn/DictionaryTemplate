using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTemplate.Api.Domain.Models
{
    public class EmailConfirmation : BaseEntity
    {
        public string OldEmailAddress { get; set; } = string.Empty;
        public string NewEmailAddress { get; set; } = string.Empty;
    }
}
