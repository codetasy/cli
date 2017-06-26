namespace Codetasy.Cli
{
    public static class CliExtensions
    {
        public static void Run(this Cli cli, object invoker)
        {
            cli.Execute(new CommandLoader().LoadCommandsFrom(invoker));          
        }
    }
}
