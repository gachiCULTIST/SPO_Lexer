using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lexer;

namespace LexSyntGenerator
{
    class UnOperator : Node
    {
        public TokenType op { get; set; }

        public Node operand { get; set; }

        public UnOperator(TokenType op, Node operand)
        {
            this.op = op;
            this.operand = operand;
        }

        public string tree(int level)
        {
            return new string('\t', level) + this.op + ":\n" + operand.tree(level + 1);
        }

        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            switch (op)
            {
                case TokenType.Tok_uminus:
                    return -operand.interpret(vars, funcs);
                default:
                    throw new Exception("Unsupported operation: " + op + "!");
            }
        }

        public Node funcArgsReplacment(List<(Type type, string id)> parametrs, List<Node> args)
        {
            return new UnOperator(op, operand.funcArgsReplacment(parametrs, args));
        }
    }
}
