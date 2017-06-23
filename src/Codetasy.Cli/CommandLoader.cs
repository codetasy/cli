using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Codetasy.Cli
{
    public class CommandLoader
	{
		public Dictionary<string, Action<Dictionary<string, string>>> Commands { get; set; }		

		public CommandLoader()
		{
            Commands = new Dictionary<string, Action<Dictionary<string, string>>>();
		}

		public Dictionary<string, Action<Dictionary<string, string>>> LoadCommandsFrom(object invoker)
		{
			RegisterMethods(invoker);
			return Commands;
		}

		void RegisterMethods(object invoker)
		{
			foreach (var type in invoker.GetType().GetTypeInfo().Assembly.ExportedTypes)
			{
				foreach(var methodInfo in type.GetTypeInfo().DeclaredMethods)
				{
					var attribute = methodInfo.GetCustomAttributes(typeof(CommandAttribute)).FirstOrDefault();
					if (attribute != null)
					{
						var action = methodInfo.CreateDelegate(typeof(Action<System.Collections.Generic.Dictionary<string, string>>), null);
                        Commands.Add((attribute as CommandAttribute).Name, (Action<System.Collections.Generic.Dictionary<string, string>>)action);
					}
				}
			}
		}
	}
}
