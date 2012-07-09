using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommDemo.Commanding.Commands;
using SimpleCqrs.Commanding;

namespace ECommDemo.Web.Controllers
{
    public class ItemsController : Controller
    {
        private  ICommandBus Bus { get; set; }

        public ItemsController(ICommandBus bus)
        {
            Bus = bus;
        }

        public ActionResult Index()
        {
            Bus.Send(new NewInventoryItemCommand("a", "1","Articolo 1"));
            
            return Content("ok");
        }
    }
}
