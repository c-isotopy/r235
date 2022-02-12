using R365.Models;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Parsing
{
    public interface IExpressionParserService
    {
        Task<ExpressionDto> ParseAsync(string value, CancellationToken ct);
    }
}
