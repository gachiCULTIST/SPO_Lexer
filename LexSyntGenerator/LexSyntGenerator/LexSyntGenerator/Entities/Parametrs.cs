using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class Parametrs : Node
    {
        public List<(Type type, string id)> parametrs = new List<(Type type, string id)>();

        public Parametrs(string type, string param)
        {
            if (type.Equals("int"))
            {
                parametrs.Add((Type.Int, param));
            }
            else if (type.Equals("float"))
            {
                parametrs.Add((Type.Float, param));
            } else
            {
                throw new Exception("Undefined type: " + type + "!");
            }
        }

        public Parametrs() { }

        public Parametrs addParametr(string type, string param)
        {
            if (type.Equals("int"))
            {
                parametrs.Add((Type.Int, param));
            }
            else if (type.Equals("float"))
            {
                parametrs.Add((Type.Float, param));
            }
            else
            {
                throw new Exception("Undefined type: " + type + "!");
            }
            return this;
        }

        public string tree(int level)
        {
            StringBuilder result = new StringBuilder(new string('t', level) + "params:");
            foreach ((Type t,string str) in parametrs)
            {
                result.Append("\n" + new string('\t', level + 1) + str + ":" + t);
            }
            return result.ToString();
        }
        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            throw new NotImplementedException();
        }

        public Node funcArgsReplacment(List<(Type type, string id)> parametrs, List<Node> args)
        {
            throw new NotImplementedException();
        }
    }
}
