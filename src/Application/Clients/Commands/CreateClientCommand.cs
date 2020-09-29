using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Clients.Commands
{
    public class CreateClientCommand : IRequest<Client>
    {
        public Store Store { get; private set; }
        public string Name { get; private set; }

        public static CreateClientCommand Create(Store store, string name)
        {
            return new CreateClientCommand()
            {
                Store = store,
                Name = name
            };
        }
    }
}
