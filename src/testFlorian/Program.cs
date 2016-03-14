using System;
using System.Reflection;
using System.Collections.Generic;

using Common.Logging;
using Common.Logging.Simple;

using ScriptCs.Argument;
using ScriptCs.Command;
using ScriptCs.Hosting;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Engine.Mono;

using ScriptCs;

namespace testFlorian
{
	class Program
	{
		private static ScriptCs.Repl CreateRepl (Common.Logging.ILog logger,IConsole console)
		{
			ObjectSerializer serializer = new ObjectSerializer ();

			var scriptEngine =new MonoScriptEngine (new ScriptHostFactory (), logger);
			//RoslynScriptEngine scriptEngine =new RoslynScriptEngine (new ScriptHostFactory (), logger);
			var initializationServices = new InitializationServices(logger);
			initializationServices.GetAppDomainAssemblyResolver().Initialize();
			
			var fileSystem = initializationServices.GetFileSystem();
			string[] ScriptArgs = new string[0];
						
			FilePreProcessor filePreProcessor = new FilePreProcessor (fileSystem,logger, new List<ILineProcessor>());
			var repl = new ScriptCs.Repl(ScriptArgs, fileSystem, scriptEngine, serializer, logger, console, filePreProcessor, new List<IReplCommand> ());

			var workingDirectory = fileSystem.CurrentDirectory;
			var assemblies = initializationServices.GetAssemblyResolver ();
			// var assemblies = _assemblyResolver.GetAssemblyPaths(workingDirectory);
			// initializationServices.GetModuleLoader();
			// var scriptPacks = _scriptPackResolver.GetPacks();
			string[] ass = new string[0];
			
			repl.Initialize(ass, new List<IScriptPack>(), ScriptArgs);
			return repl;
		}
		
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			Common.Logging.ILog logger = new ConsoleOutLogger("kernel.log", Common.Logging.LogLevel.All, true, true, false, "yyyy/MM/dd HH:mm:ss:fff");
			var console = new ScriptConsole(); // an IConsole; for icsharp we want a differentone.
						
			var repl =CreateRepl (logger, console);
						
			repl.Execute("int i=13;");
			repl.Execute("int j=i+7;");
			repl.Execute("j");
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}