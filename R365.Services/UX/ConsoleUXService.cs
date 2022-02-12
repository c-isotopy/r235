using R365.Services.Calculator;
using R365.Services.Input;
using R365.Services.Output;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.UX
{
    public class ConsoleUXService : IUXService
    {
        private readonly IInputService _reader;
        private readonly IOutputService _writer;
        private readonly ICalculatorService _calculator;

        public ConsoleUXService(IInputService reader, IOutputService writer, ICalculatorService calculator)
        {
            _reader = reader;
            _writer = writer;
            _calculator = calculator;
        }

        public async Task ExecuteAsync(CancellationToken ct)
        {
            while (true)
            {
                var input = await _reader.ReadInputAsync(ct);
                if (input == null)
                    return;

                var output = await _calculator.EvaluateAsync(input, ct);
                _writer.WriteOutputAsync(output, ct);
            }
        }
    }
}
