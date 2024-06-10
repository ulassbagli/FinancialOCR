using Core.Persistence.Repositories;
using Core.Security.Entities;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services.Repositories.CustomerOperationClaims
{
    public class CustomerOperationClaim : Entity
    {
        public Guid CustomerId { get; set; }
        public Guid OperationClaimId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }

        public CustomerOperationClaim() 
        {
        }

        public CustomerOperationClaim(Guid id, Guid customerId, Guid operationClaimId) : base(id)
        {
            CustomerId = customerId;
            OperationClaimId = operationClaimId; 
        }
    }

}
