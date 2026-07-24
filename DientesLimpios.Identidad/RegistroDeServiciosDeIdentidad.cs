using DientesLimpios.Identidad.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Identidad
{
    public static class RegistroDeServiciosDeIdentidad
    {
        public static void AgregarServiciosDeIdentidad(this IServiceCollection services) 
        {
            services.AddAuthentication(IdentityConstants.BearerScheme)
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorizationBuilder();

            services.AddDbContext<DientesLimpiosIdentityDbContext>(
                options => options.UseSqlServer("name=DientesLimpiosConnectionString")
            );

            services.AddIdentityCore<Usuario>()
                .AddEntityFrameworkStores<DientesLimpiosIdentityDbContext>()
                .AddApiEndpoints();
        }
    }
}
