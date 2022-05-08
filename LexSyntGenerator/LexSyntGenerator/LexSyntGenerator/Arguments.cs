using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class Arguments : Node
    {
        public List<Node> arguments = new List<Node>();

        public Arguments(Node arg)
        {
            arguments.Add(arg);
        }

        public Arguments() { }

        public Arguments addArgument(Node arg)
        {
            arguments.Add(arg);
            return this;
        }

        public string tree(int level)
        {
            StringBuilder result = new StringBuilder(new string('\t', level) + "args:");
            foreach (Node n in arguments)
            {
                result.Append("\n" + n.tree(level + 1));
            }
            return result.ToString();
        }
        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            throw new NotImplementedException();
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            Arguments newArgs = new Arguments();
            foreach(Node n in arguments)
            {
                newArgs.addArgument(n.funcArgsReplacment(parametrs, args));
            }
            return newArgs;
        }
    }
}
