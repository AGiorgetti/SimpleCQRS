using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace ECommDemo.Domain.ShopContext
{
    public abstract class TenantDomainEvent : DomainEvent
    {
        public string TenantId { get; private set; }

        protected TenantDomainEvent(string tenantId)
        {
            TenantId = tenantId;
        }
    }

    public class ShopItemCreatedEvent : TenantDomainEvent
    {
        public string ItemId { get; protected set; }
        public string Description { get; protected set; }

        public ShopItemCreatedEvent(string tenant, string itemId, string description)
            :base(tenant)
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

        public ShopItem(string shopid, string id, string description)
        {
            Apply(new ShopItemCreatedEvent(shopid, id, description)
                      {
                          AggregateRootId = Guid.NewGuid()
                      });
        }


        protected void OnShopItemCreated(ShopItemCreatedEvent e)
        {
            this.Id = e.AggregateRootId;
            this.ItemId = e.ItemId;
            this.Description = e.Description;
        }
    }
}
