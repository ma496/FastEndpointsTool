namespace FastEndpointsTool.Parsing.Endpoint;

public class EndpointArgument : Argument
{
    public EndpointType Type { get; set; }
    public string Name { get; set; } = null!;
    public string Method { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Entity { get; set; } = null!;
    public string EntityFullName { get; set; } = null!;
    public string Output { get; set; } = null!;
    public string Group { get; set; } = null!;
    public string GroupFullName { get; set; } = null!;
}

