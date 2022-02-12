using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace R365.Services.Output
{
    public interface IOutputService
    {
        Task WriteOutputAsync(string output, CancellationToken ct);
    }
}
