using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Interfaces
{
    public interface IFacilityService
    {
        List<Facility> GetAllFacilities();
        Facility AddFacility(Facility facility);
        void DeleteFacility(int id);
        Facility GetFacilityById(int x);
    }
}
