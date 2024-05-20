using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Bases
{
    public class BaseHandler
    {
        public readonly IHttpContextAccessor _contextAccessor;
        public readonly IMapper _mapper;
        public readonly Guid userId;

        public BaseHandler(IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            this._mapper = mapper;
            this._contextAccessor = httpContextAccessor;
            userId = Guid.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
