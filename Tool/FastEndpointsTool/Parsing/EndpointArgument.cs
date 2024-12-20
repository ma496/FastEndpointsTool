namespace FastEndpointsTool.Parsing;

public class EndpointArgument : Argument, ICloneable
{
    public string Name { get; set; } = null!;
    public string Method { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Entity { get; set; } = null!;
    public string EntityFullName { get; set; } = null!;
    public string Output { get; set; } = null!;
    public string Group { get; set; } = null!;
    public string GroupFullName { get; set; } = null!;
    public string PluralName { get; set; } = null!;
    public string DataContext { get; set; } = null!;
    public string DataContextFullName { get; set; } = null!;
    public string Namespace { get; set; } = null!;
    public List<string> UsingNamespaces { get; set; } = new();
    public string BaseProperties { get; set; } = "false"; // "true" or "false"
    public string Authorization { get; set; } = "false"; // "true" or "false"
    public string Permission { get; set; } = null!;

    public object Clone()
    {
        return MemberwiseClone();
    }
}

