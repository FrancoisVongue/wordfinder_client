using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WordFinder_Business;
using WordFinder_Repository;

namespace WordFinder_BusinessTests
{
    public class PasswordHandlerTests
    {
        private DbContextOptions<ApiContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: "Add_to_database")
                .Options;
            
        }

        [TestCase("sadfsdafsadf","joasdkffpoif", false)]
        [TestCase("32v4234v23klh","32v4234v23klh", true)]
        [TestCase("b35btdxbber","09ujdfsd987fy", false)]
        [TestCase("4b5345b34fdgd435b","4b5345b34fdGd435b", true)] // ignorecase test
        public void VerifyPassword_WhenCalledWithSameHash_ReturnsTrue(string s1, string s2, bool shouldPass)
        {
            var result = PasswordHandler.verifyPassword(s1, s2);
            Assert.That(result, Is.EqualTo(shouldPass));
        }
        
        [TestCase(6)]
        [TestCase(1)]
        [TestCase(9)]
        public void getSalt_WhenCalledWithNumber_ShouldReturnRandomStringOfGivenLength(int length)
        {
            var result = PasswordHandler.getSalt(length);
            Assert.That(result.Length, Is.EqualTo(length));
        }
        
        [TestCase("something")]
        [TestCase("allohomora")]
        [TestCase("tomorrow")]
        public void getHashed_WhenCalledWithString_ShouldReturnHashedVersion(string s)
        {
            var result = PasswordHandler.getHashed(s);
            Assert.That(result, Is.Not.EqualTo(s));
            Assert.That(result.Length, Is.EqualTo(64));
        }
    }
}