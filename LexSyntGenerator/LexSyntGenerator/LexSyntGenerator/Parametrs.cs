using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class Parametrs : Node
    {
        public List<string> parametrs = new List<string>();

        public Parametrs(string param)
        {
            parametrs.Add(param);
        }

        public Parametrs() { }

        public Parametrs addParametr(string param)
        {
            parametrs.Add(param);
            return this;
        }

        public string tree(int level)
        {
            StringBuilder result = new StringBuilder(new string('t', level) + "params:");
            foreach (string str in parametrs)
            {
                result.Append("\n" + new string('\t', level + 1) + str);
            }
            return result.ToString();
        }
        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            throw new NotImplementedException();
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            throw new NotImplementedException();
        }
    }
}
