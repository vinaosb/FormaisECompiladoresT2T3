using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormaisECompiladores
{
    public class Token
    {
        
        public enum Attributes
        {
            ID,
            BASIC, // Outras palavras reservadas
            BRKTPARE, // {}[]()
            NUM, // Numeros nao float
            LOOP, // DO WHILE BREAK
            ITE, // if then else
            VALUES, // REAL TRUE FALSE
            ASSERT, // =
            COMPARISON, // <= >= != ...
            ARITMETHIC, // + - * / 
            LOGICAL, // ! || &&
            SEPARATOR, // ;
            ERROR,
            EMPTY // Auxiliar pro sintatico
        };
        public enum Terminals
        {
            ID,
            BASIC, // Outras palavras reservadas
            OPENBRACE, CLOSEBRACE, OPENBRKT, CLOSEBRKT, OPENPARENT, CLOSEPARENT, // {}[]()
            NUM, // Numeros nao float
            DO, WHILE, BREAK, // DO WHILE BREAK
            IF, THEN, ELSE,
            REAL, TRUE, FALSE, // REAL TRUE FALSE
            ASSERT, // =
            LT, LE, EQ, GT, GE, NE, // < <= == > >= <>
            ADD, MINUS, MULTIPLY, DIVIDE, // + - * / 
            NOT, OR, AND, // ! || &&
            SEPARATOR, // ;
            ERROR,
            EMPTY // Auxiliar pro sintatico
        };


        public string path { get; set; }
        public Dictionary<string, Terminals> TokenCorrelation;
        public Dictionary<Terminals, Attributes> AttrCorrelation;

        public struct Tok
        {
            public string s;
            public Terminals t;
            public Attributes a;
        }

        public Token (string Path)
        {
            path = Path;
            TokenCorrelation = new Dictionary<string, Terminals>();
            AttrCorrelation = new Dictionary<Terminals, Attributes>();
            init();
        }

        public void init()
        {
            TokenCorrelation.Add("if", Terminals.IF);
            TokenCorrelation.Add("then", Terminals.THEN);
            TokenCorrelation.Add("else", Terminals.ELSE);
            TokenCorrelation.Add("do", Terminals.DO);
            TokenCorrelation.Add("while", Terminals.WHILE);
            TokenCorrelation.Add("break", Terminals.BREAK);
            TokenCorrelation.Add(";", Terminals.SEPARATOR);
            TokenCorrelation.Add("basic", Terminals.BASIC);
            TokenCorrelation.Add("(", Terminals.OPENPARENT);
            TokenCorrelation.Add(")", Terminals.CLOSEPARENT);
            TokenCorrelation.Add("{", Terminals.OPENBRACE);
            TokenCorrelation.Add("}", Terminals.CLOSEBRACE);
            TokenCorrelation.Add("[", Terminals.OPENBRKT);
            TokenCorrelation.Add("]", Terminals.CLOSEBRKT);
            TokenCorrelation.Add("num", Terminals.NUM);
            TokenCorrelation.Add("true", Terminals.TRUE);
            TokenCorrelation.Add("false", Terminals.FALSE);
            TokenCorrelation.Add("real", Terminals.REAL);
            TokenCorrelation.Add("=", Terminals.ASSERT);
            TokenCorrelation.Add(">", Terminals.GE);
            TokenCorrelation.Add(">=", Terminals.GT);
            TokenCorrelation.Add("<", Terminals.LT);
            TokenCorrelation.Add("<=", Terminals.LE);
            TokenCorrelation.Add("==", Terminals.EQ);
            TokenCorrelation.Add("!=", Terminals.NE);           
            TokenCorrelation.Add("!", Terminals.NOT);
            TokenCorrelation.Add("&&", Terminals.AND);
            TokenCorrelation.Add("||", Terminals.OR);
            TokenCorrelation.Add("+", Terminals.ADD);
            TokenCorrelation.Add("-", Terminals.MINUS);
            TokenCorrelation.Add("*", Terminals.MULTIPLY);
            TokenCorrelation.Add("/", Terminals.DIVIDE);
            AttrCorrelation.Add(Terminals.ID, Attributes.ID);
            AttrCorrelation.Add(Terminals.BASIC, Attributes.BASIC);
            AttrCorrelation.Add(Terminals.OPENBRACE, Attributes.BRKTPARE);
            AttrCorrelation.Add(Terminals.OPENBRKT, Attributes.BRKTPARE);
            AttrCorrelation.Add(Terminals.OPENPARENT, Attributes.BRKTPARE);
            AttrCorrelation.Add(Terminals.CLOSEBRACE, Attributes.BRKTPARE);
            AttrCorrelation.Add(Terminals.CLOSEBRKT, Attributes.BRKTPARE);
            AttrCorrelation.Add(Terminals.CLOSEPARENT, Attributes.BRKTPARE);
            AttrCorrelation.Add(Terminals.NUM, Attributes.NUM);
            AttrCorrelation.Add(Terminals.DO, Attributes.LOOP);
            AttrCorrelation.Add(Terminals.WHILE, Attributes.LOOP);
            AttrCorrelation.Add(Terminals.BREAK, Attributes.LOOP);
            AttrCorrelation.Add(Terminals.IF, Attributes.ITE);
            AttrCorrelation.Add(Terminals.THEN, Attributes.ITE);
            AttrCorrelation.Add(Terminals.ELSE, Attributes.ITE);
            AttrCorrelation.Add(Terminals.REAL, Attributes.VALUES);
            AttrCorrelation.Add(Terminals.TRUE, Attributes.VALUES);
            AttrCorrelation.Add(Terminals.FALSE, Attributes.VALUES);
            AttrCorrelation.Add(Terminals.ASSERT, Attributes.ASSERT);
            AttrCorrelation.Add(Terminals.LT, Attributes.COMPARISON);
            AttrCorrelation.Add(Terminals.LE, Attributes.COMPARISON);
            AttrCorrelation.Add(Terminals.EQ, Attributes.COMPARISON);
            AttrCorrelation.Add(Terminals.GT, Attributes.COMPARISON);
            AttrCorrelation.Add(Terminals.GE, Attributes.COMPARISON);
            AttrCorrelation.Add(Terminals.NE, Attributes.COMPARISON);
            AttrCorrelation.Add(Terminals.ADD, Attributes.ARITMETHIC);
            AttrCorrelation.Add(Terminals.MINUS, Attributes.ARITMETHIC);
            AttrCorrelation.Add(Terminals.MULTIPLY, Attributes.ARITMETHIC);
            AttrCorrelation.Add(Terminals.DIVIDE, Attributes.ARITMETHIC);
            AttrCorrelation.Add(Terminals.NOT, Attributes.LOGICAL);
            AttrCorrelation.Add(Terminals.OR, Attributes.LOGICAL);
            AttrCorrelation.Add(Terminals.AND, Attributes.LOGICAL);
            AttrCorrelation.Add(Terminals.SEPARATOR, Attributes.SEPARATOR);
        }

        public List<Tok> ReadFile()
        {
            List<Tok> LT = new List<Tok>();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    while (sr.Peek() >= 0)
                    {
                        String line = sr.ReadLine();

                        LT.AddRange(Tokenize(line));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return LT;
        }

        public List<Tok> Tokenize (String s)
        {
            List<Tok> tokens = new List<Tok>();
            char[] charSeparator = new char[] { ' ' };
            string[] result;

            result = s.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var r in result)
            {
                Tok temp;
                temp.s = r;
                temp.t = getTerminal(r);
                temp.a = AttrCorrelation.GetValueOrDefault(temp.t);
                tokens.Add(temp);
            }

            return tokens;
        }

        public Terminals getTerminal(string s)
        {
            if (Char.IsNumber(s[0]))
            {
                if (s.Contains("."))
                    return Terminals.REAL;
                return Terminals.NUM;
            }
            if (TokenCorrelation.ContainsKey(s))
                return TokenCorrelation.GetValueOrDefault(s);
            if (Char.IsLetter(s[0]))
                return Terminals.ID;
            Console.WriteLine("{0} é invalido", s);
            return Terminals.ERROR;
        }
    }
}
