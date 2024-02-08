using FluentValidation;
using SNSYS.Challenger.Api.Contracts;

namespace SNSYS.Challenger.Api.Validators
{
    public class CustomerSupplierValidator : AbstractValidator<CustomerSupplierRequest>
    {
        public CustomerSupplierValidator() 
        {

            RuleFor(customerSupplierRequest => customerSupplierRequest.Name)
                .NotNull().NotEmpty().MaximumLength(50).WithMessage("Customer or supplier name not provided or incorrect");
            RuleFor(customerSupplierRequest => customerSupplierRequest.Type)
                .NotNull().NotEmpty().WithMessage("Type of customer/supplier not specified or incorrect");
            RuleFor(customerSupplierRequest => customerSupplierRequest.DocumentNumber)
                .NotNull().NotEmpty().MaximumLength(25).WithMessage("Customer or supplier doecument number not provided or incorrec");

        }
    }
}
