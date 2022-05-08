using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public enum VarType { Integer, Double }

    public class Variable : Node
    {
        
        public string id { get; set; }

        public Node init { get; set; }

        public Variable(string id, Node expression)
        {
            this.id = id;
            this.init = expression;
        }

        public string tree(int level)
        {
            return new string('\t', level) + this.id + ":\n" + init.tree(level + 1);
        }

        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            return init.interpret(vars, funcs);
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            throw new NotImplementedException();
        }
    }
}
