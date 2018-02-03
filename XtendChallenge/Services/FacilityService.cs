using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services
{
    public class FacilityService : IFacilityService
    {
        private IFacilityRepository facilityRepository;

        public FacilityService(IFacilityRepository facilityRepository) => this.facilityRepository = facilityRepository;

        public Facility AddFacility(Facility facility) => facilityRepository.AddFacility(facility);

        public void DeleteFacility(int id) => facilityRepository.DeleteFacility(id);

        public List<Facility> GetAllFacilities() => facilityRepository.GetAllFacilities();

        public Facility GetFacilityById(int id) => facilityRepository.GetFacilityById(id);
    }
}
