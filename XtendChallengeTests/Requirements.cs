using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services;
using XtendChallenge.Services.Formatters;
using XtendChallenge.Services.Interfaces;

namespace XtendChallengeTests
{
    [TestClass]
    public class Requirements
    {
        private IAccountService accountService;
        private IFileService fileService;
        private IFacilityService facilityService;
        private IClientRepository clientRepository = new ClientRepositoryInMem();
        private IClientService clientService;

        [TestInitialize]
        public void Setup()
        {
            var facilityRepository = new FacilityRepositoryInMem();
            facilityService = new FacilityService(facilityRepository);
            var accountRepository = new AccountRepositoryInMem();
            accountService = new AccountService(accountRepository);
            fileService = new FileService(accountService, facilityService);
            clientService = new ClientService(clientRepository);
            TestHelper.CleanUpData(accountService, facilityService, clientService);
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestHelper.CleanUpData(accountService, facilityService, clientService);
        }

        [TestMethod]
        public void ICanExporPipeFormat1()
        {
            // Arrange
            var fileFormatter = new PipeFormatter();
            var client = accountService.GetAllAccounts().Where(a => a.Client.FormatterType == FormatterType.Pipe).FirstOrDefault().Client;
            var expectedExportFile = TestHelper.GeneratePipeExportFile(accountService, facilityService, fileFormatter, client, clientRepository, clientService);
            var accounts = accountService.GetAllAccountsByClient(client);

            // Act
            //var actualExportFile = fileService.GetExportFile(fileFormatter, accounts, client);
            var actualExportFile = fileService.GetExportFile(client);

            // Assert
            Assert.AreEqual(expectedExportFile, actualExportFile.FirstOrDefault());
        }

        [TestMethod]
        public void ICanExportCommaFormat1()
        {
            // Arrange
            var fileFormatter = new CommaFormatter();
            var client = accountService.GetAllAccounts().Where(a => a.Client.FormatterType == FormatterType.CSV).FirstOrDefault().Client;
            var facilities = facilityService.GetAllFacilities();
            var expectedExportFiles = TestHelper.GenerateCommaExportFiles(facilityService, accountService, fileFormatter, client);

            // Act
            // var actualExportFiles = fileService.GetExportFiles(fileFormatter, facilities);
            var actualExportFiles = fileService.GetExportFile(client);

            // Assert
            CollectionAssert.AreEqual(expectedExportFiles, actualExportFiles);
        }

        [TestMethod]
        public void CSVClientsProduceCSVFiles()
        {
            // Arrange
            var fileFormatter = new CommaFormatter();
            var client = accountService.GetAllAccounts().Where(a => a.Client.FormatterType == FormatterType.CSV).FirstOrDefault().Client;
            var facilities = facilityService.GetAllFacilities();

            var expectedExportFiles = TestHelper.GenerateCommaExportFiles(facilityService, accountService, fileFormatter, client);

            // Act
            // var actualExportFiles = fileService.GetExportFiles(fileFormatter, facilities);
            var actualExportFiles = fileService.GetExportFile(client);

            // Assert
            foreach (var file in actualExportFiles)
            {
                Assert.AreEqual("csv", file.FileName.Substring(file.FileName.Length - 3));
            }
        }

        [TestMethod]
        public void PipeClientsProducePipeFiles()
        {
            // Arrange
            var fileFormatter = new PipeFormatter();
            var accounts = accountService.GetAllAccounts();
            var client = accountService.GetAllAccounts().Where(a => a.Client.FormatterType == FormatterType.Pipe).FirstOrDefault().Client;
            var expectedExportFile = TestHelper.GeneratePipeExportFile(accountService, facilityService, fileFormatter, client, clientRepository, clientService);

            // Act
            var actualExportFile = fileService.GetExportFile(client);
            // var actualExportFile = fileService.GetExportFile(fileFormatter, accounts, client);
            foreach (var file in actualExportFile)
            {
                Assert.AreEqual("dat", file.FileName.Substring(file.FileName.Length - 3));
            }
        }

        [TestMethod]
        public void CSVClientsOnlyShowBalanceGreaterThanThreshold()
        {
            // Arrange
            var client = accountService.GetAllAccounts().Where(a => a.Client.FormatterType == FormatterType.CSV).FirstOrDefault().Client;

            // Act
            var actualExportFiles = fileService.GetExportFile(client);

            // Assert
            foreach (var file in actualExportFiles)
            {
                var lines = file.Content.Split("\n");
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    var match = Regex.Match(line, @"\d*\.\d{2}");
                    if (!match.Success)
                    {
                        Assert.Fail("No balance found");
                    }
                    if (!decimal.TryParse(match.Value, out decimal balance))
                    {
                        Assert.Fail("Balance parse fail");
                    }
                    if (balance <= client.BalanceThreshold)
                    {
                        Assert.Fail("Balance too low");
                    }
                }
            }
        }

        [TestMethod]
        public void PipeClientsOnlyGenerateASingleReport()
        {
            // Arrange
            var fileFormatter = new PipeFormatter();
            var accounts = accountService.GetAllAccounts();
            var client = accountService.GetAllAccounts().Where(a => a.Client.FormatterType == FormatterType.Pipe).FirstOrDefault().Client;
            var expectedExportFile = TestHelper.GeneratePipeExportFile(accountService, facilityService, fileFormatter, client, clientRepository, clientService);

            // Act
            var actualExportFiles = fileService.GetExportFile(client);
            Assert.AreEqual(1, actualExportFiles.Count);
        }

        [TestMethod]
        public void CSVClientsGenerateFilesForEachFacility()
        {

        }


    }
}
