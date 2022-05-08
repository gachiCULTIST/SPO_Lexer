using System;
using System.Collections.Generic;
using lexer;
using parser;

namespace LexSyntGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();

            Lexer lexer;
            Parser parser;

            Dictionary<string, Variable> vars = new Dictionary<string, Variable>();
            Dictionary<string, Function> funcs = new Dictionary<string, Function>();

            while(!str.Equals(":e"))
            {
                if (!str.Equals(""))
                {
                    try
                    {
                        lexer = new Lexer(str);
                        parser = new Parser(lexer);
                        dynamic result = parser.parse();
                        Console.WriteLine(result.tree(0));
                        TypeChecker checker = new TypeChecker(vars, funcs);
                        Console.WriteLine("Type: " + checker.getExpressionType(result));
                        if (result is Function)
                        {
                            if (funcs.ContainsKey(result.id))
                            {
                                funcs.Remove(result.id);
                            }
                            funcs.Add(result.id, result);
                        } else if (result is Variable)
                        {
                            if (vars.ContainsKey(result.id))
                            {
                                vars.Remove(result.id);
                            }
                            vars.Add(result.id, result);
                        } else
                        {
                            Console.WriteLine("Result: " + result.interpret(vars, funcs));
                        }
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                str = Console.ReadLine();
            }

        }
    }
}
