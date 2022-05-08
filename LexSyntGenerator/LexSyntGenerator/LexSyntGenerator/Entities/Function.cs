using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class Function : Node
    {
        
        public string id { get; set; }

        public Parametrs parametrs { get; set; }

        public Node init { get; set; }

        public Function(string id, Parametrs parametrs, Node init)
        {
            this.id = id;
            this.parametrs = parametrs;
            this.init = init;
        }

        public string tree(int level)
        {
            return new string('\t', level) + "function " + id + ":\n" + parametrs.tree(level + 1) +
                "\n" + new string('\t', level + 1) + "body:\n" + init.tree(level + 2);
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
