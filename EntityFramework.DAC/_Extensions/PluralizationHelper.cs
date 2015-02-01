using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DAC
{
    public static class PluralizationExtensions
    {
        private static PluralizationService _ps;

        private static PluralizationService PluralizationService
        {
            get
            {
                if (_ps == null)
                {
                    _ps = PluralizationService.CreateService(new CultureInfo("en-US"));
                }
                return _ps;
            }
        }


        public static string GetPlural(this string word)
        {
            return PluralizationService.Pluralize(word);
        }

        public static string GetSingular(this string word)
        {
            return PluralizationService.Singularize(word);
        }

    }
}
