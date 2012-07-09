using ECommDemo.Commanding.Commands;
using ECommDemo.Domain.InventoryContext;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace ECommDemo.Domain.CommandHandlers
{
    public class NewInventoryItemCommandHandler : CommandHandler<NewInventoryItemCommand>
    {
        private readonly IDomainRepositoryResolver _repositoryResolver;

        public NewInventoryItemCommandHandler(IDomainRepositoryResolver repositoryResolver)
        {
            _repositoryResolver = repositoryResolver;
        }

        public override void Handle(NewInventoryItemCommand command)
        {
            var item = new InventoryItem(command.ItemId, command.Description);

            var repo = _repositoryResolver.GetRepository(command.TenantId);
            repo.Save(item);
            _repositoryResolver.Release(repo);
        }
    }
}