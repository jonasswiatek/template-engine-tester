using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;

namespace TemplateEngineTester.Core.Engines
{
    public class RazorEngine : ITemplateEngine
    {
        private readonly string _templateBody;

        public RazorEngine(string templateBody)
        {
            _templateBody = templateBody;
        }

        public string Execute(object model)
        {
            var result = Razor.Parse(_templateBody, model);
            return result;
        }
    }
}
