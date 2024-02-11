using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Application.Contracts;
using SNSYS.Challenger.Application.Services.Interfaces;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;


namespace SNSYS.Challenger.Api.Controllers
{
    [ApiController]
    [Route("snsys/api/customersupplier")]
    [Authorize("Bearer")]
    public class CustomerSupplierController : Controller
    {
 
        public ICustomerSupplierTransactionService _customerSupplierTransactionService;
        private readonly IValidator<CustomerSupplierRequest> _validator;
        private readonly IValidator<FilterCustomerSupplierRequest> _validatorFilter;
        private readonly IMapper _mapper;

        public CustomerSupplierController(IMapper mapper,
            IValidator<CustomerSupplierRequest> validator, ICustomerSupplierTransactionService customerSupplierTransactionService, 
            IValidator<FilterCustomerSupplierRequest> validatorFilter)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _customerSupplierTransactionService = customerSupplierTransactionService ?? throw new ArgumentNullException(nameof(customerSupplierTransactionService));
            _validatorFilter = validatorFilter ?? throw new ArgumentNullException(nameof(validatorFilter)); ;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterCustomerSupplierRequest filterCustomerSupplierRequest)
        {
            var validationResult = await _validatorFilter.ValidateAsync(filterCustomerSupplierRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var filterCustomerSupplierMap = _mapper.Map<FilterCustomerSupplierRequest,FilterCustomerSupplier>(filterCustomerSupplierRequest);

            var customerSupplierList = await _customerSupplierTransactionService.GetAllAsync(filterCustomerSupplierMap);

            if (customerSupplierList != null)
            {
                var customerSupplierListMap = _mapper.Map<IEnumerable<CustomerSupplierData>, IEnumerable<CustomerSupplierResponse>>(customerSupplierList);

                return Ok(customerSupplierListMap);

            }

            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerSupplierRequest customerSupplierRequest)
        {

            var validationResult = await _validator.ValidateAsync(customerSupplierRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }



            var customerSupplierModel = _mapper.Map<CustomerSupplierRequest, CustomerSupplierModel>(customerSupplierRequest);

            await _customerSupplierTransactionService.CreateAsync(customerSupplierModel);

            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> Put(CustomerSupplierRequest customerSupplierRequest)
        {

            var validationResult = await _validator.ValidateAsync(customerSupplierRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var customerSupplierValidate = await _customerSupplierTransactionService.GetByIdAsync(customerSupplierRequest.Id.Value);

            if (customerSupplierValidate == null)
            {
                return NotFound();
            }

            var customerSupplierModel = _mapper.Map<CustomerSupplierRequest, CustomerSupplierModel>(customerSupplierRequest);

            await _customerSupplierTransactionService.UpdateAsync(customerSupplierModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerSupplier = await _customerSupplierTransactionService.GetByIdAsync(id);

            if (customerSupplier == null)
            {
                return NotFound();
            }

            await _customerSupplierTransactionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
