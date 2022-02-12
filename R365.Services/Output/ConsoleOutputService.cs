using System;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Output
{
    public class ConsoleOutputService : IOutputService
    {
        public Task WriteOutputAsync(string output, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Console.WriteLine(output);

            return Task.CompletedTask;
        }
    }
}
