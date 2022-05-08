using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public enum Type { Int, Float }
    public interface Node
    {
        public string tree(int level);
        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs);

        public Node funcArgsReplacment(List<(Type type, string id)> parametrs, List<Node> args);
    }
}
