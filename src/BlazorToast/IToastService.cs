using System.Threading.Tasks;

namespace BlazorToast
{
    public interface IToastService
    {
        ValueTask<object> Create(string title, object options);
        ValueTask<bool> IsSupported();
        ValueTask<NotificationPermission> RequestPermission();
    }
}