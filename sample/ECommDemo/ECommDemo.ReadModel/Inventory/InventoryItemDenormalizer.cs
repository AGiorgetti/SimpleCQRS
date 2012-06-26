using ECommDemo.Domain.InventoryContext;
using SimpleCqrs.Eventing;
using SignalR;

namespace ECommDemo.ViewModel.Inventory
{
	public class InventoryItemDenormalizer : IHandleDomainEvents<InventoryItemCreatedEvent>
	{
		private readonly IInventoryWriter _inventoryWriter;
		private readonly IConnectionManager _connMgr;

		public InventoryItemDenormalizer(IInventoryWriter inventoryWriter, IConnectionManager connMgr)
		{
			_inventoryWriter = inventoryWriter;
			_connMgr = connMgr;
		}

		public void Handle(InventoryItemCreatedEvent e)
		{
			_inventoryWriter.Save(new Item()
									  {
										  Id = e.AggregateRootId,
										  ItemId = e.ItemId,
										  Description = e.Description,
										  InStock = 0
									  });

			// notify that the item has been added, we can even return the data back if we need
			var hub = _connMgr.GetHubContext("catalogHub");
			hub.Clients.publish("item added");
		}
	}
}
