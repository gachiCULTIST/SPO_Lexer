using lexer;
using System;
using System.Collections.Generic;

	using LexSyntGenerator;

namespace parser {
public class Parser {
  private readonly Lexer lex;
  private readonly bool debug;
  private Stack<(uint state, dynamic value)> stack = new Stack<(uint state, dynamic value)>();
  private static uint[,] Action = new uint[,] {
    {42,25,42,42,42,42,28,42,42,42,16,35,9,42},
    {43,42,42,42,42,42,42,42,42,42,42,42,42,42},
    {1,42,42,42,42,42,42,42,42,42,42,42,42,42},
    {42,25,44,42,42,42,28,42,42,42,16,35,42,42},
    {42,42,45,42,36,3,30,42,42,42,42,42,42,42},
    {42,42,46,42,42,42,42,42,42,42,42,42,42,42},
    {42,42,47,42,42,42,42,42,42,42,7,42,42,42},
    {42,42,48,42,42,6,42,42,42,42,42,42,42,42},
    {42,42,49,42,42,42,42,42,42,42,42,42,42,42},
    {42,42,42,42,42,42,42,42,42,42,10,42,42,42},
    {42,12,42,42,42,42,42,42,40,42,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {42,42,47,42,42,42,42,42,42,42,7,42,42,42},
    {42,42,42,42,42,42,42,42,11,42,42,42,42,42},
    {42,42,13,42,42,42,42,42,42,42,42,42,42,42},
    {50,42,42,42,36,42,30,42,42,42,42,42,42,42},
    {51,17,51,51,51,51,51,51,42,51,42,42,42,42},
    {42,25,44,42,42,42,28,42,42,42,16,35,42,42},
    {52,42,52,52,52,52,52,52,42,52,42,42,42,42},
    {42,42,18,42,42,42,42,42,42,42,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {53,42,53,53,53,53,53,53,42,42,42,42,42,42},
    {54,42,54,33,54,54,54,20,42,42,42,42,42,42},
    {55,42,55,33,55,55,55,20,42,42,42,42,42,42},
    {56,42,56,33,56,56,56,20,42,42,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {57,42,57,57,57,57,57,57,42,57,42,42,42,42},
    {42,42,26,42,36,42,30,42,42,42,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {58,42,58,58,58,58,58,58,42,58,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {59,42,42,42,36,42,30,42,42,42,42,42,42,42},
    {60,42,42,42,36,42,30,42,42,42,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {61,42,61,61,61,61,61,61,42,42,42,42,42,42},
    {62,42,62,62,62,62,62,62,42,62,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {63,42,63,63,63,63,63,63,42,42,42,42,42,42},
    {64,42,64,64,64,64,64,64,42,37,42,42,42,42},
    {42,25,42,42,42,42,28,42,42,42,16,35,42,42},
    {65,42,65,65,65,65,65,65,42,42,42,42,42,42}
  };
  private static uint[,] GOTO = new uint[,] {
    {2,32,41,24,39,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,4,41,24,39,5,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,8},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,15,41,24,39,0,0},
    {0,0,0,0,0,0,14},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,4,41,24,39,19,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,21,0,39,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,27,41,24,39,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,29,0,0},
    {0,0,0,0,0,0,0},
    {0,0,41,22,39,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,34,0,39,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,41,23,39,0,0},
    {0,0,38,0,39,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,31,41,24,39,0,0},
    {0,0,0,0,0,0,0}
  };
  private uint top() {
    return stack.Count == 0 ? 0 : stack.Peek().state;
  }
  static string[] stateNames = new string[] {".","%eof","D","comma","E","P","comma","id","A","def","id","init","lparen","rparen","A","E","id","lparen","rparen","P","division","F","T","T","T","lparen","rparen","E","minus","V","minus","E","E","multiply","F","number","plus","pow","F","V","init","F"};
  static string[] expectedSyms = new string[] {"D","%eof","%eof","P","rparen/comma/minus/plus","rparen","A","rparen/comma","rparen","id/id","lparen/init","E","A","init","rparen","%eof/minus/plus","lparen/%eof/comma/division/minus/multiply/plus/pow/rparen","P","%eof/comma/division/minus/multiply/plus/pow/rparen","rparen","F","%eof/comma/division/minus/multiply/plus/rparen","division/%eof/comma/minus/plus/rparen/multiply","division/multiply/%eof/comma/minus/plus/rparen","division/multiply/%eof/comma/minus/plus/rparen","E","%eof/comma/division/minus/multiply/plus/pow/rparen","rparen/minus/plus","V","%eof/comma/division/minus/multiply/plus/pow/rparen","T","minus/plus/%eof","minus/plus/%eof","F","%eof/comma/division/minus/multiply/plus/rparen","%eof/comma/division/minus/multiply/plus/pow/rparen","T","F","%eof/comma/division/minus/multiply/plus/rparen","pow/%eof/comma/division/minus/multiply/plus/rparen","E","%eof/comma/division/minus/multiply/plus/rparen"};

  public Parser(Lexer lex, bool debug = false) {
    this.lex = lex;
    this.debug = debug;
  }
  public dynamic parse() {
    var a = lex.getNextToken();
    while (true) {
      var action = Action[top(), (int)a.type];
      switch (action) {
      case 43: {
          stack.Pop();
          return stack.Pop().value;
        }
      case 47: {
          if(debug) Console.Error.WriteLine("Reduce using A -> ");
          
          var gt = GOTO[top(), 6 /*A*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(null)));
          break;
        }
      case 48: {
          if(debug) Console.Error.WriteLine("Reduce using A -> id");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 6 /*A*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Function.addParam(_1 as string))));
          break;
        }
      case 49: {
          if(debug) Console.Error.WriteLine("Reduce using A -> id comma A");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 6 /*A*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Function.addParam(_1 as string))));
          break;
        }
      case 59: {
          if(debug) Console.Error.WriteLine("Reduce using D -> def id init E");
          dynamic _4=stack.Pop().value;
          var _3=stack.Pop().value.Item2;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*D*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Variable.defineVar(_2, Node.stack))));
          break;
        }
      case 50: {
          if(debug) Console.Error.WriteLine("Reduce using D -> def id lparen A rparen init E");
          dynamic _7=stack.Pop().value;
          var _6=stack.Pop().value.Item2;
          var _5=stack.Pop().value.Item2;
          dynamic _4=stack.Pop().value;
          var _3=stack.Pop().value.Item2;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 0 /*D*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Function.defineFunc(_2, Node.stack))));
          break;
        }
      case 60: {
          if(debug) Console.Error.WriteLine("Reduce using D -> E");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 0 /*D*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_1)));
          break;
        }
      case 54: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E minus T");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 1 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_minus, _2), Node.stack, 2))));
          break;
        }
      case 55: {
          if(debug) Console.Error.WriteLine("Reduce using E -> E plus T");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 1 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_plus, _2), Node.stack, 2))));
          break;
        }
      case 56: {
          if(debug) Console.Error.WriteLine("Reduce using E -> T");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 1 /*E*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_1)));
          break;
        }
      case 64: {
          if(debug) Console.Error.WriteLine("Reduce using F -> V");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 2 /*F*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_1)));
          break;
        }
      case 63: {
          if(debug) Console.Error.WriteLine("Reduce using F -> V pow F");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 2 /*F*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_pow, _2), Node.stack, 2))));
          break;
        }
      case 44: {
          if(debug) Console.Error.WriteLine("Reduce using P -> ");
          
          var gt = GOTO[top(), 5 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(null)));
          break;
        }
      case 45: {
          if(debug) Console.Error.WriteLine("Reduce using P -> E");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 5 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(null)));
          break;
        }
      case 46: {
          if(debug) Console.Error.WriteLine("Reduce using P -> E comma P");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 5 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(null)));
          break;
        }
      case 65: {
          if(debug) Console.Error.WriteLine("Reduce using T -> F");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 3 /*T*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_1)));
          break;
        }
      case 53: {
          if(debug) Console.Error.WriteLine("Reduce using T -> T division F");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 3 /*T*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_division, _2), Node.stack, 2))));
          break;
        }
      case 61: {
          if(debug) Console.Error.WriteLine("Reduce using T -> T multiply F");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 3 /*T*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_multiply, _2), Node.stack, 2))));
          break;
        }
      case 51: {
          if(debug) Console.Error.WriteLine("Reduce using V -> id");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_id, _1), Node.stack, 1))));
          break;
        }
      case 52: {
          if(debug) Console.Error.WriteLine("Reduce using V -> id lparen P rparen");
          var _4=stack.Pop().value.Item2;
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_call, _1), Node.stack, 0))));
          break;
        }
      case 57: {
          if(debug) Console.Error.WriteLine("Reduce using V -> lparen E rparen");
          var _3=stack.Pop().value.Item2;
          dynamic _2=stack.Pop().value;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_lparen, "()"), Node.stack, 1))));
          break;
        }
      case 58: {
          if(debug) Console.Error.WriteLine("Reduce using V -> minus V");
          dynamic _2=stack.Pop().value;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_minus, _1), Node.stack, 1))));
          break;
        }
      case 62: {
          if(debug) Console.Error.WriteLine("Reduce using V -> number");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(Node.createNode(new Token(TokenType.Tok_number, _1), Node.stack, 1))));
          break;
        }
      case 42: {
          string parsed=stateNames[top()];
          var lastSt = top();
          while(stack.Count > 0) { stack.Pop(); parsed = stateNames[top()] + " " + parsed; }
          throw new ApplicationException(
            $"Rejection state reached after parsing \"{parsed}\", when encoutered symbol \""
            + $"\"{a.type}\" in state {lastSt}. Expected \"{expectedSyms[lastSt]}\"");
        }
      default:
        if(debug) Console.Error.WriteLine($"Shift to {action}");
        stack.Push((action, a));
        a=lex.getNextToken();
        break;
      }
    }
  }
}
}