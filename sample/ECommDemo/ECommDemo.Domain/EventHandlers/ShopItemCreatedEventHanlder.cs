using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommDemo.Commanding.Commands;
using ECommDemo.Domain.InventoryContext;
using ECommDemo.Domain.ShopContext;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace ECommDemo.Domain.EventHandlers
{
    public class ShopItemCreatedEventHanlder : IHandleDomainEvents<ShopItemCreatedEvent>
    {
        private readonly ICommandBus _bus;

        public ShopItemCreatedEventHanlder(ICommandBus bus)
        {
            _bus = bus;
        }

        public void Handle(ShopItemCreatedEvent e)
        {
            _bus.Send(new NewInventoryItemCommand(e.TenantId, e.ItemId, e.Description));
        }
    }
}
