using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Identity
{
    public class User : IdentityUser<long>
    {

    
        public DateTime CreationTime { get; set; }
        public string? NickName { get; set; }

        public static implicit operator Task<object>(User v)
        {
            throw new NotImplementedException();
        }
    }

}
