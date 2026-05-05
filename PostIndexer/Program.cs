using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Shared.Models;

// Use the first argument if provided, otherwise fallback to current directory
string projectRoot = args.Length > 0 ? args[0] : Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "andrewrobilliard");

// Clean up the path (removes trailing slashes/quotes)
projectRoot = Path.GetFullPath(projectRoot);

var contentPath = Path.Combine(projectRoot, "wwwroot", "content");
var outputPath = Path.Combine(projectRoot, "wwwroot", "posts.json");

Console.WriteLine($"Scanning content at: {contentPath}");

if (!Directory.Exists(contentPath))
{
    throw new DirectoryNotFoundException($"Critical Error: Could not find content directory at {contentPath}.");
}

var deserializer = new DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .IgnoreUnmatchedProperties()
    .Build();

var posts = Directory.GetFiles(contentPath, "*.md", SearchOption.AllDirectories)
    .Select(file =>
    {
        var content = File.ReadAllText(file);
        var yamlSection = content.Split("---", 3)[1];
        var meta = deserializer.Deserialize<PostMetadata>(yamlSection);
        var contentWithoutYaml = content.Split("---", 3)[2].Trim();
        var description = string.IsNullOrEmpty(meta.Description) ? contentWithoutYaml.Substring(0, Math.Min(147, contentWithoutYaml.Length)) + "..." : meta.Description;

        return new PostSummary(
            meta.Title,
            meta.TitleIcon,
            Path.GetFileNameWithoutExtension(file),
            meta.Date,
            description,
            meta.Type
        );
    })
    .OrderByDescending(p => p.Date)
    .ToList();

File.WriteAllText(outputPath, JsonSerializer.Serialize(posts));
Console.WriteLine($"Post index generated with {posts.Count} posts at: {outputPath}");