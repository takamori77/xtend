using System.Collections.Generic;

namespace XtendChallenge.Models.Repositories
{
    public interface IFacilityRepository
    {
        Facility AddFacility(Facility facility);
        void DeleteFacility(int id);
        List<Facility> GetAllFacilities();
        Facility GetFacilityById(int id);
    }
}
