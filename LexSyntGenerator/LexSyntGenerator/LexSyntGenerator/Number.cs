using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    public class Number : Node
    {
        public dynamic value { get; set; }

        public Number() { value = 0; }

        public Number(string literal)
        {
            int integer;
            if (Int32.TryParse(literal, out integer))
            {
                value = integer;
            } else
            {
                value = Double.Parse(literal.Replace('.', ','));
            }
        }

        public string tree(int level)
        {
            return new string('\t', level) + value;
        }

        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            return value;
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            Number num = new Number();
            num.value = this.value;
            return num;
        }
    }
}
