using System;
using Xunit;
using Codetasy.Cli;
using System.Collections.Generic;
using System.IO;

namespace Codetasy.Cli.Tests
{
    public class CommandLoaderTest
    {
        [Fact]
        public void CanLoadHelloCommand()
        {
            var commands = new CommandLoader().LoadCommandsFrom(this);
            Assert.True(commands["hello"] != null);
        }

        [Fact]
        public void CanRunHelloCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                 var commands = new CommandLoader().LoadCommandsFrom(this);
                 commands["hello"].Invoke(null);

                 Assert.Equal("Output from hello command!", sw.ToString());
            }
        }
    }
}
