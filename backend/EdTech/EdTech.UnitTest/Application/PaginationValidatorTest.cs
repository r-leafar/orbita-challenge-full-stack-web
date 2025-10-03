using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.UnitTest.Application
{
    public class PaginationValidatorTest
    {
        [Fact]
        public void Validate_WithValidParameters_ShouldNotThrowException()
        {
            var exception = Record.Exception(() => EdTech.Application.Validation.PaginationValidator.Validate(1, 10));
            Assert.Null(exception);
        }
        [Fact]
        public void Validate_WithPageLessThanOrEqualToZero_ShouldThrowApplicationException()
        {
            var exception = Assert.Throws<EdTech.Application.Exceptions.ApplicationException>(() => EdTech.Application.Validation.PaginationValidator.Validate(0, 10));
            Assert.Equal("page (Parâmetro: O número da página deve ser maior que zero.)", exception.Message);

        }
        [Fact]
        public void Validate_WithPageSizeLessThanOrEqualToZero_ShouldThrowApplicationException()
        {
            var exception = Assert.Throws<EdTech.Application.Exceptions.ApplicationException>(() => EdTech.Application.Validation.PaginationValidator.Validate(1, 0));
            Assert.Equal("pageSize (Parâmetro: O tamanho da pagina deve ser maior que zero.)", exception.Message);
        }
    }
}
