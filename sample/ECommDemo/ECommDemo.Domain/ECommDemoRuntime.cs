using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ECommDemo.Commanding.Commands;
using ECommDemo.Domain.CommandHandlers;
using SimpleCqrs;
using SimpleCqrs.Domain;
using SimpleCqrs.EventStore.MongoDb;
using SimpleCqrs.Eventing;
using SimpleCqrs.Windsor;

namespace ECommDemo.Domain
{
    public class ECommDemoRuntime : SimpleCqrs.SimpleCqrsRuntime<WindsorServiceLocator>
    {
        private IWindsorContainer _container;
        private IEventStore _eventStore;
        private ISnapshotStore _snapshotStore;
        private IEnumerable<Assembly> _assembliesToScan;

        public ECommDemoRuntime(IWindsorContainer container, IEventStore eventStore, ISnapshotStore snapshotStore, IEnumerable<Assembly> assemblies)
        {
            _container = container;
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
            _assembliesToScan = assemblies;
        }

        protected override SimpleCqrs.Domain.IDomainRepositoryResolver GetDomainRepositoryResolver(IServiceLocator locator)
        {
            _container.Register(
                    Component
                        .For<IDomainRepository>()
                        .Instance(new DomainRepository(
                            new MongoEventStore("server=localhost;database=ecomm-a"),
                            new MongoSnapshotStore("server=localhost;database=ecomm-a"), 
                            locator.Resolve<IEventBus>()))
                        .Named("dr-a"),
                    Component
                        .For<IDomainRepository>()
                        .Instance(new DomainRepository(
                            new MongoEventStore("server=localhost;database=ecomm-b"),
                            new MongoSnapshotStore("server=localhost;database=ecomm-b"),
                            locator.Resolve<IEventBus>()))
                        .Named("dr-b")
                );


            return new MultiTenantDomainRepositoryResolver(locator);
        }

        protected override WindsorServiceLocator GetServiceLocator()
        {
            return new WindsorServiceLocator(_container);
        }

        protected override IEventStore GetEventStore(IServiceLocator serviceLocator)
        {
            return _eventStore;
        }

        protected override ISnapshotStore GetSnapshotStore(IServiceLocator serviceLocator)
        {
            return _snapshotStore;
        }

        protected override System.Collections.Generic.IEnumerable<System.Reflection.Assembly> GetAssembliesToScan(IServiceLocator serviceLocator)
        {
            return _assembliesToScan;
        }
    }
}
