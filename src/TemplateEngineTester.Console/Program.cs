using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TemplateEngineTester.Core;
using TemplateEngineTester.Core.Engines;

namespace ITU.SMDP2013.TemplateEngineTester.Console
{
    internal class Program
    {
        private static dynamic GetFromJsonFile(string jsonFile)
        {
            var serializer = new JsonSerializer();
            using (var reader = new JsonTextReader(File.OpenText(jsonFile)))
            {
                var obj = serializer.Deserialize<ExpandoObject>(reader);

                return obj;
            }
        }

        private static void Main(string[] args)
        {
            TestTemplate("templates/template00");
            TestTemplate("templates/template01");
            TestTemplate("templates/template02");
            TestTemplate("templates/template03");
            TestTemplate("templates/template04");
            //TestTemplate("templates/template05"); Wrapped functions not supported
            TestTemplate("templates/template06");
            TestTemplate("templates/template07"); //Helper must use HasValues in Razor instead of Any() and php must use length and not length()

            System.Console.WriteLine("PRESS ANY KEY TO TERMINATE");
            System.Console.Read();
        }

        private static void TestTemplate(string templateBaseName)
        {
            var model = GetFromJsonFile(templateBaseName + ".json");

            var referenceEngine = EngineFromFileName(templateBaseName + ".mustache");
            var engines = new []{".cshtml", ".php"}.Select(a => EngineFromFileName(templateBaseName + a)).Where(a => a != null);

            var referenceResult = referenceEngine.Execute(model);
            var referenceHtmlTree = new HtmlDocument();
            referenceHtmlTree.LoadHtml(referenceResult);

            var results = engines.Select(a =>
                                             {
                                                 var success = true;

                                                 var engine = a.GetType().Name;
                                                 var html = string.Empty;
                                                 var htmlDom = new HtmlDocument();

                                                 try
                                                 {
                                                     html = a.Execute(model);
                                                     htmlDom.LoadHtml(html);
                                                 }
                                                 catch
                                                 {
                                                     success = false;
                                                 }

                                                 return new
                                                            {
                                                                success,
                                                                Engine = engine,
                                                                Html = html,
                                                                HtmlTree = htmlDom
                                                            };

                                             });

            System.Console.WriteLine("Template: " + templateBaseName);

            foreach (var result in results)
            {
                if (result.success)
                {
                    System.Console.WriteLine("\t" + result.Engine + ": " + (CompareHtmlDocuments(referenceHtmlTree, result.HtmlTree) ? "Passed" : "Failed"));
                }
                else
                {
                    System.Console.WriteLine("\t" + result.Engine + ": FATAL");
                }
            }
        }

        private static bool CompareHtmlDocuments(HtmlDocument first, HtmlDocument second)
        {
            var firstString = GetHtmlString(first);
            var secondString = GetHtmlString(second);

            return firstString == secondString;
        }

        private static string GetHtmlString(HtmlDocument document)
        {
            HtmlDocumentCleaner.VisitNode(document.DocumentNode);

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                document.Save(writer);
            }

            return sb.ToString();
        }

        private static ITemplateEngine EngineFromFileName(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }

            if (fileName.EndsWith(".mustache"))
            {
                return new MustacheEngine(File.ReadAllText(fileName));
            }
            else if (fileName.EndsWith(".cshtml"))
            {
                return new RazorEngine(File.ReadAllText(fileName));
            }
            else if (fileName.EndsWith(".php"))
            {
                return new PhpEngine(File.ReadAllText(fileName));
            }
            else
            {
                throw new ArgumentException("Could not determine template engine for " + fileName, "fileName");
            }
        }
    }
}