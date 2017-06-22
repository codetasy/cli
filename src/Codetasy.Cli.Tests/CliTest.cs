using System;
using Xunit;
using Codetasy.Cli;
using System.Collections.Generic;
using System.IO;

namespace Codetasy.Cli.Tests
{
    public class CliTest
    {
        [Fact]
        public void CanExecuteCommand()
        {
            var executed = false;

            var arguments = new [] {"hello"};

            var commands = new Dictionary<string, Action<CliDictionary<string, string>>>();
            commands.Add("hello", args =>
            {
                executed = true;
            });
            
            new Cli(arguments).Execute(commands);

            Assert.True(executed);
        }

        [Fact]
        public void CanParseSingleArgument()
        {
            var message = string.Empty;

            var arguments = new [] {"hello", "name=Frodo"};

            var commands = new Dictionary<string, Action<CliDictionary<string, string>>>();
            commands.Add("hello", args =>
            {
                message = $"Hello {args["name"]}";
            });
            
            new Cli(arguments).Execute(commands);

            Assert.Equal("Hello Frodo", message);
        }

        [Fact]
        public void CanParseMultipleArguments()
        {
            var message = string.Empty;

            var arguments = new [] {"hello", "--name=\"Frodo Baggins\"", "--region=\"The Shire\""};

            var commands = new Dictionary<string, Action<CliDictionary<string, string>>>();
            commands.Add("hello", args =>
            {
                message = $"Hello {args["name"]} from {args["region"]}";
            });
            
            new Cli(arguments).Execute(commands);

            Assert.Equal("Hello \"Frodo Baggins\" from \"The Shire\"", message);
        }
    }
}
