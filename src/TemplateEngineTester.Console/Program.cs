using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngineTester.Core;
using TemplateEngineTester.Core.Engines;

namespace ITU.SMDP2013.TemplateEngineTester.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var files = new Queue<string>(args);

            var referenceEngine = EngineFromFileName(files.Dequeue());
            var engines = files.Select(EngineFromFileName);

            var model = new
                            {
                                bla = "Lovely lovely text!"
                            };

            var referenceResult = referenceEngine.Execute(model);
            var results = engines.Select(a => new
                                                  {
                                                      Engine = a.GetType().Name,
                                                      Html = a.Execute(model)
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
                System.Console.WriteLine("");
            }

            System.Console.Read();
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