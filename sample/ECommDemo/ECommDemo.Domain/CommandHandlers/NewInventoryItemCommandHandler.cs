using ECommDemo.Commanding.Commands;
using ECommDemo.Domain.InventoryContext;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace ECommDemo.Domain.CommandHandlers
{
    public class NewInventoryItemCommandHandler : CommandHandler<NewInventoryItemCommand>
    {
        private readonly IDomainRepository _repository;

        public NewInventoryItemCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(NewInventoryItemCommand command)
        {
            var item = new InventoryItem(command.ItemId, command.Description);

            _repository.Save(item);
		}
    }
}