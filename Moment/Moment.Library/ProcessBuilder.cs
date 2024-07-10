using System.Diagnostics;

namespace Moment.Library;

public class ProcessBuilder
{
    protected Process _process;

    protected ProcessStartInfo _processStartInfo;
    
    public ProcessBuilder(IEnumerable<string> args)
    {
        _process = new Process();

        _processStartInfo = new ProcessStartInfo()
        {
            Arguments = args.
        };
    }

    public Process ToProcess()
    {
        return _process;
    }
    
    protected 
}