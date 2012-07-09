using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using ECommDemo.Commanding.Commands;
using ECommDemo.Domain;
using ECommDemo.Domain.Support;
using ECommDemo.ViewModel.Support;
using ECommDemo.Web.Support;
using NUnit.Framework;
using SimpleCqrs.Commanding;
using SimpleCqrs.Eventing;

namespace ECommDemo.Tests.EngineTests
{
    [TestFixture]
    public class MultitenantTests
    {
        private IWindsorContainer _container = new WindsorContainer();
        private ECommDemoRuntime _runtime;
        private ICommandBus _commandBus;

        private void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            _commandBus.Send(command);
        }
        
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container.Install(
                    new SupportInstaller(),
                    new HandlersInstaller(),
                    new DomainEventHandlersInstaller(),
                    new ViewModelInstaller().AddReaders().AddWriters()
                );

            _runtime = new ECommDemoRuntime(
                _container
                , _container.Resolve<IEventStore>()
                , _container.Resolve<ISnapshotStore>()
                , AssemblyToScanProvider.List
                );


        }

        [SetUp]
        public void BeforeEachTest()
        {
            _runtime.Start();
            _commandBus = _container.Resolve<ICommandBus>();
        }

        [TearDown]
        public void AfterEachTest()
        {
            _runtime.Shutdown();
        }


        [Test]
        public void create_items_on_two_shops()
        {
            var newItemShopA = new NewShopItemCommand("a", "a", "Item for shop a");
            var newItemShopB = new NewShopItemCommand("b", "b", "Item for shop b");
            Send(newItemShopA);
            Send(newItemShopB);
        }
    }
}
