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
    {42,3,42,42,42,42,38,42,42,42,27,37,31,42,42},
    {43,42,42,42,42,42,42,42,42,42,42,42,42,42,42},
    {1,42,42,42,42,42,42,42,42,42,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {44,42,44,44,44,44,44,44,42,44,42,42,42,42,42},
    {42,42,4,42,23,42,17,42,42,42,42,42,42,42,42},
    {42,3,45,42,42,42,38,42,42,42,27,37,42,42,42},
    {42,42,46,42,42,42,42,42,42,42,42,42,42,42,42},
    {42,42,47,42,23,6,17,42,42,42,42,42,42,42,42},
    {42,42,48,42,42,42,42,42,42,42,10,42,42,42,42},
    {42,42,49,42,42,9,42,42,42,42,42,42,42,42,42},
    {42,42,50,42,42,42,42,42,42,42,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {51,42,51,51,51,51,51,51,42,42,42,42,42,42,42},
    {52,42,52,21,52,52,52,12,42,42,42,42,42,42,42},
    {53,42,53,21,53,53,53,12,42,42,42,42,42,42,42},
    {54,42,54,21,54,54,54,12,42,42,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {55,42,42,42,23,42,17,42,42,42,42,42,42,42,42},
    {56,42,42,42,23,42,17,42,42,42,42,42,42,42,42},
    {57,42,42,42,23,42,17,42,42,42,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {58,42,58,58,58,58,58,58,42,42,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {59,42,59,59,59,59,59,59,42,42,42,42,42,42,42},
    {60,42,60,60,60,60,60,60,42,24,42,42,42,42,42},
    {61,28,61,61,61,61,61,61,42,61,42,42,42,42,42},
    {42,3,45,42,42,42,38,42,42,42,27,37,42,42,42},
    {62,42,62,62,62,62,62,62,42,62,42,42,42,42,42},
    {42,42,29,42,42,42,42,42,42,42,42,42,42,42,42},
    {42,42,42,42,42,42,42,42,42,42,32,42,42,42,42},
    {42,34,42,42,42,42,42,42,40,42,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {42,42,48,42,42,42,42,42,42,42,10,42,42,42,42},
    {42,42,42,42,42,42,42,42,33,42,42,42,42,42,42},
    {42,42,35,42,42,42,42,42,42,42,42,42,42,42,42},
    {63,42,63,63,63,63,63,63,42,63,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {64,42,64,64,64,64,64,64,42,64,42,42,42,42,42},
    {42,3,42,42,42,42,38,42,42,42,27,37,42,42,42},
    {65,42,65,65,65,65,65,65,42,42,42,42,42,42,42}
  };
  private static uint[,] GOTO = new uint[,] {
    {2,20,41,16,26,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,5,41,16,26,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,8,41,16,26,7,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,11},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,13,0,26,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,41,14,26,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,22,0,26,0,0},
    {0,0,0,0,0,0,0},
    {0,0,41,15,26,0,0},
    {0,0,25,0,26,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,8,41,16,26,30,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,18,41,16,26,0,0},
    {0,0,0,0,0,0,36},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0},
    {0,0,0,0,39,0,0},
    {0,0,0,0,0,0,0},
    {0,19,41,16,26,0,0},
    {0,0,0,0,0,0,0}
  };
  private uint top() {
    return stack.Count == 0 ? 0 : stack.Peek().state;
  }
  static string[] stateNames = new string[] {".","%eof","D","lparen","rparen","E","comma","A","E","comma","id","P","division","F","T","T","T","minus","E","E","E","multiply","F","plus","pow","F","V","id","lparen","rparen","A","def","id","init","lparen","rparen","P","number","minus","V","init","F"};
  static string[] expectedSyms = new string[] {"D","%eof","%eof","E","%eof/comma/division/minus/multiply/plus/pow/rparen","rparen/minus/plus","A","rparen","comma/rparen/minus/plus","P","comma/rparen","rparen","F","%eof/comma/division/minus/multiply/plus/rparen","division/%eof/comma/minus/plus/rparen/multiply","division/multiply/%eof/comma/minus/plus/rparen","division/multiply/%eof/comma/minus/plus/rparen","T","minus/plus/%eof","minus/plus/%eof","minus/plus/%eof","F","%eof/comma/division/minus/multiply/plus/rparen","T","F","%eof/comma/division/minus/multiply/plus/rparen","pow/%eof/comma/division/minus/multiply/plus/rparen","lparen/%eof/comma/division/minus/multiply/plus/pow/rparen","A","%eof/comma/division/minus/multiply/plus/pow/rparen","rparen","id/id","lparen/init","E","P","init","rparen","%eof/comma/division/minus/multiply/plus/pow/rparen","V","%eof/comma/division/minus/multiply/plus/pow/rparen","E","%eof/comma/division/minus/multiply/plus/rparen"};

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

      case 45: {
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

      case 47: {
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

      case 46: {
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

      case 56: {
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

      case 55: {
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

      case 57: {
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

      case 52: {
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

      case 53: {
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

      case 54: {
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

      case 60: {
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

      case 59: {
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

      case 48: {
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

      case 49: {
          if(debug) Console.Error.WriteLine("Reduce using P -> id");
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 6 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(new Parametrs(_1))));
          break;

        }

      case 50: {
          if(debug) Console.Error.WriteLine("Reduce using P -> id comma P");
          dynamic _3=stack.Pop().value;
          var _2=stack.Pop().value.Item2;
          var _1=stack.Pop().value.Item2;
          var gt = GOTO[top(), 6 /*P*/];
          if(gt==0) throw new ApplicationException("No goto");
          if(debug) {
            Console.Error.WriteLine($"{top()} is now on top of the stack;");
            Console.Error.WriteLine($"{gt} will be placed on the stack");
          }
          stack.Push((gt,(_3.addParametr(_1))));
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

      case 51: {
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

      case 58: {
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

      case 61: {
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

      case 62: {
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

      case 44: {
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

      case 64: {
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

      case 63: {
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
