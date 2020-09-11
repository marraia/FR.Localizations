using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using System;
using Xunit;
using FluentAssertions;

namespace FR.Localizations.Test
{
    public class StringLocalizationTests
    {
        private IHostEnvironment subHostEnvironment;
        private IHttpContextAccessor subHttpContextAccessor;

        public StringLocalizationTests()
        {
            this.subHostEnvironment = Substitute.For<IHostEnvironment>();
            this.subHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        }

        private StringLocalization CreateStringLocalization()
        {
            return new StringLocalization(
                this.subHostEnvironment,
                this.subHttpContextAccessor);
        }

        private void Init()
        {
            this.subHttpContextAccessor.HttpContext.Request.Headers["Accept-Language"] = "pt-BR";
            this.subHostEnvironment.ContentRootPath = "../../../../FR.Localizations.Test";
        }

        [Fact]
        public void GetAllStrings_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Init();
            var stringLocalization = this.CreateStringLocalization();

            // Act
            var result = stringLocalization
                            .GetAllStrings();

            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .Should()
                .HaveCount(2);
        }

        [Fact]
        public void GetMessage_WithKey_Success()
        {
            // Arrange
            Init();
            var stringLocalization = this.CreateStringLocalization();
            var key = "CommitError";

            // Act
            var result = stringLocalization
                            .GetAllStrings();

            var message = stringLocalization[key].Value;

            // Assert
            result
                .Should()
                .NotBeNull();

            message
                .Should()
                .Be("Erro ao comitar!");
        }

        [Fact]
        public void GetMessage_WithKeyParameters_Success()
        {
            // Arrange
            Init();
            var stringLocalization = this.CreateStringLocalization();
            var key = "CommitSuccess";
            var param = "Teste";

            // Act
            var result = stringLocalization
                            .GetAllStrings();

            var message = stringLocalization[key, param].Value;

            // Assert
            result
                .Should()
                .NotBeNull();

            message
                .Should()
                .Be("Sucesso ao comitar Teste!");
        }

        [Fact]
        public void GetMessage_WithContextNull()
        {
            // Arrange

            // Act
            Action act = () =>
            {
                CreateStringLocalization();
            };

            // Assert
            act
            .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("Accept-Language not loaded");
        }
    }
}
