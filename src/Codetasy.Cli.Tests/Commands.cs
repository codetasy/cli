using System.Collections.Generic;

namespace Codetasy.Cli.Tests
{
    public class Commands
    {
        [Command("hello")]
        public void Hello(Dictionary<string, string> args)
        {
            System.Console.Clear();
            System.Console.Write("Output from hello command!");
        }

        [Command("goodbye")]
        public void Bye(Dictionary<string, string> args)
        {
            System.Console.Clear();
            System.Console.Write($"Bye {args["name"]}!");
        }
    }    
}
