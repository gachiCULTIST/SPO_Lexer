using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexSyntGenerator
{
    class TypeChecker
    {
        private Dictionary<string, Variable> vars;
        private Dictionary<string, Function> funcs;

        public TypeChecker(Dictionary<string, Variable> vars, Dictionary<string, Function> funcs)
        {
            this.vars = vars;
            this.funcs = funcs;
        }

        public Type getExpressionType(Node node)
        {
            if (node is Number)
            {
                return (node as Number).type;
            } else if (node is VarUsage)
            {
                VarUsage varUsage = (VarUsage)node;
                Variable var = null;
                if (vars.TryGetValue(varUsage.id, out var))
                {
                    return getExpressionType(var);
                }
                throw new Exception("Undefined variable: " + varUsage.id + "!");
            } else if (node is Variable)
            {
                return getExpressionType((node as Variable).init);
            } else if (node is UnOperator)
            {
                return getExpressionType((node as UnOperator).operand);
            } else if (node is BinOperator)
            {
                BinOperator bo = (BinOperator)node;
                if (getExpressionType(bo.operand1) == Type.Float || 
                    getExpressionType(bo.operand2) == Type.Float)
                {
                    return Type.Float;
                } else
                {
                    return Type.Int;
                }
            } else if (node is FuncUsage)
            {
                FuncUsage funcUsage = (FuncUsage)node;
                Function func = null;
                if (funcs.TryGetValue(funcUsage.id, out func))
                {
                    if (func.parametrs.parametrs.Count != funcUsage.args.arguments.Count)
                    {
                        throw new Exception("Неправильное количество аргументов в функции " + funcUsage.id + "!");
                    }
                    if (!argsToParamsCasting(func.parametrs.parametrs, funcUsage.args.arguments))
                    {
                        throw new Exception("Типы аргументы не соответствуют типам параметров функции " + funcUsage.id + "!");
                    }
                    List<Node> castedArgs = new List<Node>();
                    for (int i = 0; i < funcUsage.args.arguments.Count; ++i)
                    {
                        dynamic temp = funcUsage.args.arguments[i].interpret(vars, funcs);
                        if (temp is int && func.parametrs.parametrs[i].type == Type.Float)
                        {
                            temp = Convert.ToDouble(temp);
                        }
                        castedArgs.Add(new Number(temp, func.parametrs.parametrs[i].type));
                    }
                    Node body = func.init.funcArgsReplacment(func.parametrs.parametrs, castedArgs);
                    return getExpressionType(body);
                }
                throw new Exception("Undefined function: " + funcUsage.id + "!");
            } else if (node is Function)
            {
                Function func = (Function)node;
                List<Node> args = new List<Node>();
                foreach ((Type t, string id) in func.parametrs.parametrs)
                {
                    args.Add(new Number(0, t));
                }
                Node body = func.init.funcArgsReplacment(func.parametrs.parametrs, args);
                return getExpressionType(body);
            } else
            {
                throw new NotImplementedException("А чё это вы хотите проверить?");
            }
        }

        public bool argsToParamsCasting(List<(Type type, string id)> parametrs, List<Node> args)
        {
            for (int i = 0; i < parametrs.Count; ++i)
            {
                if (parametrs[i].type == Type.Int && getExpressionType(args[i]) == Type.Float)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
