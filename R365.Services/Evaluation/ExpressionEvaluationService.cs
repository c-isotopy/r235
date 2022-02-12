using R365.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Evaluation
{
    public class ExpressionEvaluationService : IExpressionEvaluationService
    {
        public Task<int> EvaluateAsync(ExpressionDto expression, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var components = expression?.Components;
            var result = components == null ? 0 : components.Sum();

            return Task.FromResult(result);
        }
    }
}
