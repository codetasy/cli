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
        Dictionary<string, Action<Dictionary<string, string>>> commands;
        Dictionary<string, string> commandArguments;       

        public string[] Arguments { get; private set; }

        public Cli(string[] args)
        {           
            commands = new Dictionary<string, Action<Dictionary<string, string>>>();
            Arguments = args;
            commandArguments = new CliArguments().ToDictionary(Arguments);
        }     

        public void Register(string commandName, Action<Dictionary<string, string>> command) 
        {
            commands.Add(commandName, command);
        }

        public void Register(Dictionary<string, Action<Dictionary<string, string>>> cmds) 
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

        public void Execute(Dictionary<string, Action<Dictionary<string, string>>> cmds)
        {
            Register(cmds);
            Execute();
        }

        Action<Dictionary<string, string>> GetCommandFromArgs()
        {
            return commands.GetValueOrDefault(Arguments.FirstOrDefault());
        }
    }
}
