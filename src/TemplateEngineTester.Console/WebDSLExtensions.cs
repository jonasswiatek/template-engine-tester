using System.Collections.Generic;

namespace WebDSL
{
    public static class WebDSLExtensions
    {
        public static dynamic GetCollection(object obj)
        {
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