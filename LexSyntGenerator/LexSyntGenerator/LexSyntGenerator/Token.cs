using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lexer;

namespace LexSyntGenerator
{
    public class Token : ICloneable
    {

        public TokenType token { get; set; }

        public dynamic content { get; set; }

        public Token(TokenType token, dynamic content)
        {
            this.token = token;
            this.content = content;
        }

        public object Clone()
        {
            return new Token(this.token, this.content);
        }
    }

    public class Variable
    {
        public static Dictionary<string, Variable> definedVars = new Dictionary<string, Variable>();

        public static bool defineVar(String id, Stack<Node> stack)
        {
            if (definedVars.ContainsKey(id))
            {
                definedVars.Remove(id);
            }

            definedVars.Add(id, new Variable(id, new Stack<Node>(stack).Peek()));
            OpNode def = new OpNode(new Token(TokenType.Tok_init, "def " + id), stack, 1);
            stack.Push(def);

            return true;
        }

        public string id { get; set; }

        public Node init { get; set; }

        public Variable(string id, Node expression)
        {
            this.id = id;
            this.init = expression;
        }
    }

    public class Function
    {
        public static Dictionary<string, Function> definedFuncs = new Dictionary<string, Function>();

        public static List<string> paramsId = new List<string>();

        public static bool addParam(string id)
        {
            paramsId.Add(id);

            return true;
        }

        public static bool defineFunc(String id, Stack<Node> stack)
        {
            if (definedFuncs.ContainsKey(id))
            {
                definedFuncs.Remove(id);
            }

            definedFuncs.Add(id, new Function(id, stack.Peek(), paramsId));
            OpNode def = new OpNode(new Token(TokenType.Tok_init, "def " + id + "(" + string.Join(',', paramsId) + ")"), stack, 1);
            stack.Push(def);

            paramsId = new List<string>();

            return true;
        }
        public string id { get; set; }

        public List<string> fParams {get; set;}

        public Node init { get; set; }

        public Function(string id, Node expression, List<string> fParams)
        {
            this.id = id;
            this.init = expression;
            this.fParams = fParams;
            this.fParams.Reverse();
        }
    }

    public enum NodeType { Leaf, Op }

    // Создаем дерево в процессе парсинга, а потом все вычисляем
    // Данный класс хранит основные методы и переменные, к которым обращаются семантические вставки из парсера
    public interface Node
    {
        public Token token { get; set; }
        public NodeType getNodeType();

        // Хранилище для сбора операндов, а потом их запаковки
        public static Stack<Node> stack = new Stack<Node>();

        // Вычисление результата при помощи синтаксического дерева
        public static dynamic getResult(Node node)
        {
            if (node.getNodeType() == NodeType.Leaf)
            {
                if (node.token.token == TokenType.Tok_id)
                {
                    Variable var;
                    if (!Variable.definedVars.TryGetValue(node.token.content as string, out var))
                    {
                        throw new Exception("Variable " + (node.token.content as string) + " is not defined!");
                    }

                    return getResult(var.init);
                }
                else
                {
                    return node.token.content;
                }
            }
            else
            {
                OpNode op = (OpNode)node;
                switch (op.token.token)
                {
                    case TokenType.Tok_plus:
                        return getResult(op.operands.ElementAt(0)) + getResult(op.operands.ElementAt(1));
                    case TokenType.Tok_minus:
                        if (op.operands.Count == 2)
                        {
                            return getResult(op.operands.ElementAt(0)) - getResult(op.operands.ElementAt(1));
                        } else
                        {
                            return -getResult(op.operands[0]);
                        }
                    case TokenType.Tok_multiply:
                        return getResult(op.operands.ElementAt(0)) * getResult(op.operands.ElementAt(1));
                    case TokenType.Tok_division:
                        return getResult(op.operands.ElementAt(0)) / getResult(op.operands.ElementAt(1));
                    case TokenType.Tok_pow:
                        return Math.Pow(getResult(op.operands.ElementAt(0)), getResult(op.operands.ElementAt(1)));
                    case TokenType.Tok_init:
                        return getResult(op.operands[0]);
                    case TokenType.Tok_lparen:
                        return getResult(op.operands[0]);
                    case TokenType.Tok_call:
                        Function f;
                        if(!Function.definedFuncs.TryGetValue(op.token.content, out f))
                        {
                            throw new Exception("Undefined function: " + op.token.content + "!");
                        }

                        Node body = f.init.Clone();
                        body = replaceParams(body, f, op.operands);
                        return getResult(body);
                    default:
                        throw new Exception("Unsupported operation: " + op.token.content + "!" );

                }
            }
        }

        // Функция подставляющая аргументы вместо параметров функции при ее вызове 
        public static Node replaceParams(Node body, Function f, List<Node> operands)
        {
            if (body.getNodeType() == NodeType.Op)
            {
                OpNode n = (OpNode)body;
                for (int i = 0; i < n.operands.Count; ++i)
                {
                    if (n.operands[i].getNodeType() == NodeType.Leaf)
                    {
                        if (n.operands[i].token.token == TokenType.Tok_id)
                        {
                            int j = 0;
                            bool isParam = false;
                            for (; j < f.fParams.Count; ++j)
                            {
                                if (f.fParams[j].Equals(n.operands[i].token.content))
                                {
                                    isParam = true;
                                    break;
                                }
                            }

                            if (isParam)
                            {
                                n.operands[i] = operands[j];
                            }
                        }
                    } else
                    {
                        replaceParams(n.operands[i], f, operands);
                    }
                }
            } if (body.token.token == TokenType.Tok_id)
            {
                int j = 0;
                bool isParam = false;
                for (; j < f.fParams.Count; ++j)
                {
                    if (f.fParams[j].Equals(body.token.content))
                    {
                        isParam = true;
                        break;
                    }
                }

                if (isParam)
                {
                    body = operands[j];
                }
            }
            return body;
        }

        public static void printTree(Node node, int span)
        {
            Console.WriteLine((new string('\t', span)) + node.token.token + " : " + node.token.content);

                if (node.getNodeType() != NodeType.Leaf)
                {
                    OpNode op = (OpNode)node;
                foreach (Node n in op.operands)
                {
                    printTree(n, span + 1);
                }
                }
        }

        // Построение дерева
        public static bool createNode(Token token, Stack<Node> stack, int opAmount)
        {
            if (token.token == TokenType.Tok_id || token.token == TokenType.Tok_number)
            {
                LeafNode leaf = new LeafNode(token);
                stack.Push(leaf);
            }
            else
            {
                OpNode op;
                if (token.token == TokenType.Tok_call)
                {
                    Function func;
                    if (!Function.definedFuncs.TryGetValue(token.content, out func))
                    {
                        throw new Exception("Undefined function: " + token.content + "!" );
                    }
                    op = new OpNode(token, stack, func.fParams.Count);
                } else
                {
                    op = new OpNode(token, stack, opAmount);
                }
                stack.Push(op);
            }
            return true;
        }

        public Node Clone();
    }
    public class LeafNode : Node
    {
        public Token token { get; set; }

        public LeafNode(Token token)
        {
            this.token = token;
        }

        public NodeType getNodeType()
        {
            return NodeType.Leaf;
        }
        public Node Clone()
        {
            return new LeafNode(this.token);
        }
    }

    public class OpNode : Node
    {
        public Token token { get; set; }
        public List<Node> operands = new List<Node>();
        public int opAmount { get; set; }

        public OpNode(Token token, Stack<Node> stack, int opAmount)
        {
            this.token = token;
            Stack<Node> temp = new Stack<Node>();
            for (int i = 0; i < opAmount; ++i)
            {
                temp.Push(stack.Pop());
            }


            operands.AddRange(temp);
        }

        public OpNode(Token token, List<Node> operands)
        {
            this.token = token;
            this.operands = operands;
        }

        public NodeType getNodeType()
        {
            return NodeType.Op;
        }

        public Node Clone()
        {
            return new OpNode(this.token, new List<Node>(this.operands));
        }
    }
}
