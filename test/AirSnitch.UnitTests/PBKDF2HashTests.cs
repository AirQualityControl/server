using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Cryptography.Hashing;
using NUnit.Framework;

namespace AirSnitch.UnitTests
{
    [TestFixture]
    public class Pbkdf2HashTests
    {
        [Test]
        public void Generate_ValidNonEmptyString_ReturnsHashValue()
        {
            var targetString = "stringToHash";
            
            var hash = Pbkdf2Hash.Generate(targetString);
            
            Assert.AreEqual("IbcDRYz5zdyQGEv/ITX+Y+a9dLzOvtsrESysZBwBQTA=", hash);
        }

        [TestCase("stringToHash")]
        [TestCase("stringToHash")]
        [TestCase("stringToHash")]
        [TestCase("stringToHash")]
        [TestCase("stringToHash")]
        [TestCase("stringToHash")]
        public void Generate_ValidNonEmptyString_ReturnsAlwaysTheSameValue(string inputString)
        {
            var expectedHash = "IbcDRYz5zdyQGEv/ITX+Y+a9dLzOvtsrESysZBwBQTA=";

            var hashedResult = Pbkdf2Hash.Generate(inputString);
            
            Assert.AreEqual(expectedHash, hashedResult);

        }
    }
}