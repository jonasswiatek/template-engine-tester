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
    class Program
    {
        static IList<ITemplateEngine> engines = new List<ITemplateEngine>();
 

        static void Main(string[] args)
        {
            //Load up engines
            engines.Add(new PhpEngine(File.ReadAllText("template.php")));
            engines.Add(new MustacheEngine(File.ReadAllText("template.mustache")));

            var model = new {
                                bla = "gay AS FUCK!"
                            };

            foreach (var engine in engines)
            {
                System.Console.WriteLine("Engine result --");
                System.Console.WriteLine(engine.Execute(model));
                System.Console.WriteLine("End Engine result --");
            }

            System.Console.Read();
        }
    }
}