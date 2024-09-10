/*
    BasisBox - Moment Library
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */


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
            FileName = args.First(),
        };
    }

    public Process ToProcess()
    {
        return _process;
    }
    
    protected 
}