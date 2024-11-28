using FastEndpointsTool.Generator;
using FastEndpointsTool.Parsing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FastEndpointsTool;

internal class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"FastEndpointsTool Version v{GetVersion()}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                ShowHelp();
                return;
            }
            if (args.Length == 1 && (args[0] == "--help" || args[0] == "-h"))
            {
                Console.WriteLine("Usage:");
                ShowHelp();
                return;
            }
            if (args.Length == 1 && (args[0] == "--version" || args[0] == "-v"))
            {
                Console.WriteLine($"FastEndpointsTool Version v{GetVersion()}");
                return;
            }

            var argument = new Parser().Parse(args);
            await new CodeGenerator().Generate(argument);
        }
        catch (UserFriendlyException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException == null || !(ex.InnerException is UserFriendlyException))
                throw;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.InnerException?.Message}");
            Console.ResetColor();
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("  use like this");
        Console.WriteLine("  fet {command} {options}");
        var arguments = ArgumentInfo.Arguments();
        foreach (var arg in arguments)
        {
            Console.WriteLine();
            Console.WriteLine("  command");
            Console.WriteLine($"  {arg.Name}, {arg.ShortName}, {arg.Description}");
            Console.WriteLine("  options");
            foreach (var opt in arg.Options)
            {
                Console.WriteLine($"    {opt.Name}, {opt.ShortName}, Required: {opt.Required}, {opt.Description}");
            }
        }
    }

    static string GetVersion()
    {
        var versionString = Assembly.GetEntryAssembly()?
                                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                    .InformationalVersion
                                    .ToString();
        if (string.IsNullOrEmpty(versionString))
            throw new Exception("No version found.");
        var pattern = @"\d+\.\d+\.\d+";
        var match = Regex.Match(versionString, pattern);
        if (match.Success)
            return match.Value;
        else
            throw new Exception("No version found.");
    }
}