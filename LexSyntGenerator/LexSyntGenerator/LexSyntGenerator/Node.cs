using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public interface Node
    {
        public string tree(int level);
        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs);

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args);
    }
}
