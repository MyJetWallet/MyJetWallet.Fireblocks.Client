using Autofac;
using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public class CommonExtensionsTests 
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData("hello7of,")]
        [InlineData("$%^&*@")]
        [InlineData("123 1")]
        [InlineData("-123 1")]
        public void ValidValueTryStringToConvertTest(string? value)
        {
            Assert.Equal(0m, value.TryStringToConvert());            
        }

        [Theory]
        [InlineData("123")]
        [InlineData("123,1")]
        [InlineData("123.1")]
        [InlineData("-123")]
        [InlineData("-123,1")]
        [InlineData("-123.1")]
        public void NotValidTryStringToConvertTest(string? value)
        {
            Assert.NotEqual(0m, value.TryStringToConvert());
        }

        [Theory]
        [InlineData(123.11)]
        [InlineData(-123.11)]
        [InlineData(123)]
        [InlineData(-123)]
        public void ValidTryConvertToString(decimal value)
        {
            Assert.NotNull(value.TryConvertToString());
            Assert.NotEmpty(value.TryConvertToString());
        }

        [Theory]
        [InlineData(0)]
        public void NotValidTryConvertToString(decimal value)
        {
            Assert.NotNull(value.TryConvertToString());
            Assert.NotEmpty(value.TryConvertToString());
            Assert.Equal("0", value.TryConvertToString());
        }
    }
}
