namespace SimpleCqrs.Domain
{
    public interface IDomainRepositoryResolver
    {
        IDomainRepository GetRepository(string name);
        void Release(IDomainRepository repository);
    }
}