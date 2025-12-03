using KairosInstaller.Models;

namespace KairosInstaller.Services;

public interface IFormSchemaService
{
    FormSchema Schema { get; }
    Task SaveResultAsync(Dictionary<string, object> data, string outputPath);
}
