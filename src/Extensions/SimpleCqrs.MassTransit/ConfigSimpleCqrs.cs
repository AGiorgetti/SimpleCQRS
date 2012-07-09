using SimpleCqrs.Commanding;
using SimpleCqrs.Eventing;
using MassTransit;
using SimpleCqrs.MassTransit.Commanding;
using SimpleCqrs.MassTransit.Eventing;

namespace SimpleCqrs.MassTransit
{
    public class ConfigSimpleCqrs 
    {
        private readonly ISimpleCqrsRuntime runtime;

        public ConfigSimpleCqrs(ISimpleCqrsRuntime runtime)
        {
            this.runtime = runtime;
        }

        public IServiceLocator ServiceLocator { get; private set; }

		private IServiceBus bus;

        public void Configure()
        {
            runtime.Start();

			bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseMsmq();
				sbc.VerifyMsmqConfiguration();
				sbc.UseMulticastSubscriptionClient();
				sbc.ReceiveFrom("msmq://localhost/test_queue");
			});
			
            ServiceLocator = runtime.ServiceLocator;
            ServiceLocator.Register(bus);
        }

        public ConfigSimpleCqrs UseLocalCommandBus()
        {
            var commandBus = ServiceLocator.Resolve<LocalCommandBus>();
            ServiceLocator.Register<ICommandBus>(commandBus);
             return this;
        }

		public ConfigSimpleCqrs UseLocalEventBus()
		{
			var eventBus = ServiceLocator.Resolve<LocalEventBus>();
			ServiceLocator.Register<IEventBus>(eventBus);
			return this;
		}

        public ConfigSimpleCqrs UseMtCommandBus()
        {
			ICommandBus cbus = new MtCommandBus(bus);
			
			ServiceLocator.Register<ICommandBus>(cbus);
            
            return this;
        }

        public ConfigSimpleCqrs UseNsbEventBus()
        {
            var eventBus = new MtEventBus(bus);

            ServiceLocator.Register<IEventBus>(eventBus);
            
            return this;
        }

		public ConfigSimpleCqrs SubscribeForDomainEvents()
		{
			var typeCatalog = ServiceLocator.Resolve<ITypeCatalog>();
			var domainEventHandlerFactory = ServiceLocator.Resolve<IDomainEventHandlerFactory>();
			var domainEventTypes = typeCatalog.GetGenericInterfaceImplementations(typeof(IHandleDomainEvents<>));

			var eventBus = new NsbLocalEventBus(domainEventTypes, domainEventHandlerFactory);
			Configurer.RegisterSingleton<IEventBus>(eventBus);

			var configEventBus = new ConfigEventBus();
			configEventBus.Configure(this, runtime);

			return this;
		}
    }
}