using System;
using Xunit;
using Codetasy.Cli;
using System.Collections.Generic;
using System.IO;

namespace Codetasy.Cli.Tests
{
    public class IntegrationTest
    {
        [Fact]
        public void Main()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                var arguments = new [] {"goodbye", "name=Frodo"};
                new Cli(arguments).Execute(new CommandLoader().LoadCommandsFrom(this));

                Assert.Equal("Bye Frodo!", sw.ToString());
            }
        }
    }
}
