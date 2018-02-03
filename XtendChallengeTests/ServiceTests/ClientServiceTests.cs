using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services;
using XtendChallenge.Services.Formatters;
using XtendChallenge.Services.Interfaces;

namespace XtendChallengeTests.ServiceTests
{
    [TestClass]
    public class ClientServiceTests
    {
        private IClientService clientService;

        public ClientServiceTests()
        {
            var clientRepository = new ClientRepositoryInMem();
            clientService = new ClientService(clientRepository);
        }


        [TestInitialize]
        public void Setup()
        {
            Cleanup();
            clientService.AddClient(TestHelper.GenerateClient(1));
            clientService.AddClient(TestHelper.GenerateClient(6));
        }

        [TestCleanup]
        public void Cleanup()
        {
            var clients = clientService.GetAllClients();
            foreach (var client in clients)
            {
                clientService.DeleteClient(client.Id);
            }
        }

        [TestMethod]
        public void TestGetAllClient()
        {
            // Arrange

            // Act
            var actualClients = clientService.GetAllClients();

            // Assert
            Assert.IsNotNull(actualClients);
        }

        [TestMethod]
        public void TestGetClientById()
        {
            // Arrange

            var expectedClient = new Client
            {
                Abbreviation = "nc",
                BalanceThreshold = 0.0m,
                FormatterType = FormatterType.CSV,
                Name = "New Client",
                Id = 66
            };

            clientService.AddClient(expectedClient);

            // Act
            var actualClient = clientService.GetClientById(expectedClient.Id);

            // Assert
            Assert.IsNotNull(actualClient);
        }


        [TestMethod]
        public void TestAddClient()
        {
            // Arrange
            var expectedClient = new Client
            {
                Abbreviation = "nc",
                BalanceThreshold = 0.0m,
                FormatterType = FormatterType.CSV,
                Name = "New Client",
                Id = 66
            };


            // Act
            var actualClient = clientService.AddClient(expectedClient);

            // Assert
            Assert.IsNotNull(actualClient);
        }

        [TestMethod]
        public void TestDeleteClient()
        {
            // Arrange
            var expectedClient = new Client
            {
                Abbreviation = "nc",
                BalanceThreshold = 0.0m,
                FormatterType = FormatterType.CSV,
                Name = "New Client",
                Id = 66
            };
            clientService.AddClient(expectedClient);

            // Act
            var actualClient = clientService.DeleteClient(66);

            // Assert
            Assert.IsNotNull(actualClient);
        }
    }
}
