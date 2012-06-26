using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SignalR;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel;
using Castle.Windsor.Installer;
using Castle.MicroKernel.ComponentActivator;
using SignalR.Hubs;

namespace ECommDemo.SignalR
{
	public class CastleWindsorDependencyResolver : DefaultDependencyResolver
	{
		private readonly IWindsorContainer _container;

		public CastleWindsorDependencyResolver(IWindsorContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}

			_container = container;

			// perform the lazy registrations
			foreach (var c in _lazyRegistrations)
				_container.Register(c);

			_lazyRegistrations.Clear();


		}

		public override object GetService(Type serviceType)
		{
			if (_container.Kernel.HasComponent(serviceType))
				return _container.Resolve(serviceType);
			return base.GetService(serviceType);
		}

		public override IEnumerable<object> GetServices(Type serviceType)
		{
			IEnumerable<object> objects;
			if (_container.Kernel.HasComponent(serviceType))
				objects = _container.ResolveAll(serviceType).Cast<object>();
			else
				objects = new object[] { };

			var originalContainerServices = base.GetServices(serviceType);
			if (originalContainerServices != null)
				return objects.Concat(originalContainerServices);

			return objects;
		}

		public override void Register(Type serviceType, Func<object> activator)
		{
			// cannot override registrations in windsor
			if (serviceType == typeof(IAssemblyLocator))
			{
				base.Register(serviceType, activator);
				return;
			}

			if (_container != null)
				_container.Register(Component.For(serviceType).UsingFactoryMethod<object>(activator, true));

				// cannot unregister components in windsor
				//if (_container.Kernel.HasComponent(serviceType))
				//{
				//    var model = (_container.Kernel as IKernelInternal).GetHandler(serviceType).ComponentModel;
				//    _container.Register(Component.For(serviceType).UsingFactoryMethod<object>(activator, true));
				//}
				//else
				//    _container.Register(Component.For(serviceType).UsingFactoryMethod<object>(activator, true));
			else
				// lazy registration for when the container is up
				_lazyRegistrations.Add(Component.For(serviceType).UsingFactoryMethod<object>(activator));

			// register the factory method in the default container too
			//base.Register(serviceType, activator);
		}

		private List<ComponentRegistration<object>> _lazyRegistrations = new List<ComponentRegistration<object>>();



	}
}
