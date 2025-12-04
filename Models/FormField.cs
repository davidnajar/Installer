namespace KairosInstaller.Models;

public class FormField
{
    public string Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Placeholder { get; set; }
    public List<SelectOption>? Options { get; set; }
}

public class SelectOption
{
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}
