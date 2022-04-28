using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yak.Cap.RabbitMQ.DB;

namespace Yak.Cap.RabbitMQ.PublisherApi
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

            services.AddControllers();
            //������ݿ������ķ���
            services.AddDbContext<CapRabbitMQDbContext>();
            //����¼�����cap
            services.AddCap(x => {
                // ʹ���ڴ�洢��Ϣ(��Ϣ����ʧ�ܴ���)
                //x.UseInMemoryStorage();

                x.UseEntityFramework<CapRabbitMQDbContext>();

                //ʹ��RabbitMQ�����¼����Ĵ���
                x.UseRabbitMQ(rb => {
                    rb.HostName = "localhost";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                    rb.Port = 5672;
                    rb.VirtualHost = "/";
                });
                //�����Ǳ���
                x.UseDashboard();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CapRabbitMQDbContext dataDBContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            dataDBContext.Database.EnsureCreated();//���ݿⲻ���ڵĻ������Զ�����
            //app.UseCap();
        }
    }
}
