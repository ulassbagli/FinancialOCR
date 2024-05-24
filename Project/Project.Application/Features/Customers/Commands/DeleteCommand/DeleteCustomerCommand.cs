using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Customers.Dtos;
using Project.Application.Features.Customers.Dtos.BaseDto;
using Project.Application.Features.Customers.Rules;
using Project.Application.Services.Repositories.Customers;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Customers.Commands.DeleteCommand
{
    public class DeleteCustomerCommand : IRequest<DeletedCustomerDto
    {
        public string Id { get; set; }
        public bool isSoftDelete { get; set; }

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeletedCustomerDto>
        {
            private readonly ICustomerWriteRepository _customerWriteRepository;
            private readonly IMapper _mapper;
            private readonly CustomerBusinessRules _customerBusinessRules;

            public DeleteCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, IMapper mapper,
                CustomerBusinessRules customerBusinessRules)
            {
                _mapper = mapper;
                _customerBusinessRules = customerBusinessRules;
                _customerWriteRepository = customerWriteRepository;
            }

            public async Task<DeletedCustomerDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerBusinessRules.CheckIfCustomerDoesNotExistsAndGetCustomer(request.Id);

                Customer deletedCustomer;
                if (request.isSoftDelete)
                    deletedCustomer = await _customerWriteRepository.SoftRemove(customer);
                else
                    deletedCustomer = await _customerWriteRepository.HardRemove(customer);

                return _mapper.Map<DeletedCustomerDto>(deletedCustomer);
            }
        }
    }
}
