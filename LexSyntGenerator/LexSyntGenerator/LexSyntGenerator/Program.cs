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


            while(!str.Equals(":e"))
            {
                if (!str.Equals(""))
                {
                    try
                    {
                        lexer = new Lexer(str);
                        parser = new Parser(lexer);
                        parser.parse();
                        if (Node.stack.Peek().token.token != TokenType.Tok_init)
                        {
                            Console.WriteLine(Node.getResult(Node.stack.Peek()));
                        }
                        Node.printTree(Node.stack.Peek(), 0);
                        Node.stack.Clear();
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
