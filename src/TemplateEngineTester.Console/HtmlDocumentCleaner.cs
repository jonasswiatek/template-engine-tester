using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ITU.SMDP2013.TemplateEngineTester.Console
{
    public static class HtmlDocumentCleaner
    {
        public static void VisitNode(HtmlNode htmlNode)
        {
            if (htmlNode is HtmlTextNode)
            {
                VisitNode(htmlNode as HtmlTextNode);
            }

            foreach (var node in htmlNode.ChildNodes)
            {
                VisitNode(node);
            }
        }

        public static void VisitNode(HtmlTextNode textNode)
        {
            textNode.Text = textNode.Text.Trim();
        }
    }
}
