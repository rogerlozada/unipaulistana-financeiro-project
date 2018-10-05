namespace unipaulistana.web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using unipaulistana.data;
    using unipaulistana.model;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();
            services.AddMvc(options => options.MaxModelValidationErrors = 50);

            services.Configure<AppConnectionSettings>(options => Configuration.GetSection("ConnectionStrings").Bind(options));

             services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  
                    .AddCookie(options =>  
                    {  
                        options.LoginPath = "/Home/Index";  
                        options.AccessDeniedPath = "/Home/AcessoNegado";
                    });  

            services.AddScoped<ConexaoContext>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IGrupoDeSegurancaRepository, GrupoDeSegurancaRepository>();
            services.AddScoped<IGrupoDeSegurancaService, GrupoDeSegurancaService>();
            services.AddScoped<IDiretivaSegurancaRepository, DiretivaSegurancaRepository>();
            services.AddScoped<IDiretivaSegurancaService, DiretivaSegurancaService>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();
            services.AddScoped<IFinanceiroService, FinanceiroService>();

            services.AddAuthorization(options => {
               
               //diretivas de usuário
               options.AddPolicy("PermiteListarUsuario", policy=> policy.RequireClaim("PermiteListarUsuario"));  
               options.AddPolicy("PermiteCriarUsuario", policy=> policy.RequireClaim("PermiteCriarUsuario"));  
               options.AddPolicy("PermiteAlterarUsuario", policy=> policy.RequireClaim("PermiteAlterarUsuario"));  
               options.AddPolicy("PermiteExcluirUsuario", policy=> policy.RequireClaim("PermiteExcluirUsuario"));  
               options.AddPolicy("PermiteAlterarSenhaViaAdminUsuario", policy=> policy.RequireClaim("PermiteAlterarSenhaViaAdminUsuario"));  

                //direitvas de cliente
                options.AddPolicy("PermiteListarCliente", policy=> policy.RequireClaim("PermiteListarCliente"));  
                options.AddPolicy("PermiteCriarCliente", policy=> policy.RequireClaim("PermiteCriarCliente"));  
                options.AddPolicy("PermiteAlterarCliente", policy=> policy.RequireClaim("PermiteAlterarCliente"));  
                options.AddPolicy("PermiteExcluirCliente", policy=> policy.RequireClaim("PermiteExcluirCliente"));  

                //direitvas de grupo de segurança
                options.AddPolicy("PermiteListarGrupoDeSeguranca", policy=> policy.RequireClaim("PermiteListarGrupoDeSeguranca"));  
                options.AddPolicy("PermiteCriarGrupoDeSeguranca", policy=> policy.RequireClaim("PermiteCriarGrupoDeSeguranca"));  
                options.AddPolicy("PermiteAlterarGrupoDeSeguranca", policy=> policy.RequireClaim("PermiteAlterarGrupoDeSeguranca"));  
                options.AddPolicy("PermiteExcluirGrupoDeSeguranca", policy=> policy.RequireClaim("PermiteExcluirGrupoDeSeguranca")); 
                options.AddPolicy("PermiteAssociarDiretivaGrupoDeSeguranca", policy=> policy.RequireClaim("PermiteAssociarDiretivaGrupoDeSeguranca")); 

                 //diretivas de solicitação
                options.AddPolicy("PermiteListarFinanceiro", policy=> policy.RequireClaim("PermiteListarFinanceiro"));  
                options.AddPolicy("PermiteCriarFinanceiro", policy=> policy.RequireClaim("PermiteCriarFinanceiro"));  
                options.AddPolicy("PermiteAlterarFinanceiro", policy=> policy.RequireClaim("PermiteAlterarFinanceiro"));  
                options.AddPolicy("PermiteConcluirFinanceiro", policy=> policy.RequireClaim("PermiteConcluirFinanceiro"));  
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            app.UseAuthentication();  

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
