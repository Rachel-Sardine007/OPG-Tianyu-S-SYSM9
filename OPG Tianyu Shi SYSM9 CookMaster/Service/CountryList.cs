using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Service
{
    public static class CountryService
    {
        private static readonly string countryFilePath = 
                "C:\\Mac\\Home\\Documents\\Csharp101\\Inluppg\\OPG Tianyu Shi SYSM9 CookMaster\\OPG Tianyu Shi SYSM9 CookMaster\\CountryList.xml";

        public static List<CountryItem> LoadCountryList()
        {
            var doc = XDocument.Load(countryFilePath);
            var countryList = from c in doc.Descendants("country")
                              select new CountryItem // using model instead of new items
                              {
                                  Name = c.Element("short_desc")?.Value,
                                  Code = c.Element("country_code")?.Value
                              };
            return countryList.ToList();
        }
    }
}
