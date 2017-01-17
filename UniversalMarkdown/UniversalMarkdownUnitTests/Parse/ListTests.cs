﻿using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using UniversalMarkdown.Parse;
using UniversalMarkdown.Parse.Elements;
using UITestMethodAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.UITestMethodAttribute;

namespace UniversalMarkdownUnitTests.Parse
{
    [TestClass]
    public class ListTests : ParseTestBase
    {
        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_SingleLine()
        {
            AssertEqual("- List",
                new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List" }))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Simple()
        {
            AssertEqual(CollapseWhitespace(@"
                before

                - List item 1
                * List item 2
                + List item 3

                after"),
                new ParagraphBlock().AddChildren(
                    new TextRunInline { Text = "before" }),
                new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" })),
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" })),
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))),
                new ParagraphBlock().AddChildren(
                    new TextRunInline { Text = "after" }));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_BlankLineIsOkay()
        {
            AssertEqual(CollapseWhitespace(@"
                * List item 1

                * List item 2"),
                new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" })),
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" }))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_WithBlocks()
        {
            AssertEqual(CollapseWhitespace(@"
                * #Header

                 ___
                *  #Not a header

                 ___"),
                new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                    new ListItemBlock().AddChildren(
                        new HeaderBlock { HeaderLevel = 1 }.AddChildren(new TextRunInline { Text = "Header" }),
                        new HorizontalRuleBlock()),
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = " #Not a header" }),
                        new HorizontalRuleBlock())));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_Simple()
        {
            AssertEqual(CollapseWhitespace(@"
                - List item 1
                    - Nested item
                + List item 2"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "Nested item" })))),
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" }))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_Complex()
        {
            // This is super weird.
            AssertEqual(CollapseWhitespace(@"
                - #Level 1
                - #Level 1
                    - #Level 2
                        - #Level 3
                            - #Level 4  
                level 4, line 2

                     text"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "#Level 1" })),
                    new ListItemBlock().AddChildren(
                        new HeaderBlock { HeaderLevel = 1 }.AddChildren(new TextRunInline { Text = "Level 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new HeaderBlock { HeaderLevel = 1 }.AddChildren(new TextRunInline { Text = "Level 2" }),
                                new ListBlock().AddChildren(
                                    new ListItemBlock().AddChildren(
                                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "#Level 3" }),
                                        new ListBlock().AddChildren(
                                            new ListItemBlock().AddChildren(
                                                new ParagraphBlock().AddChildren(new TextRunInline { Text = "#Level 4\r\nlevel 4, line 2" }))))),
                                new ParagraphBlock().AddChildren(new TextRunInline { Text = "text" }))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_MinSpace()
        {
            // 1 space is the min relative indentation.
            AssertEqual(CollapseWhitespace(@"
                - List item 1
                 - Nested item"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "Nested item" }))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_MaxSpace()
        {
            // 7 spaces is the max relative indentation for two items.
            AssertEqual(CollapseWhitespace(@"
                - List item 1
                       - Nested item"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "Nested item" }))))));

            // 11 spaces is the max relative indentation for three items.
            AssertEqual(CollapseWhitespace(@"
                - List item 1
                 - List item 2
                           - List item 3"),
            new ListBlock().AddChildren(
                new ListItemBlock().AddChildren(
                    new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                    new ListBlock().AddChildren(
                        new ListItemBlock().AddChildren(
                            new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" }),
                            new ListBlock().AddChildren(
                                new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_SpaceDifference()
        {
            // This is weird.
            AssertEqual(CollapseWhitespace(@"
                 - List item 1
                - Nested item"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "Nested item" }))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_Combo()
        {
            AssertEqual(CollapseWhitespace(@"
                   - List item 1
                - List item 2
                - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" })),
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))));
            AssertEqual(CollapseWhitespace(@"
                   - List item 1
                - List item 2
                  - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" })),
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))));
            AssertEqual(CollapseWhitespace(@"
                   - List item 1
                - List item 2
                   - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" })))),
                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))));
            AssertEqual(CollapseWhitespace(@"
                   - List item 1
                - List item 2
                    - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" })),
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))));
            AssertEqual(CollapseWhitespace(@"
                   - List item 1
                - List item 2
                     - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" }),
                                new ListBlock().AddChildren(
                                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))))));
            AssertEqual(CollapseWhitespace(@"
                - List item 1
                 - List item 2
                    - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" })),
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))));
            AssertEqual(CollapseWhitespace(@"
                - List item 1
                 - List item 2
                     - List item 3"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" }),
                                new ListBlock().AddChildren(
                                    new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }))))))));
            AssertEqual(CollapseWhitespace(@"
                - 1
                 - 2
                - 3
                 - 4"),
                new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "1" }),
                        new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "2" })))),
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "3" }),
                        new ListBlock { Style = ListStyle.Bulleted }.AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "4" }))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Nested_Paragraph()
        {
            AssertEqual(CollapseWhitespace(@"
                * 1
                 * 2

                 3"),
            new ListBlock().AddChildren(
                new ListItemBlock().AddChildren(
                    new ParagraphBlock().AddChildren(new TextRunInline { Text = "1" }),
                    new ListBlock().AddChildren(
                        new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "2" }))),
                    new ParagraphBlock().AddChildren(new TextRunInline { Text = "3" }))));

            AssertEqual(CollapseWhitespace(@"
                * 1
                 * 2
                * 3

                     4"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "1" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(new ParagraphBlock().AddChildren(new TextRunInline { Text = "2" })))),
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = "3" }),
                        new ParagraphBlock().AddChildren(new TextRunInline { Text = " 4" }))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_NestedLists()
        {
            // No blank line means only the last line actually has a bullet.
            AssertEqual(CollapseWhitespace(@"
1. * Ordered list item 1
2. * Bullet 1 in list item 2
    * Bullet 2 in list item 2"),
                new ListBlock { Style = ListStyle.Numbered }.AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(
                            new TextRunInline { Text = "* Ordered list item 1" })),
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(
                            new TextRunInline { Text = "* Bullet 1 in list item 2" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(
                                    new TextRunInline { Text = "Bullet 2 in list item 2" }))))));

            // But if you put a blank line in there it works.
            AssertEqual(CollapseWhitespace(@"
1. * Ordered list item 1

2. * Bullet 1 in list item 2
    * Bullet 2 in list item 2"),
                new ListBlock { Style = ListStyle.Numbered }.AddChildren(
                    new ListItemBlock().AddChildren(
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(
                                    new TextRunInline { Text = "Ordered list item 1" })))),
                    new ListItemBlock().AddChildren(
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(
                                    new TextRunInline { Text = "Bullet 1 in list item 2" })),
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(
                                    new TextRunInline { Text = "Bullet 2 in list item 2" }))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Negative_SpaceRequired()
        {
            // The space is required.
            AssertEqual("-List",
                new ParagraphBlock().AddChildren(
                    new TextRunInline { Text = "-List" }));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Negative_NewParagraph()
        {
            // Bulleted lists must start on a new paragraph
            AssertEqual(CollapseWhitespace(@"
                before
                * List
                after"),
                new ParagraphBlock().AddChildren(
                    new TextRunInline { Text = "before * List after" }));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void BulletedList_Negative_TooMuchSpaceToBeNested()
        {
            // 7 spaces is the maximum indentation for two items.
            AssertEqual(CollapseWhitespace(@"
                * a
                        * b"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(
                            new TextRunInline { Text = "a * b" }))));

            // 11 spaces is the maximum indentation for three items.
            AssertEqual(CollapseWhitespace(@"
                * a
                 * b
                            * c"),
                new ListBlock().AddChildren(
                    new ListItemBlock().AddChildren(
                        new ParagraphBlock().AddChildren(
                            new TextRunInline { Text = "a" }),
                        new ListBlock().AddChildren(
                            new ListItemBlock().AddChildren(
                                new ParagraphBlock().AddChildren(
                                    new TextRunInline { Text = "b * c" }))))));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void NumberedList_SingleLine()
        {
            AssertEqual("1. List",
                new ListBlock { Style = ListStyle.Numbered }.AddChildren(
                    new ListItemBlock { Blocks = new List<MarkdownBlock> { new ParagraphBlock().AddChildren(new TextRunInline { Text = "List" }) } }));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void NumberedList_Numbering()
        {
            // The numbers are ignored, and they can be any length.
            AssertEqual(CollapseWhitespace(@"
                7. List item 1
                502. List item 2
                502456456456456456456456456456456456. List item 3"),
                new ListBlock { Style = ListStyle.Numbered }.AddChildren(
                    new ListItemBlock { Blocks = new List<MarkdownBlock> { new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 1" }) } },
                    new ListItemBlock { Blocks = new List<MarkdownBlock> { new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 2" }) } },
                    new ListItemBlock { Blocks = new List<MarkdownBlock> { new ParagraphBlock().AddChildren(new TextRunInline { Text = "List item 3" }) } }));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void NumberedList_Negative_SpaceRequired()
        {
            // A space is required after the dot.
            AssertEqual("1.List", new ParagraphBlock().AddChildren(
                new TextRunInline { Text = "1.List" }));
        }

        [UITestMethod]
        [TestCategory("Parse - block")]
        public void NumberedList_Negative_NoLetters()
        {
            // Only digits can make a numbered list.
            AssertEqual("a. List", new ParagraphBlock().AddChildren(
                new TextRunInline { Text = "a. List" }));
        }
    }
}
