using FluentValidation;
using DataAccessLayer.Model;

namespace BusinessLogicLayer.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.Id).NotNull();
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x=> x.Description).NotEmpty();
            RuleFor(x=>x.Price).NotEmpty();
        }
    }
}
