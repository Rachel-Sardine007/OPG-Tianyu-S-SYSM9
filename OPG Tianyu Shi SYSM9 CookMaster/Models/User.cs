using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // created automatic once && wont affect recipe list filter after updating user info
        public string Username { get; set; }
        public string Password { get; set; }
        public CountryItem Country { get; set; }

        //list<recipe> recipelist 
    }
}
