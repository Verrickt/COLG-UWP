using System;
using System.Linq;

namespace Colg_UWP.Service
{
    using Colg_UWP.Util;
    using HtmlAgilityPack;

    public static class Html2Markdown
    {
        public static string ToMarkdown(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            return ToMarkdown(document.DocumentNode, false, true);
        }

        private static string ToMarkdown(HtmlNode node, bool isQuote, bool isFirst)
        {
            string markdown = isQuote && isFirst ? ">" : string.Empty;
            if (node.Name == "#document")
            {
                var first = ToMarkdown(node.FirstChild, false, true);
                var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, false, false));
                var whole = first.GetSingle().Concat(remaining);
                markdown = string.Join(String.Empty, whole);
            }
            else
            if (node.Name == "br")
            {
                if (isQuote)
                {
                    markdown = Environment.NewLine + ">" + Environment.NewLine + ">";
                }
                else
                {
                    markdown = Environment.NewLine + Environment.NewLine;
                }
            }
            else if (node.Name == "strong")
            {
                if (!string.IsNullOrWhiteSpace(node.InnerText))
                {
                    markdown = ($"**{node.InnerText}**");
                }
                else
                {
                    markdown = string.Empty;
                }
            }
            else if (node.Name == "#text")
            {
                if (string.IsNullOrWhiteSpace(node.InnerText))
                {
                    markdown = string.Empty;
                }
                else
                {
                    markdown += node.InnerText;
                }
            }
            else if (node.Name == "span")
            {
                if (string.IsNullOrWhiteSpace(node.InnerText))
                {
                    markdown = string.Empty;
                }
                else
                {
                    string newline = Environment.NewLine;

                    var first = ToMarkdown(node.FirstChild, isQuote, isFirst);
                    var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, isQuote, false));
                    var whole = newline.GetSingle().ConcatMany(first.GetSingle(), remaining, newline.GetSingle());
                    markdown = string.Join(String.Empty, whole);
                }
            }
            else if (node.Name == "a")
            {
                var first = ToMarkdown(node.FirstChild, isQuote, isFirst);
                var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, isQuote, false));
                var whole = first.GetSingle().Concat(remaining);
                markdown = string.Join(String.Empty, whole);
            }
            else if (node.Name == "font")
            {
                var first = ToMarkdown(node.FirstChild, isQuote, isFirst);
                var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, isQuote, false));
                var whole = first.GetSingle().Concat(remaining);
                markdown = string.Join(String.Empty, whole);
            }
            else if (node.Name == "div")
            {
                if (node.Attributes.Any(x => x.Value == "reply_wrap"))
                {
                    var newline = Environment.NewLine;
                    var first = ToMarkdown(node.FirstChild, true, true);
                    var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, true, false));
                    var whole = first.GetSingle().ConcatMany(remaining, newline.GetSingle(), newline.GetSingle());
                    markdown = string.Join(String.Empty, whole);
                }
                else
                {
                    var first = ToMarkdown(node.FirstChild, true, false);
                    var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, true, false));
                    var whole = first.GetSingle().Concat(remaining);
                    markdown = string.Join(String.Empty, whole);
                }
            }
            else if (node.Name == "p")
            {
                string before;
                if (isQuote)
                {
                    before = Environment.NewLine + ">" + Environment.NewLine + ">";
                }
                else
                {
                    before = Environment.NewLine + Environment.NewLine;
                }
                string after = before;

                var first = ToMarkdown(node.FirstChild, isQuote, isFirst);
                var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, isQuote, false));
                var whole = before.GetSingle().ConcatMany(first.GetSingle(), remaining, after.GetSingle());
                markdown = string.Join(String.Empty, whole);
            }
            else if (node.Name == "section")
            {
                var first = ToMarkdown(node.FirstChild, isQuote, isFirst);
                var remaining = node.ChildNodes.Skip(1).Select(x => ToMarkdown(x, isQuote, false));
                var whole = first.GetSingle().Concat(remaining);
                markdown = string.Join(String.Empty, whole);
            }
            else
            {
                markdown = string.Empty;
            }

            if (isQuote && markdown == ">")
            {
                markdown = string.Empty;
            }
            return markdown;
        }
    }
}