using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Calculator
{
    public interface ICalculatorService
    {
        Task<string> EvaluateAsync(string input, CancellationToken ct);
    }
}
