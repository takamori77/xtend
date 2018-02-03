using System.Collections.Generic;
using System.Linq;

namespace XtendChallenge.Models.Repositories
{
    public class FacilityRepositoryInMem : IFacilityRepository
    {
        private Dictionary<int, Facility> facilities = new Dictionary<int, Facility>();

        public Facility AddFacility(Facility facility)
        {
            facilities.Add(facility.Id, facility);
            return facilities[facility.Id];
        }

        public void DeleteFacility(int id) => facilities.Remove(id);

        public List<Facility> GetAllFacilities() => facilities.Values.ToList();

        public Facility GetFacilityById(int id) => facilities[id];
    }
}
