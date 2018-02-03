using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services;
using XtendChallenge.Services.Interfaces;

namespace XtendChallengeTests.RepositoryTests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private IFacilityService facilityService;
        private IAccountRepository accountRepository;
        private IClientRepository clientRepository = new ClientRepositoryInMem();

        public AccountRepositoryTests()
        {
            var facilityRepository = new FacilityRepositoryInMem();
            facilityService = new FacilityService(facilityRepository);
            accountRepository = new AccountRepositoryInMem(new ClientRepositoryInMem(), facilityService);
        }

        [TestInitialize]
        public void Setup()
        {
            TestHelper.CleanUpData(accountRepository, facilityService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestHelper.CleanUpData(accountRepository, facilityService);
        }

        [TestMethod]
        public void TestGetAllAccounts()
        {
            // Arrange
            var expectedAccounts = TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);

            // Act
            var actualAccounts = accountRepository.GetAllAccounts();

            // Assert
            CollectionAssert.AreEqual(expectedAccounts.OrderBy(account => account.Id).ToList(), actualAccounts.OrderBy(account => account.Id).ToList());
        }

        [TestMethod]
        public void TestGetAccountById()
        {
            // Arrange
            var expectedAccounts = TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);
            var expectedAccount = expectedAccounts[1];

            // Act
            var actualAccount = accountRepository.GetAccountById(1);

            // Assert
            Assert.AreEqual(expectedAccount, actualAccount);
        }


        [TestMethod]
        public void TestAddAccount()
        {
            // Arrange
            TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);
            var facility = TestHelper.GenerateFacility(66);
            facilityService.AddFacility(facility);
            var expectedAccount = TestHelper.GenerateAccount(66, facilityService, clientRepository);
            accountRepository.AddAccount(expectedAccount);

            // Act
            var actualAccount = accountRepository.AddAccount(expectedAccount);

            // Assert
            var expectedAccounts = accountRepository.GetAllAccounts();
            Assert.AreEqual(expectedAccount, actualAccount);
            Assert.IsTrue(expectedAccounts.Contains(expectedAccount));
        }

        [TestMethod]
        public void TestDeleteAccount()
        {
            // Arrange
            var expectedAccounts = TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);
            var facility = TestHelper.GenerateFacility(66);
            facilityService.AddFacility(facility);
            var expectedAccount = TestHelper.GenerateAccount(66, facilityService, clientRepository);
            accountRepository.AddAccount(expectedAccount);

            // Act
            var actualAccount = accountRepository.DeleteAccount(66);

            // Assert
            Assert.IsFalse(expectedAccounts.Contains(expectedAccount));
        }

        [TestMethod]
        public void TestGetAllAccountsByFacility()
        {
            // Arrange
            var expectedAccounts = TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);
            expectedAccounts = expectedAccounts.Where(a => a.Facility.Id == 1).ToList(); ;

            // Act
            var actualAccounts = accountRepository.GetAllAccountsByFacility(facilityService.GetFacilityById(1));

            // Assert
            CollectionAssert.AreEqual(expectedAccounts.OrderBy(account => account.Id).ToList(), actualAccounts.OrderBy(account => account.Id).ToList());
        }

        [TestMethod]
        public void TestGetAllAccountsByClient()
        {
            // Arrange
            var expectedAccounts = TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);
            expectedAccounts = expectedAccounts.Where(a => a.Client.Id == 1).ToList(); ;

            // Act
            var actualAccounts = accountRepository.GetAllAccountsByClient(clientRepository.GetClientById(1));

            // Assert
            CollectionAssert.AreEqual(expectedAccounts.OrderBy(account => account.Id).ToList(), actualAccounts.OrderBy(account => account.Id).ToList());
        }

        [TestMethod]
        public void TestGetAllAccountsByFacilityForClient()
        {
            // Arrange
            var expectedAccounts = TestHelper.SetupAccounts(accountRepository, facilityService, clientRepository);
            expectedAccounts = expectedAccounts.Where(a => a.Facility.Id == 1 && a.Client.Id == 1).ToList(); ;

            // Act
            var actualAccounts = accountRepository.GetAllAccountsByFacilityForClient(facilityService.GetFacilityById(1), clientRepository.GetClientById(1));

            // Assert
            CollectionAssert.AreEqual(expectedAccounts.OrderBy(account => account.Id).ToList(), actualAccounts.OrderBy(account => account.Id).ToList());
        }
    }
}
