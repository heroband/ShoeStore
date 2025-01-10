using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShoeStore.Models.Entities
{
    public class User : IdentityUser
    {
        public string Theme { get; set; } = "light";
    }
}
