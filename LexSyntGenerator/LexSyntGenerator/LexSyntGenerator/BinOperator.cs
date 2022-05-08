using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lexer;

namespace LexSyntGenerator
{
    class BinOperator : Node
    {
        public TokenType op { get; set; }

        public Node operand1 { get; set; }
        public Node operand2 { get; set; }

        public BinOperator(TokenType op, Node operand1, Node operand2)
        {
            this.op = op;
            this.operand1 = operand1;
            this.operand2 = operand2;
        }

        public string tree(int level)
        {
            return new string('\t', level) + this.op + ":\n" + operand1.tree(level + 1) + "\n" + operand2.tree(level + 1);
        }

        public dynamic interpret(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            switch (op)
            {
                case TokenType.Tok_plus:
                    return operand1.interpret(vars, funcs) + operand2.interpret(vars, funcs);
                case TokenType.Tok_minus:
                    return operand1.interpret(vars, funcs) - operand2.interpret(vars, funcs);
                case TokenType.Tok_multiply:
                    return operand1.interpret(vars, funcs) * operand2.interpret(vars, funcs);
                case TokenType.Tok_division:
                    return operand1.interpret(vars, funcs) / operand2.interpret(vars, funcs);
                case TokenType.Tok_pow:
                    return Math.Pow(operand1.interpret(vars, funcs), operand2.interpret(vars, funcs));
                default:
                    throw new Exception("Unsupported operation: " + op + "!");
            }
        }

        public Node funcArgsReplacment(List<string> parametrs, List<Node> args)
        {
            return new BinOperator(op, operand1.funcArgsReplacment(parametrs, args), operand2.funcArgsReplacment(parametrs, args));
        }
    }
}
