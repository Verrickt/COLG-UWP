using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RichTextblockTest : Page
    {
        public RichTextblockTest()
        {
            this.InitializeComponent();
        }

        public void Parse(string html)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            parse(document);
        }

        private void parse(HtmlAgilityPack.HtmlDocument document)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
        }

        private void Parse(HtmlAgilityPack.HtmlNode node, List<Paragraph> paragraphs, ref Paragraph paragraph, ref Span span)
        {
            if (node.Name == "#strong")
            {
            }
        }

        private Run ParseStrong(HtmlAgilityPack.HtmlNode node)
        {
            if (node.Name != "strong")
            {
                throw new System.Exception("Node is not of type strong!");
            }

            Run run = new Run();
            run.FontWeight = FontWeights.Bold;
            run.Text = node.InnerText;
            return run;
        }

        private Run ParseText(HtmlAgilityPack.HtmlNode node)
        {
            if (node.Name != "#text")
            {
                throw new System.Exception("Node is not of type text!");
            }

            Run run = new Run();
            run.Text = node.InnerText;
            return run;
        }

        private LineBreak ParseBr(HtmlAgilityPack.HtmlNode node)
        {
            if (node.Name != "br")
            {
                throw new System.Exception("Node is not of type br!");
            }

            return new LineBreak();
        }

        private Image ParseImg(HtmlAgilityPack.HtmlNode node)
        {
            return null;
        }
    }
}