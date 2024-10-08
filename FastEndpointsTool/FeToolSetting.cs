namespace FastEndpointsTool;

public class FeToolSetting
{
    public ProjectSetting Project { get; set; } = null!;

    public void Validate()
    {
        if (Project == null)
            throw new Exception($"{nameof(Project)} can not be null.");
        if (string.IsNullOrWhiteSpace(Project.Name))
            throw new Exception($"{nameof(Project)}.{nameof(Project.Name)} can not be empty.");
        if (string.IsNullOrWhiteSpace(Project.EndpointPath))
            throw new Exception($"{nameof(Project)}.{nameof(Project.EndpointPath)} can not be empty.");
        if (string.IsNullOrWhiteSpace(Project.RootNamespace))
            throw new Exception($"{nameof(Project)}.{nameof(Project.RootNamespace)} can not be empty.");
    }
}

public class ProjectSetting
{
    public string Name { get; set; } = null!;
    public string EndpointPath { get; set; } = null!;
    public string RootNamespace { get; set; } = null!;
}
