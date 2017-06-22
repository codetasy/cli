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
            commandArguments = ArgumentsToDictionary();
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

        /// <summary>
        /// Parse and returns the arguments as Dictionary
        /// e.g.["--file=/some/file.txt"] stored as {Key="file", Value="/some/file.txt"}
        /// </summary>
        /// <returns></returns>
        CliDictionary<string, string> ArgumentsToDictionary() 
        {
            var argsDic = new CliDictionary<string, string>();
            
            // avoiding to add the first argument since thats the command name
            for (int i = 1; i < Arguments.Count(); i++)
            {
                var arg = Arguments[i];
                KeyValuePair<string, string> nameValue;
                argsDic.AddOrUpdate(TryParseNameValueArg(arg, out nameValue) ? nameValue : ParseFlagArg(arg));
            }           

            return argsDic;
        }

        private bool TryParseNameValueArg(string arg, out KeyValuePair<string, string> nameValue, char separator = '=')
        {
            try
            {
                var nv = arg.Split(separator);
                if (nv.Count() > 0)
                {
                    nameValue = new KeyValuePair<string, string>(RemoveTwoInitialHyphens(nv[0]), nv[1]);
                    return true;
                }
            }
            catch
            {
                // TODO: log here                
            }

            return false;                    
        }

        private KeyValuePair<string, string> ParseFlagArg(string arg)
        {
            return new KeyValuePair<string, string>(RemoveTwoInitialHyphens(arg), true.ToString());
        }

        private string RemoveTwoInitialHyphens(string argName)
        {
            return argName.StartsWith("--") ? argName.Remove(0, 2) : argName;
        }
    }
}
