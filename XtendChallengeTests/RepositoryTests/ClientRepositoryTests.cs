using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using XtendChallenge.Models;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services.Formatters;

namespace XtendChallengeTests.RepositoryTests
{
    [TestClass]
    public class ClientRepositoryTests
    {
        private IClientRepository clientRepository = new ClientRepositoryInMem();

        [TestInitialize]
        public void Setup()
        {
            Cleanup();
            clientRepository.AddClient(TestHelper.GenerateClient(1));
            clientRepository.AddClient(TestHelper.GenerateClient(6));
        }

        [TestCleanup]
        public void Cleanup()
        {
            var clients = clientRepository.GetAllClients();
            foreach (var client in clients)
            {
                clientRepository.DeleteClient(client.Id);
            }
        }

        [TestMethod]
        public void TestGetAllClient()
        {
            // Arrange
            var expectedClients = new List<Client>
            {
                TestHelper.GenerateClient(1),
                TestHelper.GenerateClient(6)
            };

            // Act
            var actualClients = clientRepository.GetAllClients();

            // Assert
            CollectionAssert.AreEqual(expectedClients.OrderBy(client => client.Id).ToList(), actualClients.OrderBy(client => client.Id).ToList());
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

            clientRepository.AddClient(expectedClient);

            // Act
            var actualClient = clientRepository.GetClientById(expectedClient.Id);

            // Assert
            Assert.AreEqual(expectedClient, actualClient);
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
            var actualClient = clientRepository.AddClient(expectedClient);

            // Assert
            Assert.AreEqual(expectedClient, actualClient);
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
            clientRepository.AddClient(expectedClient);

            // Act
            var actualClient = clientRepository.DeleteClient(66);

            // Assert
            CollectionAssert.DoesNotContain(clientRepository.GetAllClients(), actualClient);
        }
    }
}
