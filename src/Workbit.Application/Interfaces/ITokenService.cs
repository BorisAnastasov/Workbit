using System;
using System.Collections.Generic;
using System.Text;
using Workbit.Application.Common.Models;
using Workbit.Domain.Entities.Account;

namespace Workbit.Application.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResult> GenerateTokenAsync(ApplicationUser user, CancellationToken cancellationToken);
    }
}
