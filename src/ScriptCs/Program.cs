using System;
using System.Reflection;
using ScriptCs.Argument;
using ScriptCs.Command;
using ScriptCs.Hosting;
using ScriptCs.Contracts;

namespace ScriptCs
{
    internal static class Program
    {
        [LoaderOptimizationAttribute(LoaderOptimization.MultiDomain)]
        private static int Main(string[] args)
        {
            SetProfile();
            var arguments = ParseArguments(args);
            
            IConsole console = new ScriptConsole();
            var scriptServicesBuilder = ScriptServicesBuilderFactory.Create(arguments.CommandArguments, arguments.ScriptArguments, console);
            
            var factory = new CommandFactory(scriptServicesBuilder,console);
            var command = factory.CreateCommand(arguments.CommandArguments, arguments.ScriptArguments);
            
            return (int)command.Execute();
        }

        private static void SetProfile()
        {
            var profileOptimizationType = Type.GetType("System.Runtime.ProfileOptimization");
            if (profileOptimizationType != null)
            {
                var setProfileRoot = profileOptimizationType.GetMethod("SetProfileRoot", BindingFlags.Public | BindingFlags.Static);
                setProfileRoot.Invoke(null, new object[] { typeof(Program).Assembly.Location });

                var startProfile = profileOptimizationType.GetMethod("StartProfile", BindingFlags.Public | BindingFlags.Static);
                startProfile.Invoke(null, new object[] { typeof(Program).Assembly.GetName().Name + ".profile" });
            }
        }

        private static ArgumentParseResult ParseArguments(string[] args)
        {
        	
            var console = new ScriptConsole(); // an IConsole; for icsharp we want a differentone.
            try
            {
                var parser = new ArgumentHandler(new ArgumentParser(console), new ConfigFileParser(console), new FileSystem());
                return parser.Parse(args);
            }
            finally
            {
                console.Exit();
            }
        }
    }
}
