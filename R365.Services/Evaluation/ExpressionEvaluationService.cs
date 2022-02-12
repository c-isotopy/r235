using R365.Models;
using System;
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

            if (components != null)
            {
                var negatives = components.Where(candidate => candidate < 0).ToArray();
                if (negatives.Length > 0)
                    throw new ArgumentOutOfRangeException($"Invalid inputs - negative values are not allowed: ({string.Join(", ", negatives)})"); 
            }

            var result = components == null ? 0 : components.Sum();

            return Task.FromResult(result);
        }
    }
}
