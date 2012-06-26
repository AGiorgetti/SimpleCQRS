using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace ECommDemo.Domain.ShopContext
{
    public class ShopItemCreatedEvent : DomainEvent
    {
        public string ItemId { get; protected set; }
        public string Description { get; protected set; }

        public ShopItemCreatedEvent(string itemId, string description)
        {
            ItemId = itemId;
            Description = description;
        }
    }

    public class ShopItem : AggregateRoot
    {
        public string ItemId { get; protected set; }
        public string Description { get; protected set; }
        public decimal BasePrice { get; protected set; }

        public ShopItem()
        {
        }

        public ShopItem(string id, string description)
        {
            Apply(new ShopItemCreatedEvent(id, description)
                      {
                          AggregateRootId = Guid.NewGuid()
                      });
        }
		
        protected void OnItemCreated(ShopItemCreatedEvent e)
        {
            this.Id = e.AggregateRootId;
            this.ItemId = e.ItemId;
            this.Description = e.Description;
        }
    }
}
