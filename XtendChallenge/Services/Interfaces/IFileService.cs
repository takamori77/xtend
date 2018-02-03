using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Services.Formatters;

namespace XtendChallenge.Services.Interfaces
{
    public interface IFileService
    {
        ExportFile GetExportFile(PipeFormatter fileFormatter, List<Account> accounts, Client client);
        List<ExportFile> GetExportFiles(CommaFormatter fileFormatter, List<Facility> facilities, Client client);
        List<ExportFile> GetExportFile(Client client);
    }
}
