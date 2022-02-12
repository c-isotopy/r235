using R365.Services.Evaluation;
using R365.Services.Parsing;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Calculator
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IExpressionParserService _parser;
        private readonly IExpressionEvaluationService _evaluator;

        public CalculatorService(IExpressionParserService parser, IExpressionEvaluationService evaluator)
        {
            _parser = parser;
            _evaluator = evaluator;
        }

        public async Task<string> EvaluateAsync(string input, CancellationToken ct)
        {
            var expression = await _parser.ParseAsync(input, ct);
            var result = await _evaluator.EvaluateAsync(expression, ct);

            return result.ToString();
        }
    }
}
