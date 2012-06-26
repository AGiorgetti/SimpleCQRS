using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SignalR.Hubs;
using SimpleCqrs.Commanding;
using ECommDemo.ViewModel.Shop;
using ECommDemo.Commanding.Commands;

namespace ECommDemo.SignalR
{
	[HubName("catalogHub")]
	public class CatalogHub : Hub
	{
		//public CatalogHub()
		//{ }

		public CatalogHub(ICommandBus commandBus, ICatalogReader catalogReader, ICatalogWriter catalogWriter)
		{
			CommandBus = commandBus;
			CatalogReader = catalogReader;
			CatalogWriter = catalogWriter;
		}

		private ICommandBus CommandBus { get; set; }

		private ICatalogReader CatalogReader { get; set; }

		private ICatalogWriter CatalogWriter { get; set; }

		public string Echo(string message)
		{
			return message;
		}

		public IEnumerable<Item> GetItems()
		{
			return CatalogReader.Items;
		}

		public void Create(string itemId, string description)
		{
			CommandBus.Send(new NewShopItemCommand(
				itemId,
				description
			));
		}
	}
}
