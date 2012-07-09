using Castle.Windsor;
using SimpleCqrs;
using SimpleCqrs.Domain;

namespace ECommDemo.Domain
{
    public class MultiTenantDomainRepositoryResolver : IDomainRepositoryResolver
    {
        private IServiceLocator _locator;

        public MultiTenantDomainRepositoryResolver(IServiceLocator locator)
        {
            _locator = locator;
        }

        public IDomainRepository GetRepository(string name)
        {
            string key = "dr-" + name;
            return _locator.Resolve<IDomainRepository>(key);
        }

        public void Release(IDomainRepository repository)
        {
            _locator.Release(repository);
        }
    }
}