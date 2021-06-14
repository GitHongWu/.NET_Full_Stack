using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPurchaseRepository _purchaseRepository;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IPurchaseRepository purchaseRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _purchaseRepository = purchaseRepository;
        }

        // access HttpContext 
        public int UserId =>
            Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity != null &&
                                       _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Email => _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value + " " +
                                  _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        public bool IsAdmin => _IsAdmin();
        private bool _IsAdmin()
        {
            if(_httpContextAccessor.HttpContext?.User.Identity != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var roles = Roles;
                return roles.Any(r => r.Contains("Admin"));
            }
            return false;
        }

        public IEnumerable<string> Roles => _Roles();
        private IEnumerable<string> _Roles()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;
            var roles = new List<string>();
            foreach (var claim in claims)
                if (claim.Type == ClaimTypes.Role)
                    roles.Add(claim.Value);
            return roles;
        }

        public bool IsSuperAdmin => _IsSuperAdmin();
        private bool _IsSuperAdmin()
        {
            if (_httpContextAccessor.HttpContext?.User.Identity != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var roles = Roles;
                return roles.Any(r => r.Contains("SuperAdmin"));
            }
            return false;
        }

        public int TotalMovies => _TotalMovies();

        private int _TotalMovies()
        {
            return _purchaseRepository.CountPurchasesByUser(UserId); ;
        }
    }
}
