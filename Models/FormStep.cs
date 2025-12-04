namespace KairosInstaller.Models;

public class FormStep
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<FormField> Fields { get; set; } = new();
}
