using Microsoft.Extensions.Hosting;
using R365.Services.UX;
using System.Threading;
using System.Threading.Tasks;

namespace R365.UI.Infrastructure
{
    public class Worker : BackgroundService
    {
        private readonly IUXService _ux;

        public Worker(IUXService ux)
        {
            _ux = ux;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            await _ux.ExecuteAsync(ct);
        }
    }
}
