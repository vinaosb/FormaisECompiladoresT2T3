using System;
using System.Collections.Generic;
using System.IO;

namespace FormaisECompiladores
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===### Analise Sintatica ###===");
            string name;
            string path = @"C:\Lex\program.txt";
            Console.WriteLine("Escreva o nome do arquivo");
            //name = Console.ReadLine();

            //path += name;

            Token t = new Token(path);
            List<Token.Tok> lt = t.ReadFile();
            //Console.WriteLine("Analise Lexica");
            //foreach (var l in lt)
            //{
            //    Console.WriteLine("<{0},{1}>", l.a, l.s);
            //}
            Sintatico s = new Sintatico();
            // ##### PRINT PARSING TABLE ####
            Console.WriteLine("Parsing Table:");
            Console.WriteLine("...");
            string prod = "";
            foreach (var sy in s.ReferenceTable)
            {
                prod = "";
                foreach (var pr in sy.Value)
                {
                    if (pr.nonterminal.Equals(Sintatico.NonTerminal.EMPTY))
                        prod += pr.terminal.ToString();
                    else
                        prod += pr.nonterminal.ToString();
                }
                Console.WriteLine("{0},{1}->{2}", sy.Key.nonterminal, sy.Key.terminal, prod);
            }
            // ##### END PRINT PARSING TABLE ####

            if (s.predictiveParser(lt))
                Console.WriteLine("Entrada Aceita");
            else
                Console.WriteLine("Entrada Nao Aceita");

            Console.Read();

        }
    }
}
