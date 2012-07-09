using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace ECommDemo.Commanding.Commands
{
    public class TenantCommand : ICommand
    {
        public string TenantId { get; private set; }

        public TenantCommand(string tenantId)
        {
            TenantId = tenantId;
        }
    }

    public class NewInventoryItemCommand : TenantCommand
    {
        public string ItemId { get; protected set; }
        public string Description { get; protected set; }

        public NewInventoryItemCommand(string tenant, string itemId, string description)
            :base(tenant)
        {
            ItemId = itemId;
            Description = description;
        }
    }
}
