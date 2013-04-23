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

        private static ExpandoObject CreatePerson(string name, int age)
        {
            dynamic o = new ExpandoObject();
            o.Name = name;
            o.Age = age;

            o.Child = new ExpandoObject();
            o.Child.Name = "Barn";
            o.Child.Age = 5;

            return o;
        }

        private static dynamic GetFromJsonFile(string jsonFile)
        {
            var serializer = new JsonSerializer();
            using (var reader = new JsonTextReader(File.OpenText(jsonFile)))
            {
                var obj = serializer.Deserialize<dynamic>(reader);

                return obj;
            }
        }

        private static void Main(string[] args)
        {
            var files = new Queue<string>(args);

            var model = GetFromJsonFile(files.Dequeue());

            var referenceEngine = EngineFromFileName(files.Dequeue());
            var engines = files.Select(EngineFromFileName);

            var referenceResult = referenceEngine.Execute(model);
            var referenceHtmlTree = new HtmlDocument();
            referenceHtmlTree.LoadHtml(referenceResult);

            var results = engines.Select(a =>
                                             {
                                                 var engine = a.GetType().Name;
                                                 var html = a.Execute(model);
                                                 var htmlDom = new HtmlDocument();
                                                 htmlDom.LoadHtml(html);

                                                 return new
                                                            {
                                                                Engine = engine,
                                                                Html = html,
                                                                HtmlTree = htmlDom
                                                            };
                                             });

            System.Console.WriteLine("Output from reference engine: ");
            System.Console.WriteLine(referenceResult);
            System.Console.WriteLine("");
            System.Console.WriteLine("");

            foreach (var result in results)
            {
                System.Console.WriteLine("Engine " + result.Engine + " --");
                System.Console.WriteLine(result.Html);
                System.Console.WriteLine("");
                System.Console.WriteLine("Equivalency test: " + (CompareHtmlDocuments(referenceHtmlTree, result.HtmlTree) ? "Passed" : "Failed"));
                System.Console.WriteLine("");
                System.Console.WriteLine("");
            }

            System.Console.WriteLine("PRESS ANY KEY TO TERMINATE");
            System.Console.Read();
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