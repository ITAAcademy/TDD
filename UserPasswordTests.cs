using NUnit.Framework;
using TDD.Password;

namespace TDD.Tests
{
    [TestFixture]
    public class UserPasswordTests
    {
        [Test]
        public void Password_PasswordLength5to10Symbols()
        {
            var testString = "Vs%est1";
            var userPassword = new UserPassword(testString);
            Assert.That(userPassword.IsValid);
            Assert.That(userPassword.Length, Is.EqualTo(testString.Length));
        }

        [Test]
        public void Password_PasswordLengthLessThenValid()
        {
            var userPassword = new UserPassword("ttt");
            Assert.That(!userPassword.IsValid);
        }

        [Test]
        public void Password_PasswordLengthExceedsValid()
        {
            var userPassword = new UserPassword("123456789123456789");
            Assert.That(!userPassword.IsValid);
        }

        [Test]
        public void Password_PasswordNullValue()
        {
            var userPassword = new UserPassword(null);
            Assert.That(!userPassword.IsValid);
        }

        [Test]
        public void Password_GeneratePassword()
        {
            var userPassword = new UserPassword();
            Assert.That(userPassword.IsValid);
        }
    }
}
