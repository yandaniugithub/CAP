using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Yak.Cap.InMemory.Api.Controllers
{
    public class ConsumerController : Controller
    {
        [NonAction]
        [CapSubscribe("test.show.time")]
        public void ReceiveMessage(DateTime time)
        {
            Console.WriteLine("message time is:" + time);
        }
        [NonAction]
        [CapSubscribe("test.CapHeadershow.time")]
        public void ReceiveCapHeaderMessage(DateTime time)
        {
            Console.WriteLine("message time is:" + time);
        }

    }
}
