using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Input
{
    public interface IInputService
    {
        Task<string> ReadInputAsync(CancellationToken ct);
    }
}
