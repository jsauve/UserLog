using System;
using System.Linq;
using com.spectrum.UserLog.Core;
using Xunit;

namespace com.spectrum.UserLog.Tests
{
    public class PasswordPatternValidationServiceTests
    {
        [Fact]
        public void PasswordLengthLessThenMinimum()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "Tige";

            //act
            var result = service.Validate(password);

            //assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Reasons, x => x.SequenceEqual(PasswordPatternValidationService.Messages.Length));
        }

        [Fact]
        public void PasswordLengthGreaterThanMaximum()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerBalm1234";

            //act
            var result = service.Validate(password);

            //assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Reasons, x => x.SequenceEqual(PasswordPatternValidationService.Messages.Length));
        }

        [Fact]
        public void PasswordLengthWithinRange()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerBalm123";

            //act
            var result = service.Validate(password);

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void PasswordContainsIdenticalConsecutiveSequences()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerTiger";

            //act
            var result = service.Validate(password);

            //assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Reasons, x => x.SequenceEqual(PasswordPatternValidationService.Messages.IdenticalConsecutiveSequence));
        }

        [Fact]
        public void PasswordDoesNotContainIdenticalConsecutiveSequences()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerBalm123";

            //act
            var result = service.Validate(password);

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void PasswordIsAlphanumericOnly()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerBalm123";

            //act
            var result = service.Validate(password);

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void PasswordIsNotAlphanumericOnly()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerBalm12!";

            //act
            var result = service.Validate(password);

            //assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void PasswordMeetsAllCriteria()
        {
            // arrange
            var service = new PasswordPatternValidationService();
            var password = "TigerBalm123";

            //act
            var result = service.Validate(password);

            //assert
            Assert.True(result.IsValid);
        }
    }
}
