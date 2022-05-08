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
    {43,3,43,43,43,43,39,43,43,43,28,38,32,43,43},
    {44,43,43,43,43,43,43,43,43,43,43,43,43,43,43},
    {1,43,43,43,43,43,43,43,43,43,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {45,43,45,45,45,45,45,45,43,45,43,43,43,43,43},
    {43,43,4,43,24,43,18,43,43,43,43,43,43,43,43},
    {43,3,46,43,43,43,39,43,43,43,28,38,43,43,43},
    {43,43,47,43,43,43,43,43,43,43,43,43,43,43,43},
    {43,43,48,43,24,6,18,43,43,43,43,43,43,43,43},
    {43,43,49,43,43,43,43,43,43,43,10,43,43,43,43},
    {43,43,43,43,43,43,43,43,43,43,11,43,43,43,43},
    {43,43,50,43,43,9,43,43,43,43,43,43,43,43,43},
    {43,43,51,43,43,43,43,43,43,43,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {52,43,52,52,52,52,52,52,43,43,43,43,43,43,43},
    {53,43,53,22,53,53,53,13,43,43,43,43,43,43,43},
    {54,43,54,22,54,54,54,13,43,43,43,43,43,43,43},
    {55,43,55,22,55,55,55,13,43,43,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {56,43,43,43,24,43,18,43,43,43,43,43,43,43,43},
    {57,43,43,43,24,43,18,43,43,43,43,43,43,43,43},
    {58,43,43,43,24,43,18,43,43,43,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {59,43,59,59,59,59,59,59,43,43,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {60,43,60,60,60,60,60,60,43,43,43,43,43,43,43},
    {61,43,61,61,61,61,61,61,43,25,43,43,43,43,43},
    {62,29,62,62,62,62,62,62,43,62,43,43,43,43,43},
    {43,3,46,43,43,43,39,43,43,43,28,38,43,43,43},
    {63,43,63,63,63,63,63,63,43,63,43,43,43,43,43},
    {43,43,30,43,43,43,43,43,43,43,43,43,43,43,43},
    {43,43,43,43,43,43,43,43,43,43,33,43,43,43,43},
    {43,35,43,43,43,43,43,43,41,43,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {43,43,49,43,43,43,43,43,43,43,10,43,43,43,43},
    {43,43,43,43,43,43,43,43,34,43,43,43,43,43,43},
    {43,43,36,43,43,43,43,43,43,43,43,43,43,43,43},
    {64,43,64,64,64,64,64,64,43,64,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {65,43,65,65,65,65,65,65,43,65,43,43,43,43,43},
    {43,3,43,43,43,43,39,43,43,43,28,38,43,43,43},
    {66,43,66,66,66,66,66,66,43,43,43,43,43,43,43}
  };
  private static uint[,] GOTO = new uint[,] {
    {2,21,42,17,27,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,5,42,17,27,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,8,42,17,27,7,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,12},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,14,0,27,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,42,15,27,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,23,0,27,0,0},
    {0,0,0,0,0,0,0},
    {0,0,42,16,27,0,0},
    {0,0,26,0,27,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,8,42,17,27,31,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,19,42,17,27,0,0},
    {0,0,0,0,0,0,37},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,40,0,0},
    {0,0,0,0,0,0,0},
    {0,20,42,17,27,0,0},
    {0,0,0,0,0,0,0}
  };
  private uint top() {
    return stack.Count == 0 ? 0 : stack.Peek().state;
  }
  static string[] stateNames = new string[] {".","%eof","D","lparen","rparen","E","comma","A","E","comma","id","id","P","division","F","T","T","T","minus","E","E","E","multiply","F","plus","pow","F","V","id","lparen","rparen","A","def","id","init","lparen","rparen","P","number","minus","V","init","F"};
  static string[] expectedSyms = new string[] {"D","%eof","%eof","E","%eof/comma/division/minus/multiply/plus/pow/rparen","rparen/minus/plus","A","rparen","comma/rparen/minus/plus","P","id/id","comma/rparen","rparen","F","%eof/comma/division/minus/multiply/plus/rparen","division/%eof/comma/minus/plus/rparen/multiply","division/multiply/%eof/comma/minus/plus/rparen","division/multiply/%eof/comma/minus/plus/rparen","T","minus/plus/%eof","minus/plus/%eof","minus/plus/%eof","F","%eof/comma/division/minus/multiply/plus/rparen","T","F","%eof/comma/division/minus/multiply/plus/rparen","pow/%eof/comma/division/minus/multiply/plus/rparen","lparen/%eof/comma/division/minus/multiply/plus/pow/rparen","A","%eof/comma/division/minus/multiply/plus/pow/rparen","rparen","id/id","lparen/init","E","P","init","rparen","%eof/comma/division/minus/multiply/plus/pow/rparen","V","%eof/comma/division/minus/multiply/plus/pow/rparen","E","%eof/comma/division/minus/multiply/plus/rparen"};

  public Parser(Lexer lex, bool debug = false) {
    this.lex = lex;
    this.debug = debug;
  }
  public dynamic parse() {
    var a = lex.getNextToken();
    while (true) {
      var action = Action[top(), (int)a.type];
      switch (action) {
      case 44: {
          stack.Pop();
          return stack.Pop().value;

        }

      case 46: {
          if(debug) Console.Error.WriteLine("Reduce using A -> ");
          
          var gt = GOTO[top(), 5 /*A*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new Arguments())));
          break;

        }

      case 48: {
          if(debug) Console.Error.WriteLine("Reduce using A -> E");
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 5 /*A*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new Arguments(_1))));
          break;

        }

      case 47: {
          if(debug) Console.Error.WriteLine("Reduce using A -> E comma A");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          dynamic _1=stack.Pop().value;
          var gt = GOTO[top(), 5 /*A*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_3.addArgument(_1))));
          break;

        }

      case 57: {
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
          stack.Push((gt,(new Variable(_2, _4))));
          break;

        }

      case 56: {
          if(debug) Console.Error.WriteLine("Reduce using D -> def id lparen P rparen init E");
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
          stack.Push((gt,(new Function(_2, _4, _7))));
          break;

        }

      case 58: {
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

      case 53: {
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
          stack.Push((gt,(new BinOperator(TokenType.Tok_minus, _1, _3))));
          break;

        }

      case 54: {
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
          stack.Push((gt,(new BinOperator(TokenType.Tok_plus, _1, _3))));
          break;

        }

      case 55: {
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

      case 61: {
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

      case 60: {
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
          stack.Push((gt,(new BinOperator(TokenType.Tok_pow, _1, _3))));
          break;

        }

      case 49: {
          if(debug) Console.Error.WriteLine("Reduce using P -> ");
          
          var gt = GOTO[top(), 6 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new Parametrs())));
          break;

        }

      case 50: {
          if(debug) Console.Error.WriteLine("Reduce using P -> id id");
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 6 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new Parametrs(_1, _2))));
          break;

        }

      case 51: {
          if(debug) Console.Error.WriteLine("Reduce using P -> id id comma P");
          dynamic _4=stack.Pop().value;
          var _3=stack.Pop().value.Item2;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 6 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_4.addParametr(_1, _2))));
          break;

        }

      case 66: {
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

      case 52: {
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
          stack.Push((gt,(new BinOperator(TokenType.Tok_division, _1, _3))));
          break;

        }

      case 59: {
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
          stack.Push((gt,(new BinOperator(TokenType.Tok_multiply, _1, _3))));
          break;

        }

      case 62: {
          if(debug) Console.Error.WriteLine("Reduce using V -> id");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new VarUsage(_1))));
          break;

        }

      case 63: {
          if(debug) Console.Error.WriteLine("Reduce using V -> id lparen A rparen");
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
          stack.Push((gt,(new FuncUsage(_1, _3))));
          break;

        }

      case 45: {
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
          stack.Push((gt,(_2)));
          break;

        }

      case 65: {
          if(debug) Console.Error.WriteLine("Reduce using V -> minus V");
          dynamic _2=stack.Pop().value;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new UnOperator(TokenType.Tok_uminus, _2))));
          break;

        }

      case 64: {
          if(debug) Console.Error.WriteLine("Reduce using V -> number");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 4 /*V*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new Number(_1))));
          break;

        }

      case 43: {
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
