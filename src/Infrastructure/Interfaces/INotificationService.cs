using Doctrina.Application.Notification.Models;
using System.Threading.Tasks;

namespace Doctrina.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}