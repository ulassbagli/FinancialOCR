using Core.Persistence.Repositories;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities
{
    public class AccountantOperationClaim : Entity
    {
        public Guid AccountantId { get; set; }
        public Guid OperationClaimId { get; set; }

        public virtual Accountant Accountant { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }

        public AccountantOperationClaim() 
        { 
        }

        public AccountantOperationClaim(Guid id, Guid accountantId, Guid operationClaimId) : base(id)
        {
            AccountantId = accountantId;
            OperationClaimId = operationClaimId;
        }
    }
}
