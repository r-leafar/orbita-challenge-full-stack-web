using EdTech.Core.Exceptions;
using EdTech.Core.Shared.Ensure;

namespace EdTech.UnitTest.Core
{
    public class EnsureTest
    {

        [Fact]
        public void EnsureNotNullString_ForEmpty_WithSucess()
        {
                Ensure.NotNull("");
        }

        [Fact]
        public void EnsureNotNullString_ForNull_ShouldThrowException()
        {       
           var exception = Assert.Throws<DomainException>(() =>
            {
                Ensure.NotNull(null);
            });

            Assert.Equal("null (Parâmetro: O valor não pode ser nulo.)", exception.Message);
        }

        [Fact]
        public void EnsureNotNullOrWhiteSpace_ForEmpty_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                Ensure.NotNullOrWhiteSpace("");
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: \"\")", exception.Message);
        }

        [Fact]
        public void EnsureNotNullOrWhiteSpace_WithSucess()
        {
            Ensure.NotNullOrWhiteSpace("1");
        }

        [Fact]
        public void EnsureNotNullOrWhiteSpace_ForObject_WithSucess()
        {
            Ensure.NotNull(new Object());
        }
    }
}
