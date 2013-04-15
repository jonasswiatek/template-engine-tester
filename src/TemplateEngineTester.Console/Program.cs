using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngineTester.Core;
using TemplateEngineTester.Core.Engines;

namespace ITU.SMDP2013.TemplateEngineTester.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ITemplateEngine phpEngine = new PhpEngine(
                @"Hej: <?=$model->bla?>"
                );

            var result = phpEngine.Execute(new
                                               {
                                                   bla = "gay AS FUCK!"
                                               });

            System.Console.Write(result);
            System.Console.Read();
        }
    }
}