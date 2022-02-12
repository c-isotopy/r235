using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.UX
{
    public interface IUXService
    {
        Task ExecuteAsync(CancellationToken ct);
    }
}
