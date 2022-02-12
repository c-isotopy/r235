using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Parsing
{
    public class NumericalParserService : INumericalParserService
    {
        public Task<int> ParseAsync(string value, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (int.TryParse(value?.Trim(), out int result))
                return Task.FromResult(result);
            else
                return Task.FromResult(0);
        }
    }
}
