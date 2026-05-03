namespace Shared.Models;

public record PostSummary(string Title, string TitleIcon, string Slug, DateTime Date, string Description, string PostType = "blog");