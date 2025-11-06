using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Ingredients {  get; set; }
        public string Instructions {  get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; } // for display
        //public Guid OwnerId { get; set; } // for filtering
    }
}
