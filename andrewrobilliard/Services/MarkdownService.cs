using Markdig;
using Microsoft.AspNetCore.Components;

public interface IMarkdownService
{
  MarkupString ConvertToHtml(string markdown);
}

public class MarkdownService : IMarkdownService
{
  private readonly MarkdownPipeline _pipeline;

  public MarkdownService()
  {
    // Configure the pipeline with advanced extensions (Tables, Citations, etc.)
    _pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseEmojiAndSmiley()
        .Build();
  }

  public MarkupString ConvertToHtml(string markdown)
  {
    if (string.IsNullOrWhiteSpace(markdown))
      return new MarkupString(string.Empty);

    // Convert Markdown string to HTML string
    var html = Markdown.ToHtml(markdown, _pipeline);

    // Wrap in MarkupString so Blazor renders it as HTML rather than literal text
    return new MarkupString(html);
  }
}