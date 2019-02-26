using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Utils
    {
        public static List<T> FromEnumberatorToList<T>(IEnumerator<T> enumerator){
            List<T> elements = new List<T>();
            while (enumerator.MoveNext()) {
                elements.Add(enumerator.Current);
            }
            return elements;
        }
    }
}
