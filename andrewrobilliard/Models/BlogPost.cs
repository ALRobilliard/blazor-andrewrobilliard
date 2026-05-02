public record PostMetadata
{
  public string Title { get; init; } = string.Empty;
  public DateTime Date { get; init; }
  public List<string> Tags { get; init; } = new();
  public string Description { get; init; } = string.Empty;
  public PostType PostType { get; init; } = PostType.Blog;
  public List<ProjectIcon> ProjectIcons { get; init; } = new();
  public string ProjectLink { get; init; } = string.Empty;
}

public record BlogPost
{
  public PostMetadata Metadata { get; init; } = new();
  public string Content { get; init; } = string.Empty;
}

public record ProjectIcon
{
  public string Name { get; init; } = string.Empty;
  public string Class { get; init; } = string.Empty;
}

public enum PostType
{
  Blog,
  Project
}