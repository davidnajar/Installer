namespace KairosInstaller.Models;

public class FormSchema
{
    public string Title { get; set; } = string.Empty;
    public List<FormStep> Steps { get; set; } = new();
}
