using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Shared.Models;

namespace andrewrobilliard.Helpers;

public class PostParser
{
  private readonly IDeserializer _yamlDeserializer;

  public PostParser()
  {
    _yamlDeserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();
  }

  public BlogPost Parse(string rawFileContent)
  {
    // Metadata is between the first two '---' lines
    var parts = rawFileContent.Split("---", 3, StringSplitOptions.RemoveEmptyEntries);

    if (parts.Length < 2)
    {
      // No frontmatter found, return content only
      return new BlogPost { Content = rawFileContent };
    }

    var yamlSection = parts[0];
    var markdownSection = parts[1];

    var metadata = _yamlDeserializer.Deserialize<PostMetadata>(yamlSection);

    return new BlogPost
    {
      Metadata = metadata,
      Content = markdownSection.Trim()
    };
  }
}