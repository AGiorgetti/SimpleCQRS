using SimpleCqrs.Commanding;

namespace ECommDemo.Commanding.Commands
{
    public class NewShopItemCommand : TenantCommand
    {
        public string ItemId { get; protected set; }
        public string Description { get; protected set; }

        public NewShopItemCommand(string tenant, string itemId, string description)
            :base(tenant)
        {
            ItemId = itemId;
            Description = description;
        }
    }
}