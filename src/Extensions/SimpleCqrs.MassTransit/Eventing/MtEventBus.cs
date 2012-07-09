using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Eventing;
using MassTransit;

namespace SimpleCqrs.MassTransit.Eventing
{
	public class MtEventBus : IEventBus
	{
		public MtEventBus(IServiceBus bus)
		{
			_bus = bus;
		}

		private IServiceBus _bus;

		public void PublishEvent(DomainEvent domainEvent)
		{
			_bus.Publish(domainEvent);
		}

		public void PublishEvents(IEnumerable<DomainEvent> domainEvents)
		{
			foreach (var de in domainEvents)
				_bus.Publish(de);
		}
	}
}
