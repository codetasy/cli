using System.Collections.Generic;
using System.Linq;
using Codetasy.Extensions;

namespace Codetasy.Cli
{
    class CliArguments
    {
        /// <summary>
        /// Parse and returns the arguments as Dictionary
        /// e.g.["--file=/some/file.txt"] stored as {Key="file", Value="/some/file.txt"}
        /// </summary>
        /// <returns></returns>
        public CliDictionary<string, string> ToDictionary(string[] arguments) 
        {
            var argsDic = new CliDictionary<string, string>();
            
            // avoiding to add the first argument since thats the command name
            for (int i = 1; i < arguments.Count(); i++)
            {
                var arg = arguments[i];
                KeyValuePair<string, string> nameValue;
                argsDic.AddOrUpdate(TryParseNameValueArg(arg, out nameValue) ? nameValue : ParseFlagArg(arg));
            }           

            return argsDic;
        }

        bool TryParseNameValueArg(string arg, out KeyValuePair<string, string> nameValue, char separator = '=')
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

        KeyValuePair<string, string> ParseFlagArg(string arg)
        {
            return new KeyValuePair<string, string>(RemoveTwoInitialHyphens(arg), true.ToString());
        }

        string RemoveTwoInitialHyphens(string argName)
        {
            return argName.StartsWith("--") ? argName.Remove(0, 2) : argName;
        }        
    }    
}