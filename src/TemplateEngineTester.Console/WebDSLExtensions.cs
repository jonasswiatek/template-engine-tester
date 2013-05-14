using System.Collections.Generic;

namespace WebDSL
{
    public static class WebDSLExtensions
    {
        public static dynamic GetCollection(dynamic baseModel, object obj)
        {
            if (obj is bool)
            {
                var b = (bool)obj;
                if (b)
                {
                    return new dynamic[] {baseModel};
                }
            }

            var list = obj as IEnumerable<dynamic>;
            if (list == null)
            {
                list = new List<dynamic>(new dynamic[]
                                             {
                                                obj
                                             });
            }
            return list;
        }
    }
}