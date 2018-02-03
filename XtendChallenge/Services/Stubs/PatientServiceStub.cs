using System;
using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services.Stubs
{
    public class PatientServiceStub : IPatientService
    {
        public List<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }
    }
}
