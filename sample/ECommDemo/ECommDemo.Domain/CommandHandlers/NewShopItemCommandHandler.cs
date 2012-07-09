using ECommDemo.Commanding.Commands;
using ECommDemo.Domain.ShopContext;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace ECommDemo.Domain.CommandHandlers
{
    public class NewShopItemCommandHandler : CommandHandler<NewShopItemCommand>
    {
        private readonly IDomainRepositoryResolver _repositoryResolver;

        public NewShopItemCommandHandler(IDomainRepositoryResolver repositoryResolver)
        {
            _repositoryResolver = repositoryResolver;
        }

        public override void Handle(NewShopItemCommand command)
        {
            var item = new ShopItem(command.TenantId, command.ItemId, command.Description);

            var repo = _repositoryResolver.GetRepository(command.TenantId);
            repo.Save(item);
            _repositoryResolver.Release(repo);
        }
    }
}