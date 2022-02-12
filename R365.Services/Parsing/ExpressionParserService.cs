using R365.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Parsing
{
    public class ExpressionParserService : IExpressionParserService
    {
        public static readonly char[] Delimiters = new char[] { ',', '\n' };

        private readonly INumericalParserService _numericalParser;

        public ExpressionParserService(INumericalParserService numericalParser)
        {
            _numericalParser = numericalParser;
        }

        public Task<ExpressionDto> ParseAsync(string value, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(value))
            {
                var components = new int[] { 0 };
                var result = new ExpressionDto(components);
                return Task.FromResult(result);
            }

            var componentStrings = value.Split(Delimiters, StringSplitOptions.None);

            var componentTasks = componentStrings
                .Select(componentString => _numericalParser.ParseAsync(componentString, ct))
                .ToArray();

            Task.WaitAll(componentTasks, ct);

            {
                var components = componentTasks.Select(task => task.Result);
                var result = new ExpressionDto(components);
                return Task.FromResult(result);
            }
        }
    }
}
