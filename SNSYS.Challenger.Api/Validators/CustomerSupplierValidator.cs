using FluentValidation;
using SNSYS.Challenger.Api.Contracts;

namespace SNSYS.Challenger.Api.Validators
{
    public class CustomerSupplierValidator : AbstractValidator<CustomerSupplierRequest>
    {
        public CustomerSupplierValidator() 
        {

            RuleFor(customerSupplierRequest => customerSupplierRequest.Name)
                .NotNull().NotEmpty().WithMessage("Customer or supplier name field cannot be empty")
                .MaximumLength(50).WithMessage("Customer or supplier name not incorrect.Maximum size allowed 50");
            RuleFor(customerSupplierRequest => customerSupplierRequest.Type)
                .NotNull().NotEmpty().WithMessage("Type of customer/supplier not specified or incorrect");
            RuleFor(customerSupplierRequest => customerSupplierRequest.DocumentNumber)
                .NotNull().NotEmpty().WithMessage("Customer or supplier document number field cannot be empty")
                .MaximumLength(25).WithMessage("Customer or supplier document number not provided or incorrect.Maximum size allowed 25");



        }
    }
}
