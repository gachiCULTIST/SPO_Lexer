using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class FuncUsage : Node
    {
        public string id { get; set; }

        public Arguments args { get; set; }

        public FuncUsage(string id, Arguments args)
        {
            this.id = id;
            this.args = args;
        }

        public string tree(int level)
        {
            return new string('\t', level) + id + ":\n" + args.tree(level + 1);
        }

        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            Function func = null;
            if (funcs.TryGetValue(id, out func))
            {
                if (func.parametrs.parametrs.Count != args.arguments.Count)
                {
                    throw new Exception("Неправильное количество аргументов в функции " + id + "!");
                }
                Node body = func.init.funcArgsReplacment(func.parametrs.parametrs, args.arguments);
                return body.interpret(vars, funcs);
            }
            throw new Exception("Undefined function: " + id + "!");
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            return new FuncUsage(new string(id), this.args.funcArgsReplacment(parametrs, args) as Arguments);
        }
    }
}
