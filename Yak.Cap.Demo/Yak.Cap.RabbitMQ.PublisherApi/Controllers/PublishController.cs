using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using Yak.Cap.RabbitMQ.DB;
using Yak.Cap.RabbitMQ.Models;

namespace Yak.Cap.RabbitMQ.PublisherApi.Controllers
{
    public class PublishController : Controller
    {
        private readonly ICapPublisher _capBus;

        public PublishController(ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
        }

        //不使用事务
        [Route("~/without/transaction")]
        public IActionResult WithoutTransaction()
        {
            _capBus.Publish("xxx.services.show.time", DateTime.Now);

            return Ok();
        }

        //Ado.Net 中使用事务，自动提交
        //[Route("~/adonet/transaction")]
        //public IActionResult AdonetWithTransaction()
        //{
        //    using (var connection = new MySqlConnection(ConnectionString))
        //    {
        //        using (var transaction = connection.BeginTransaction(_capBus, autoCommit: true))
        //        {
        //            //业务代码

        //            _capBus.Publish("xxx.services.show.time", DateTime.Now);
        //        }
        //    }
        //    return Ok();
        //}

        //EntityFramework 中使用事务，自动提交
        [Route("~/ef/transaction")]
        public IActionResult EntityFrameworkWithTransaction([FromServices] CapRabbitMQDbContext dbContext)
        {
            using (var trans = dbContext.Database.BeginTransaction(_capBus, autoCommit: true))
            {
                //业务代码

                dbContext.Add<Sys_User>(new Sys_User {Name = "yak", C_Mobile = "18221546985" });
                dbContext.SaveChanges();
                _capBus.Publish("test.show.time", DateTime.Now);
            }
            return Ok();
        }
    }
}
