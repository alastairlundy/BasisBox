/*
    BasisBox - NewDir
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

using System.ComponentModel;

using Spectre.Console.Cli;

namespace NewDir.Cli.Settings;

public class NewDirCommandSettings : CommandSettings 
{
        [CommandArgument(0, "<directory_name>")]
        [DefaultValue(null)]
        public string? DirectoryName { get; init; }
        
        [CommandOption("-p|--parents")]
        [DefaultValue(false)]
        public bool CreateParentDirectories { get; init; }
        
        [CommandOption("-m|--mode")]
        [DefaultValue(null)]
        public string? Mode { get; init; }
        
        [CommandOption("--debug|--debugging|--verbose")]
        [DefaultValue(false)]
        public bool UseDebugging { get; init; }
}