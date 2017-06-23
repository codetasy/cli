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
    }
}
