using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Core.DomainObjects
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
