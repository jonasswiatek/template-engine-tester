using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngineTester.Core
{
    public interface ITemplateEngine
    {
        string Execute(object model);
    }
}