using System.Collections.Generic;
using XtendChallenge.Models;

namespace XtendChallenge.Services.Interfaces
{
    public interface IPatientService
    {
        List<Patient> GetAllPatients();
    }
}
