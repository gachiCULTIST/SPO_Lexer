using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class VarUsage : Node
    {
        public string id { get; set; }

        public VarUsage(string id)
        {
            this.id = id;
        }

        public string tree(int level)
        {
            return new string('\t', level) + id;
        }

        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            Variable var = null;
            if (vars.TryGetValue(id, out var))
            {
                return var.interpret(vars, funcs);
            }
            throw new Exception("Undefined variable: " + id + "!");
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            for (int i = 0; i < parametrs.Count; ++i)
            {
                if (this.id.Equals(parametrs[i]))
                {
                    return args[i];
                }
            }
            return this;
        }
    }
}
