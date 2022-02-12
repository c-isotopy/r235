using System;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Input
{
    public class ConsoleInputService : IInputService
    {
        public Task<string> ReadInputAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var task = new Task<string>(() =>
            {
                try
                {
                    return Console.ReadLine();
                }
                catch (OperationCanceledException)
                {
                    return null;
                }
            });

            task.Start();
            task.Wait(ct);

            ct.ThrowIfCancellationRequested();

            return Task.FromResult(task.Result);
        }
    }
}
