using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreModel.Entity
{
    public record Blog
    {
        [Key]
        public int BId { get; set; }
        public string BName { get; set; }

        public string BDescription { get; set; } = string.Empty;

        public string? Content { get; set; }

    }
}
