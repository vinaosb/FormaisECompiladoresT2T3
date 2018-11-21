﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormaisECompiladores
{
    public class Token
    {
        public enum NonTerminal
        {
            PROGRAM,
            BLOCK,
            DECLS,
            TYPE,
            TYPES,
            STMTS,
            STMT,
            MATCHED_IF,
            OPEN_IF,
            LOC,
            LOCS,
            BOOL,
            JOIN,
            EQUALITY,
            REL,
            EXPR,
            EXPRS,
            TERM,
            TERMS,
            UNARY,
            FACTOR
        };

        public enum Terminals
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
            ERROR
        };
        

        public string path { get; set; }
        public Dictionary<string, Terminals> TokenCorrelation;

        public struct Tok
        {
            public string s;
            public Terminals t;
        }

        public Token (string Path)
        {
            path = Path;
            TokenCorrelation = new Dictionary<string, Terminals>();
            init();
        }

        public void init ()
        {
            TokenCorrelation.Add("if", Terminals.ITE);
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
                tokens.Add(temp);
            }

            return tokens;
        }

        public Terminals getTerminal(string s)
        {
            if (Char.IsNumber(s[0]))
            {
                if (s.Contains("."))
                    return Terminals.VALUES;
                return Terminals.NUM;
            }
            if (s.Equals("true") || s.Equals("false") || s.Equals("TRUE") || s.Equals("FALSE"))
                return Terminals.VALUES;
            if (TokenCorrelation.ContainsKey(s))
                return TokenCorrelation.GetValueOrDefault(s);
            if (Char.IsLetter(s[0]))
                return Terminals.ID;
            Console.WriteLine("{0} é invalido", s);
            return Terminals.ERROR;
        }
    }
}
