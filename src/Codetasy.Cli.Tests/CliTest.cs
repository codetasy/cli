using System;
using Xunit;
using Codetasy.Cli;

namespace Codetasy.Cli.Tests
{
    public class CliTest
    {
        [Fact]
        public void CanInstantiate()
        {
            var args = new string[0];
            var cli = new Cli(args);

            Assert.IsType<Cli>(cli);
        }
    }
}
