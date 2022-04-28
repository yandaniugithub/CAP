using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Savorboard.CAP.InMemoryMessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yak.Cap.InMemory.Api
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
            //����¼�����cap
            services.AddCap(x => {
                // ʹ���ڴ�洢��Ϣ(��Ϣ����ʧ�ܴ���)
                x.UseInMemoryStorage();

            });
           
            //services.AddCap(x => {
            //    //�����ʹ�õ�ADO.NET���������ݿ�ѡ��������ã�
            //    x.UseSqlServer("server=.\\MSSQLSERVER2016;uid=sa;pwd=sasa;database=CAP");

            //    //CAP֧�� RabbitMQ��Kafka��AzureServiceBus ����ΪMQ������ʹ��ѡ�����ã�
            //    x.UseRabbitMQ(y => {
            //        y.HostName = "localhost";
            //        y.Port = 5672;
            //        y.VirtualHost = "/";
            //        y.UserName = "admin";
            //        y.Password = "123456";
            //    });
            //});
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
