using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Customers.Dtos.BaseDto;
using Project.Application.Features.Customers.Rules;
using Project.Application.Services.Repositories.Customers;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Commands.UpdateCommand
{
    public class UpdateCustomerCommand : IRequest<BaseCustomerDto>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, BaseCustomerDto>
        {
            private readonly ICustomerWriteRepository _customerWriteRepository;
            private readonly CustomerBusinessRules _customerBusinessRules;
            private readonly IMapper _mapper;

            public UpdateCustomerCommandHandler(IMapper mapper, ICustomerWriteRepository customerWriteRepository, CustomerBusinessRules customerBusinessRules)
            {
                _customerWriteRepository = customerWriteRepository;
                _customerBusinessRules = customerBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseCustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customerToUpdate = await _customerBusinessRules.CheckIfCustomerDoesNotExistsAndGetCustomer(request.Id);
                await _customerBusinessRules.CheckIfCustomerDoesNotExistsAndGetCustomer(request.UserId);

                _mapper.Map(request, customerToUpdate, typeof(UpdateCustomerCommand), typeof(Customer));
                await _customerWriteRepository.Update(customerToUpdate);
                return _mapper.Map<BaseCustomerDto>(customerToUpdate);
            }
        }
    }
}
