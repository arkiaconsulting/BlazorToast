using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorToast
{
    public class ToastService : IAsyncDisposable
    {
        private const string AreSupportedFunctionName = "isSupported";
        private const string RequestPermissionFunctionName = "requestPermission";
        private const string CreateFunctionName = "create";

        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ToastService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/BlazorToast/ToastService.js").AsTask());
        }

        public async ValueTask<bool> IsSupported()
        {
            var module = await moduleTask.Value;

            return await module.InvokeAsync<bool>(AreSupportedFunctionName);
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
