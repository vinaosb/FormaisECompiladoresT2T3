using System;
using System.Collections.Generic;
using System.IO;

namespace FormaisECompiladores
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string name;
            string path = @"";
            Console.WriteLine("Escreva o nome do arquivo");
            name = Console.ReadLine();

            path += name;

            Token t = new Token(path);
            List<Token.Tok> lt = t.ReadFile();
            foreach(var l in lt)
            {
                Console.WriteLine("<{0},{1}>", l.a, l.s);
            }
            Console.Read();*/
            Sintatico s = new Sintatico();
            s.calculaFirst();
            foreach (var t in s.first) {
                foreach (var h in t.Value) {
                    Console.WriteLine("FI("+t.Key+") = "+h);
                }
            }
        }
    }
}