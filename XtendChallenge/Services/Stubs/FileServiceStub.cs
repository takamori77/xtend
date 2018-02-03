using System.Collections.Generic;
using XtendChallenge.Models;
using XtendChallenge.Services.Formatters;
using XtendChallenge.Services.Interfaces;

namespace XtendChallenge.Services.Stubs
{
    public class FileServiceStub : IFileService
    {
        ExportFile exportFile = new ExportFile
        {
            FileName = "filename",
            Content = "filecontent"
        };

        public ExportFile GetExportFile(CommaFormatter fileFormatter, List<Facility> facilities)
        {
            return exportFile;
        }

        public ExportFile GetExportFile(PipeFormatter fileFormatter, List<Account> accounts, Client client)
        {
            return exportFile;
        }

        public List<ExportFile> GetExportFile(Client client)
        {
            return new List<ExportFile>();
        }

        public List<ExportFile> GetExportFiles(CommaFormatter fileFormatter, List<Facility> facilities)
        {
            return new List<ExportFile>();
        }

        public List<ExportFile> GetExportFiles(CommaFormatter fileFormatter, List<Facility> facilities, Client client)
        {
            return new List<ExportFile>();
        }
    }
}
