using FluentValidation;
using SNSYS.Challenger.Api.Contracts;

namespace SNSYS.Challenger.Api.Validators
{
    public class FilterCustomerSupplierRequestValidator : AbstractValidator<FilterCustomerSupplierRequest>
    {
        public FilterCustomerSupplierRequestValidator()
        {
            RuleFor(filterCustomerSupplierRequest => filterCustomerSupplierRequest.pageIndex)
                .NotNull().NotEmpty().WithMessage("PageIndex cannot be null or empty");

            RuleFor(filterCustomerSupplierRequest => filterCustomerSupplierRequest.pageSize)
                .NotNull().NotEmpty().WithMessage("PageSize cannot be null or empty");


            When(t => t.pageIndex != null, () =>
            {
                RuleFor(t => t)
                    .Must(t =>
                     ValidatePage(t.pageIndex))
                    .WithMessage("PageIndex must have a value greater than zero");
            });

            When(t => t.pageSize != null, () =>
            {
                RuleFor(t => t)
                    .Must(t =>
                     ValidatePage(t.pageSize))
                    .WithMessage("PageSize must have a value greater than zero");
            });
        }


        private bool ValidatePage(int pageIndex)
        {
            return pageIndex > 0;

        }
    }
}
