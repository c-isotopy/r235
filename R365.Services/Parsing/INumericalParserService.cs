using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Parsing
{
    public interface INumericalParserService
    {
        Task<int> ParseAsync(string value, CancellationToken ct);
    }
}
