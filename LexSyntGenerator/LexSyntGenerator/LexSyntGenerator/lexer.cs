using System;

namespace lexer {
public enum TokenType : uint {
  eof, Tok_lparen,Tok_rparen,Tok_multiply,Tok_plus,Tok_comma,Tok_minus,Tok_division,Tok_init,Tok_pow,Tok_id,Tok_number,Tok_def,Tok_call
}
public class Lexer {
  private readonly string input;
  private readonly bool debug;
  int curChIx;
  public Lexer(string input, bool debug = false) {
    this.input = input;
    this.debug = debug;
    curChIx = 0;
  }
  public (TokenType type, dynamic attr) getNextToken() {
    start:
    var lastAccChIx = curChIx;
    var startChIx = curChIx;
    char curCh;
    int accSt = -1;
    state_0:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == ' ') goto state_1; else if(curCh == '(') goto state_2; else if(curCh == ')') goto state_3; else if(curCh == '*') goto state_4; else if(curCh == '+') goto state_5; else if(curCh == ',') goto state_6; else if(curCh == '-') goto state_7; else if(curCh == '/') goto state_8; else if(curCh == ':') goto state_9; else if(curCh == '=') goto state_10; else if(curCh == '^') goto state_11; else if(curCh == 'd') goto state_13; else if(curCh == '_'||(curCh >= 'a' && curCh <= 'z')) goto state_12; else if((curCh >= '0' && curCh <= '9')) goto state_14;
      goto end;
    state_1:
      lastAccChIx = curChIx;
      accSt = 1;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == ' ') goto state_1;
      goto end;
    state_2:
      lastAccChIx = curChIx;
      accSt = 2;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_3:
      lastAccChIx = curChIx;
      accSt = 3;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_4:
      lastAccChIx = curChIx;
      accSt = 4;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_5:
      lastAccChIx = curChIx;
      accSt = 5;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_6:
      lastAccChIx = curChIx;
      accSt = 6;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_7:
      lastAccChIx = curChIx;
      accSt = 7;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_8:
      lastAccChIx = curChIx;
      accSt = 8;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_9:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'c') goto state_22;
      goto end;
    state_10:
      lastAccChIx = curChIx;
      accSt = 10;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_11:
      lastAccChIx = curChIx;
      accSt = 11;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_12:
      lastAccChIx = curChIx;
      accSt = 12;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_13:
      lastAccChIx = curChIx;
      accSt = 13;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'e') goto state_19; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_14:
      lastAccChIx = curChIx;
      accSt = 14;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == '.') goto state_15; else if(curCh == 'E'||curCh == 'e') goto state_16; else if((curCh >= '0' && curCh <= '9')) goto state_14;
      goto end;
    state_15:
      lastAccChIx = curChIx;
      accSt = 15;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'E'||curCh == 'e') goto state_16; else if((curCh >= '0' && curCh <= '9')) goto state_15;
      goto end;
    state_16:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == '+'||curCh == '-') goto state_17; else if((curCh >= '0' && curCh <= '9')) goto state_18;
      goto end;
    state_17:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if((curCh >= '0' && curCh <= '9')) goto state_18;
      goto end;
    state_18:
      lastAccChIx = curChIx;
      accSt = 18;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if((curCh >= '0' && curCh <= '9')) goto state_18;
      goto end;
    state_19:
      lastAccChIx = curChIx;
      accSt = 19;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'f') goto state_20; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_20:
      lastAccChIx = curChIx;
      accSt = 20;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == ':') goto state_21; else if(curCh == '_'||(curCh >= '0' && curCh <= '9')||(curCh >= 'a' && curCh <= 'z')) goto state_12;
      goto end;
    state_21:
      lastAccChIx = curChIx;
      accSt = 21;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    state_22:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'a') goto state_23;
      goto end;
    state_23:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'l') goto state_24;
      goto end;
    state_24:
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      if(curCh == 'l') goto state_25;
      goto end;
    state_25:
      lastAccChIx = curChIx;
      accSt = 25;
      
      if(curChIx >= input.Length) goto end;
      curCh = input[curChIx];
      ++curChIx;
      
      goto end;
    end:
    var lastReadChIx = curChIx;
    curChIx = lastAccChIx;
    var text = input.Substring(startChIx, curChIx - startChIx);
    switch(accSt){
      case 1:
        if (debug) Console.Error.WriteLine($"Skipping state 1: \"{text}\"");
        goto start;
      case 2:
        if (debug) Console.Error.WriteLine($"Lexed token lparen: \"{text}\"");
        return (TokenType.Tok_lparen, text);
      case 3:
        if (debug) Console.Error.WriteLine($"Lexed token rparen: \"{text}\"");
        return (TokenType.Tok_rparen, text);
      case 4:
        if (debug) Console.Error.WriteLine($"Lexed token multiply: \"{text}\"");
        return (TokenType.Tok_multiply, text);
      case 5:
        if (debug) Console.Error.WriteLine($"Lexed token plus: \"{text}\"");
        return (TokenType.Tok_plus, text);
      case 6:
        if (debug) Console.Error.WriteLine($"Lexed token comma: \"{text}\"");
        return (TokenType.Tok_comma, text);
      case 7:
        if (debug) Console.Error.WriteLine($"Lexed token minus: \"{text}\"");
        return (TokenType.Tok_minus, text);
      case 8:
        if (debug) Console.Error.WriteLine($"Lexed token division: \"{text}\"");
        return (TokenType.Tok_division, text);
      case 10:
        if (debug) Console.Error.WriteLine($"Lexed token init: \"{text}\"");
        return (TokenType.Tok_init, text);
      case 11:
        if (debug) Console.Error.WriteLine($"Lexed token pow: \"{text}\"");
        return (TokenType.Tok_pow, text);
      case 12:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        return (TokenType.Tok_id, text);
      case 13:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        return (TokenType.Tok_id, text);
      case 14:
        if (debug) Console.Error.WriteLine($"Lexed token number: \"{text}\"");
        return (TokenType.Tok_number,  Double.Parse(text) );
      case 15:
        if (debug) Console.Error.WriteLine($"Lexed token number: \"{text}\"");
        return (TokenType.Tok_number,  Double.Parse(text) );
      case 18:
        if (debug) Console.Error.WriteLine($"Lexed token number: \"{text}\"");
        return (TokenType.Tok_number,  Double.Parse(text) );
      case 19:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        return (TokenType.Tok_id, text);
      case 20:
        if (debug) Console.Error.WriteLine($"Lexed token id: \"{text}\"");
        return (TokenType.Tok_id, text);
      case 21:
        if (debug) Console.Error.WriteLine($"Lexed token def: \"{text}\"");
        return (TokenType.Tok_def, text);
      case 25:
        if (debug) Console.Error.WriteLine($"Lexed token call: \"{text}\"");
        return (TokenType.Tok_call, text /* мне лень вручную это добавлять в перечисление */);
    }
    if (curChIx >= input.Length) {
      if (debug) Console.Error.WriteLine($"Got EOF while lexing \"{text}\"");
      return (TokenType.eof, null);
    }
    throw new ApplicationException("Unexpected input: " + input.Substring(startChIx, lastReadChIx-startChIx));
  }
}
}