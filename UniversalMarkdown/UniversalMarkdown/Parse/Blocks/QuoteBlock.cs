﻿// Copyright (c) 2016 Quinn Damerell
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.


using System.Collections.Generic;

namespace UniversalMarkdown.Parse.Elements
{
    /// <summary>
    /// Represents a block that is displayed using a quote style.  Quotes are used to indicate
    /// that the text originated elsewhere (e.g. a previous comment).
    /// </summary>
    public class QuoteBlock : MarkdownBlock
    {
        /// <summary>
        /// Initializes a new quote block.
        /// </summary>
        public QuoteBlock() : base(MarkdownBlockType.Quote)
        {
        }

        /// <summary>
        /// The contents of the block.
        /// </summary>
        public IList<MarkdownBlock> Blocks { get; set; }

        /// <summary>
        /// Parses a quote block.
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="startOfLine"> The location of the start of the line. </param>
        /// <param name="maxEnd"> The location to stop parsing. </param>
        /// <param name="quoteDepth"> The current nesting level of quotes. </param>
        /// <param name="actualEnd"> Set to the end of the block when the return value is non-null. </param>
        /// <returns> A parsed quote block. </returns>
        internal static QuoteBlock Parse(string markdown, int startOfLine, int maxEnd, int quoteDepth, out int actualEnd)
        {
            var result = new QuoteBlock();

            // Recursively call into the markdown block parser.
            result.Blocks = MarkdownDocument.Parse(markdown, startOfLine, maxEnd, quoteDepth: quoteDepth + 1, actualEnd: out actualEnd);

            return result;
        }
    }
}
