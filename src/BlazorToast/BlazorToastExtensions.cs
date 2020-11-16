using Microsoft.Extensions.DependencyInjection;

namespace BlazorToast
{
    public static class BlazorToastExtensions
    {
        public static IServiceCollection AddToasts(this IServiceCollection services)
        {
            return services.AddScoped<IToastService, ToastService>();
        }
    }
}
