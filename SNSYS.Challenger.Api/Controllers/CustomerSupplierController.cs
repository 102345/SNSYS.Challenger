using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SNSYS.Challenger.Api.Contracts;
using SNSYS.Challenger.Api.Validators;
using SNSYS.Challenger.Domain.Entities;
using SNSYS.Challenger.Domain.Filter;
using SNSYS.Challenger.Domain.Services.Interfaces;

namespace SNSYS.Challenger.Api.Controllers
{
    [ApiController]
    [Route("snsys/api/customersupplier")]
    //[Authorize("Bearer")]
    public class CustomerSupplierController : Controller
    {
        public ICustomerSupplierService _customerSupplierService;
        private readonly IMapper _mapper;

        public CustomerSupplierController(ICustomerSupplierService customerSupplierService, IMapper mapper)
        {
            _customerSupplierService = customerSupplierService ?? throw new ArgumentNullException(nameof(customerSupplierService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterCustomerSupplierRequest filterCustomerSupplierRequest)
        {
            
            var filterCustomerSupplierMap = _mapper.Map<FilterCustomerSupplierRequest,FilterCustomerSupplier>(filterCustomerSupplierRequest);

            var customerSupplierList = await _customerSupplierService.GetAllAsync(filterCustomerSupplierMap);

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

            var validator = new CustomerSupplierValidator();
            var validationResult = validator.Validate(customerSupplierRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }



            var customerSupplier = _mapper.Map<CustomerSupplierRequest, CustomerSupplier>(customerSupplierRequest);

            var ret = _customerSupplierService.CreateAsync(customerSupplier);

            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> Put(CustomerSupplierRequest customerSupplierRequest)
        {
            var customerSupplierValidate = await _customerSupplierService.GetByIdAsync(customerSupplierRequest.Id.Value);

            if (customerSupplierValidate == null)
            {
                return NotFound();
            }

            var customerSupplier = _mapper.Map<CustomerSupplierRequest, CustomerSupplier>(customerSupplierRequest);

            await _customerSupplierService.UpdateAsync(customerSupplier);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerSupplier = await _customerSupplierService.GetByIdAsync(id);

            if (customerSupplier == null)
            {
                return NotFound();
            }

            await _customerSupplierService.DeleteAsync(id);

            return NoContent();
        }
    }
}
