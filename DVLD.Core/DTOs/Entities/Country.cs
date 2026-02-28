using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DVLD.Data")]

namespace DVLD.Core.DTOs.Entities
{
    public class Country
    {
        public int CountryID { get; private set; }

        public string CountryName { get; set; }

        public Country(string countryName)
        {
            CountryID = -1;
            CountryName = countryName;
        }

        internal Country(int id, string countryName)
        {
            CountryID = id;
            CountryName = countryName;
        }
    }
}
