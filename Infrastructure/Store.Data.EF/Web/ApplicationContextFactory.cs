using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.Data.EF.Store;

namespace Store.Data.EF.Identity
{
    public class ApplicationContextFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public ApplicationContextFactory(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            this.httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public ApplicationDbContext Create()
        {
            var services = httpContextAccessor.HttpContext?.RequestServices
                ?? throw new InvalidOperationException("HttpContext недоступний.");

            var dbContext = services.GetService<ApplicationDbContext>();
            if (dbContext != null)
            {
                return dbContext; 
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return dbContext;
            }
        }

    }
}
