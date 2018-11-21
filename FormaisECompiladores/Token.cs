using System;
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
            BASIC,
            LFTBRACKET,
            RGTBRACKET,
            LFTPARENTESIS,
            RGTPARENTESIS,
            LFTSQRBRACKET,
            RGTSQRBRACKET,
            NUM,
            WHILE,
            DO,
            BREAK,
            IF,
            THEN,
            ELSE,
            REAL,
            TRUE,
            FALSE,
            EQUAL,
            NOTEQUAL,
            ASSERT,
            LT,
            LTE,
            GT,
            GTE,
            ADD,
            MINUS,
            MULTIPLY,
            DIVIDE,
            NOT
        };
        

        public string path { get; set; }
        public Dictionary<string, Terminals> TokenCorrelation;

        public Token (string Path)
        {
            path = Path;
            TokenCorrelation = new Dictionary<string, Terminals>();
        }

        public List<Terminals> ReadFile()
        {
            List<Terminals> LT = new List<Terminals>();

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

        public List<Terminals> Tokenize (String s)
        {
            List<Terminals> tokens = new List<Terminals>();
            char[] charSeparator = new char[] { ' ', ';' };
            string[] result;

            result = s.Split(charSeparator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var r in result)
            {
                tokens.Add(TokenCorrelation.GetValueOrDefault(r));
            }

            return tokens;
        }
    }
}
