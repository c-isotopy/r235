using R365.Models;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Evaluation
{
    public interface IExpressionEvaluationService
    {
        Task<int> EvaluateAsync(ExpressionDto expression, CancellationToken ct);
    }
}
