using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Teste.Manager.Application;
using Teste.Manager.Application.TestCaseInterfaces.Contract;
using Teste.Manager.Application.TestCaseInterfaces.Factory;
using Teste.Manager.Application.TestCaseInterfaces.WebExecutors;
using Teste.Manager.DataAccess;


namespace dcc_teste_manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<TestManagerContext>(options =>
                options.UseLazyLoadingProxies(false).UseSqlServer(Configuration.GetConnectionString("TestManagerContext"))); 

            services.AddControllers();

            services.AddSingleton<IEngine, Engine>();
            services.AddSingleton<IExecutionSteps, ExecutionSteps>();
            services.AddSingleton<IDeviceTypeExecutorFactory, DeviceTypeExecutorFactory>();
            services.AddSingleton<IWebDeviceInterfaceFactory, WebDeviceInterfaceFactory>();
            services.AddSingleton<IWebBasicsSteps, WebBasicsSteps>();

            services.AddSingleton<ITestCaseExecutor, WebTestCaseExecutor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
