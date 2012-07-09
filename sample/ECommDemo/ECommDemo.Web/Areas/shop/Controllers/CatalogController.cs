using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommDemo.Commanding.Commands;
using ECommDemo.ViewModel.Shop;
using ECommDemo.Web.Areas.shop.Models;
using SimpleCqrs.Commanding;

namespace ECommDemo.Web.Areas.shop.Controllers
{
    public class CatalogController : Controller
    {
        public ICatalogReader Catalog { get; set; }
        private ICommandBus Bus { get; set; }

        public CatalogController(ICommandBus bus)
        {
            Bus = bus;
        }

        //
        // GET: /shop/CatalogService/

        public ActionResult Index()
        {
            ViewBag.Items = Catalog.Items;

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new NewShopItemInputModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NewShopItemInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Bus.Send(new NewShopItemCommand(
                "a",
                model.ItemId,
                model.Description
            ));

            return RedirectToAction("Index");
        }
    }
}