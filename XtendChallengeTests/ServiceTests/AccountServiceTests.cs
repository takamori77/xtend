using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtendChallenge.Models.Repositories;
using XtendChallenge.Services;
using XtendChallenge.Services.Interfaces;

namespace XtendChallengeTests.ServiceTests
{
    [TestClass]
    public class AccountServiceTests
    {
        IAccountService accountService;
        IFacilityService facilityService;
        IClientRepository clientRepository = new ClientRepositoryInMem();

        public AccountServiceTests()
        {
            var facilityRepository = new FacilityRepositoryInMem();
            facilityService = new FacilityService(facilityRepository);
            var accountRepository = new AccountRepositoryInMem();
            accountService = new AccountService(accountRepository);
        }


        [TestInitialize]
        public void Setup()
        {
            TestHelper.CleanUpData(accountService, facilityService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestHelper.CleanUpData(accountService, facilityService);
        }

        [TestMethod]
        public void TestGetAllAccounts()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);

            // Act
            var actualAccounts = accountService.GetAllAccounts();

            // Assert
            Assert.IsNotNull(actualAccounts);
        }

        [TestMethod]
        public void TestGetAccountById()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);
            facilityService.AddFacility(TestHelper.GenerateFacility(66));
            var expectedAccount = TestHelper.GenerateAccount(66, facilityService, clientRepository);
            accountService.AddAccount(expectedAccount);

            // Act
            var actualAccount = accountService.GetAccountById(66);

            // Assert
            Assert.IsNotNull(actualAccount);
        }


        [TestMethod]
        public void TestAddAccount()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);
            facilityService.AddFacility(TestHelper.GenerateFacility(66));
            var expectedAccount = TestHelper.GenerateAccount(66, facilityService, clientRepository);

            // Act
            var actualAccount = accountService.AddAccount(expectedAccount);

            // Assert
            Assert.IsNotNull(actualAccount);
        }

        [TestMethod]
        public void TestDeleteAccount()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);
            facilityService.AddFacility(TestHelper.GenerateFacility(66));
            var expectedAccount = TestHelper.GenerateAccount(66, facilityService, clientRepository);
            accountService.AddAccount(expectedAccount);

            // Act
            var actualAccount = accountService.DeleteAccount(66);

            // Assert
            Assert.IsNotNull(actualAccount);
        }

        [TestMethod]
        public void TestGetAllAccountsByClient()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);

            // Act
            var actualAccounts = accountService.GetAllAccountsByClient(clientRepository.GetClientById(1));

            // Assert
            Assert.IsNotNull(actualAccounts);
        }

        [TestMethod]
        public void TestGetAllAccountsByFacility()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);

            // Act
            var actualAccounts = accountService.GetAllAccountsByFacility(facilityService.GetFacilityById(0));

            // Assert
            Assert.IsNotNull(actualAccounts);
        }

        [TestMethod]
        public void TestGetAllAccountsByFacilityForClient()
        {
            // Arrange
            TestHelper.SetupAccounts(accountService, facilityService, clientRepository);

            // Act
            var actualAccounts = accountService.GetAllAccountsByFacilityForClient(facilityService.GetFacilityById(0),clientRepository.GetClientById(1));

            // Assert
            Assert.IsNotNull(actualAccounts);
        }
    }
}
