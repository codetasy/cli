using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Codetasy.Extensions;

namespace Codetasy.Cli
{
    public class Cli
    {
        Dictionary<string, Action<CliDictionary<string, string>>> commands;
        CliDictionary<string, string> commandArguments;       

        public string[] Arguments { get; private set; }

        public Cli(string[] args)
        {           
            commands = new Dictionary<string, Action<CliDictionary<string, string>>>();
            Arguments = args;
            commandArguments = new CliArguments().ToDictionary(Arguments);
        }     

        public void Register(string commandName, Action<CliDictionary<string, string>> command) 
        {
            commands.Add(commandName, command);
        }

        public void Register(Dictionary<string, Action<CliDictionary<string, string>>> cmds) 
        {
            commands = cmds;
        }       

        public void Execute()
        {
            try
            {
                GetCommandFromArgs()?.Invoke(commandArguments); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public void Execute(Dictionary<string, Action<CliDictionary<string, string>>> cmds)
        {
            Register(cmds);
            Execute();
        }

        Action<CliDictionary<string, string>> GetCommandFromArgs()
        {
            return commands.GetValueOrDefault(Arguments.FirstOrDefault());
        }
    }
}
