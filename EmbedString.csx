#r "nuget: Lestaly.General, 0.104.0"
#nullable enable
using Lestaly;

{
    var embeder = new EmbedStringBuilder();
    embeder.Variables["sha"] = "e5e3f590d64ed05b802ec7de79fc67f87105fc2e";
    embeder.Variables["date"] = $"{DateTime.Now:yyyyMMdd-HHmmss}";

    WriteLine(embeder.Build("SHA=${sha}/DATE=${date}"));
    WriteLine(embeder.Build("DATE: $date - $sha"));
}
