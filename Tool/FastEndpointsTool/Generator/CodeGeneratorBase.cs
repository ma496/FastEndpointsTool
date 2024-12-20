using System.Text.RegularExpressions;
using FastEndpointsTool.Parsing;

namespace FastEndpointsTool.Generator;

public abstract class CodeGeneratorBase<TArgument>
    where TArgument : Argument
{
    public abstract Task Generate(TArgument argument);

    protected string GetEndpointDir(string projectDir, string endpointPath, string output)
    {
        var endpointDir = Path.Combine(projectDir, endpointPath, output ?? string.Empty);
        return endpointDir;
    }

    protected string GetClassNamespace(string projectDir, string projectName, string className)
    {
        var assembly = Helpers.GetProjectAssembly(projectDir, projectName);
        var types = assembly.GetTypes()
            .Where(t => t.Name == className)
            .ToArray();

        if (types.Length == 0) return string.Empty;
        if (types.Length == 1) return types[0].Namespace ?? string.Empty;
        Console.WriteLine("Multiple types found.");
        var index = 0;
        foreach (var t in types)
        {
            Console.WriteLine($"{t.FullName}: Type {index} then enter");
            index++;
        }
        var indexStr = Console.ReadLine();
        var isInteger = int.TryParse(indexStr, out var selectIndex);
        if (!isInteger)
            throw new UserFriendlyException("input is not integer.");
        if (selectIndex >= 0 && selectIndex < index)
            return types[selectIndex].Namespace ?? string.Empty;
        throw new UserFriendlyException("input number out of range");
    }

    protected string GetEndpointNamespace(string rootNamespace, string endpointPath, string? output)
    {
        // replace slash to dot
        endpointPath = Regex.Replace(endpointPath, @"[\\/]+", ".");
        output = Regex.Replace(output ?? string.Empty, @"[\\/]+", ".");
        // replace multiple dots to one
        endpointPath = Regex.Replace(endpointPath, @"\.+", ".");
        output = Regex.Replace(output ?? string.Empty, @"\.+", ".");
        // remove dot at the beginning
        endpointPath = endpointPath.TrimStart('.');
        output = output?.TrimStart('.');
        return $"{rootNamespace}.{endpointPath}" + (string.IsNullOrWhiteSpace(output) ? string.Empty : $".{output}");
    }

    protected void AddPermissionToAllowClass(string filePath, string permissionName, bool newLineBefore = true)
    {
        // Read the existing Allow.cs file
        var fileContent = File.ReadAllText(filePath);

        // Prepare the new permission string
        var newPermission = $"    public const string {permissionName} = \"{Helpers.UnderscoreToDot(permissionName)}\";";

        // Check if the permission already exists
        if (Regex.IsMatch(fileContent, $@"\s*public\s+const\s+string\s+{permissionName}\s*=\s*""[^""]*""\s*;"))
        {
            Console.WriteLine($"Permission {permissionName} already exists.");
            return;
        }

        // Insert the new permission before the closing brace of the class
        var classEndIndex = fileContent.LastIndexOf("}");
        if (classEndIndex == -1)
        {
            throw new InvalidOperationException("Invalid Allow.cs class format.");
        }

        // Add the new permission just before the closing brace
        var updatedFileContent = fileContent.Substring(0, classEndIndex) + (newLineBefore ? Environment.NewLine : string.Empty) + newPermission + Environment.NewLine + fileContent.Substring(classEndIndex);

        // Save the modified file
        Console.WriteLine($"Add permission {permissionName} to {filePath}");
        File.WriteAllText(filePath, updatedFileContent);
    }
}
