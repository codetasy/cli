using System;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : System.Attribute
{
    public string Name { get; private set; }

    public CommandAttribute(string name)
    {
        Name = name;
    }    
}
