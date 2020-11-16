using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorToast
{
    internal class ToastService : IAsyncDisposable, IToastService
    {
        private const string IsSupportedMethod = "isSupported";
        private const string RequestPermissionMethod = "requestPermission";
        private const string CreateMethod = "create";

        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ToastService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorToast/ToastService.js").AsTask());
        }

        public async ValueTask<bool> IsSupported()
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<bool>(IsSupportedMethod);
        }

        public async ValueTask<NotificationPermission> RequestPermission()
        {
            var module = await moduleTask.Value;

            var invocationResult = await module.InvokeAsync<string>(RequestPermissionMethod);

            if (!Enum.TryParse(invocationResult, out NotificationPermission permission))
            {
                throw new InvalidOperationException($"Unknown permission value: '{invocationResult}'");
            }

            return permission;
        }

        public async ValueTask<object> Create(string title, object options)
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<object>(CreateMethod, title, options);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
