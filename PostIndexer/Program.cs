using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Shared.Models;

var contentPath = "../andrewrobilliard/wwwroot/content";
var outputPath = "../andrewrobilliard/wwwroot/posts.json";

var deserializer = new DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .IgnoreUnmatchedProperties()
    .Build();

var posts = Directory.GetFiles(contentPath, "*.md", SearchOption.AllDirectories)
    .Select(file =>
    {
        var content = File.ReadAllText(file);
        var yamlSection = content.Split("---", 3)[1];
        Console.WriteLine(file);
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