using System.Text.Json;
using KairosInstaller.Models;

namespace KairosInstaller.Services;

public class FormSchemaService : IFormSchemaService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<FormSchemaService> _logger;
    private FormSchema? _schema;

    public FormSchemaService(IWebHostEnvironment environment, ILogger<FormSchemaService> logger)
    {
        _environment = environment;
        _logger = logger;
        LoadSchema();
    }

    public FormSchema Schema => _schema ?? new FormSchema();

    private void LoadSchema()
    {
        try
        {
            var schemaPath = Path.Combine(_environment.ContentRootPath, "config", "form-schema.json");
            
            if (!File.Exists(schemaPath))
            {
                _logger.LogError("Schema file not found at {Path}", schemaPath);
                _schema = new FormSchema { Title = "Error: Schema not found" };
                return;
            }

            var jsonContent = File.ReadAllText(schemaPath);
            _schema = JsonSerializer.Deserialize<FormSchema>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (_schema == null)
            {
                _logger.LogError("Failed to deserialize schema");
                _schema = new FormSchema { Title = "Error: Invalid schema" };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading schema");
            _schema = new FormSchema { Title = "Error loading schema" };
        }
    }

    public async Task SaveResultAsync(Dictionary<string, object> data, string outputPath)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonContent = JsonSerializer.Serialize(data, options);
            
            var directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllTextAsync(outputPath, jsonContent);
            _logger.LogInformation("Configuration saved to {Path}", outputPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving configuration to {Path}", outputPath);
            throw;
        }
    }
}
