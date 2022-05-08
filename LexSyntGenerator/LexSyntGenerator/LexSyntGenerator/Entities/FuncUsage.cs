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
                // AUTO TYPE CASTING
                // AUTO TYPE CASTING
                // AUTO TYPE CASTING
                List<Node> castedArgs = new List<Node>();
                for (int i = 0; i < args.arguments.Count; ++i)
                {
                    dynamic temp = args.arguments[i].interpret(vars, funcs);
                    if (temp is int && func.parametrs.parametrs[i].type == Type.Float)
                    {
                        temp = Convert.ToDouble(temp);
                    }
                    castedArgs.Add(new Number(temp, func.parametrs.parametrs[i].type));
                }
                Node body = func.init.funcArgsReplacment(func.parametrs.parametrs, castedArgs);
                return body.interpret(vars, funcs);
            }
            throw new Exception("Undefined function: " + id + "!");
        }

        public Node funcArgsReplacment(List<(Type type, string id)> parametrs, List<Node> args)
        {
            return new FuncUsage(new string(id), this.args.funcArgsReplacment(parametrs, args) as Arguments);
        }
    }
}
