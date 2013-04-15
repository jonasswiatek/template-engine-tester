using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TemplateEngineTester.Core.Engines
{
    public class PhpEngine : ITemplateEngine
    {
        private readonly string _templateBody;

        public PhpEngine(string templateBody)
        {
            _templateBody = templateBody;
        }

        public string Execute(object model)
        {
            var process = new Process
                              {
                                  StartInfo = new ProcessStartInfo("php.exe", "main.php")
                                                  {
                                                      RedirectStandardInput = true,
                                                      RedirectStandardOutput = true,
                                                      UseShellExecute = false
                                                  }
                              };

            process.Start();

            var templateDescriptor = new PhpTemplateRenderDescription
                                         {
                                             TemplateBody = _templateBody,
                                             Model = model
                                         };

            var serializer = new JsonSerializer();
            using (var streamWriter = new JsonTextWriter(process.StandardInput))
            {
                serializer.Serialize(streamWriter, templateDescriptor);
            }

            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }
    }

    public class PhpTemplateRenderDescription
    {
        public string TemplateBody;
        public object Model;
    }
}
