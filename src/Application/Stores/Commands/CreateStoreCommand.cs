using Doctrina.Domain.Models;
using MediatR;

namespace Doctrina.Application.Stores.Commands
{
    public class CreateStoreCommand : IRequest<Store>
    {
        public string Name { get; private set; }
        public Organisation Organisation { get; private set; }

        public static CreateStoreCommand Create(Organisation org, string name)
        {
            return new CreateStoreCommand()
            {
                Organisation = org,
                Name = name
            };
        }
    }
}
