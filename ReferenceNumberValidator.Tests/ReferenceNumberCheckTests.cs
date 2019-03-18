using Xunit;

namespace ReferenceNumberValidator.Tests
{
    public class ReferenceNumberCheckTests
    {
        [Fact]
        public void Valid_Reference_Number()
        {
            var validReferenceNumber = "12304561";
            Assert.True(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(validReferenceNumber).IsValid);
        }

        [Fact]
        public void Reference_Number_Can_Have_Spaces()
        {
            string validReferenceNumber = "12304 561";
            Assert.True(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(validReferenceNumber).IsValid);
        }

        [Fact]
        public void Invalid_Reference_CheckSum()
        {
            var invalidCheckSum = "12304562";
            Assert.False(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(invalidCheckSum).IsValid);
        }

        [Fact]
        public void Too_Long_Reference_Number()
        {
            var longReferenceNumber = "123456789012345678901";
            Assert.False(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(longReferenceNumber).IsValid);
        }

        [Fact]
        public void Too_Short_Reference_Number()
        {
            var shortReferenceNumber = "123";
            Assert.False(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(shortReferenceNumber).IsValid);
        }

        [Fact]
        public void Non_Numeric_Reference_Number()
        {
            var nonDigitReferenceNumber = "A12304561";

            var result = FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(nonDigitReferenceNumber);

            Assert.False(result.IsValid);
            Assert.Contains("only numeric values", result.ValidationMessage);
        }

        [Fact]
        public void Reference_Number_Cannot_Be_Negative()
        {
            var negativeReferenceNumber = "-12304561";
            Assert.False(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(negativeReferenceNumber).IsValid);
        }

        [Fact]
        public void Reference_Number_Cannot_Have_Decimals()
        {
            var referenceNumberWithDecimals = "1230456,1";
            Assert.False(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(referenceNumberWithDecimals).IsValid);
        }

        [Fact]
        public void Reference_Number_Cannot_Be_Null()
        {
            string nullReferenceNumber = null;
            Assert.False(FinnishReferenceNumberValidator.ReferenceNumberValidator.IsValidFinnishReference(nullReferenceNumber).IsValid);
        }
    }
}
