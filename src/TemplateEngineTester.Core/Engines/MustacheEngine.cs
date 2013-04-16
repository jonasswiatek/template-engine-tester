using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nustache.Core;

namespace TemplateEngineTester.Core.Engines
{
    public class MustacheEngine : ITemplateEngine
    {
        private readonly string _templateBody;

        public MustacheEngine(string templateBody)
        {
            _templateBody = templateBody;
        }

        public string Execute(object model)
        {
            var result = Render.StringToString(_templateBody, model);

            return result;
        }
    }
}
