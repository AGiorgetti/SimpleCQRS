using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;
using MassTransit;

namespace SimpleCqrs.MassTransit.Commanding
{
	public class MtCommandBus : ICommandBus
	{
		public MtCommandBus(IServiceBus bus)
		{
			_bus = bus;
		}

		private IServiceBus _bus;

		public int Execute<TCommand>(TCommand command) where TCommand : ICommand
		{
			throw new NotImplementedException();
			_bus.
		}

		public void Send<TCommand>(TCommand command) where TCommand : ICommand
		{
			throw new NotImplementedException();
		}
	}
}
