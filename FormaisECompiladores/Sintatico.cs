using System;
using System.Collections.Generic;
using System.Text;

namespace FormaisECompiladores
{
    public class Sintatico
    {
        public enum NonTerminal
        {
            PROGRAM,
            BLOCK,
            DECLS,
            DECL,
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
            FACTOR,
            EMPTY // Auxiliar pro sintatico
        };

        public struct prod
        {
            public NonTerminal nonterminal { get; set; }
            public Token.Terminals terminal { get; set; }
        }

        public Dictionary<NonTerminal, List<List<prod>>> Producoes { get; set; }

        public Sintatico()
        {
            Producoes = new Dictionary<NonTerminal, List<List<prod>>>();
            initProd();
        }

        private void initProd()
        {
            List<prod> lp;
            List<List<prod>> llp;
            foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                lp = new List<prod>();
                llp = new List<List<prod>>();
                switch (nt)
                {
                    case NonTerminal.PROGRAM:
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = Token.Terminals.EMPTY });
                        break;
                    case NonTerminal.BLOCK:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.BRKTPARE });
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTS, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.BRKTPARE });
                        break;
                    case NonTerminal.DECLS:
                        lp.Add(new prod { nonterminal = NonTerminal.DECL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.DECL:
                        break;
                    case NonTerminal.TYPE:
                        break;
                    case NonTerminal.TYPES:
                        break;
                    case NonTerminal.STMTS:
                        break;
                    case NonTerminal.STMT:
                        break;
                    case NonTerminal.MATCHED_IF:
                        break;
                    case NonTerminal.OPEN_IF:
                        break;
                    case NonTerminal.LOC:
                        break;
                    case NonTerminal.LOCS:
                        break;
                    case NonTerminal.BOOL:
                        break;
                    case NonTerminal.JOIN:
                        break;
                    case NonTerminal.EQUALITY:
                        break;
                    case NonTerminal.REL:
                        break;
                    case NonTerminal.EXPR:
                        break;
                    case NonTerminal.EXPRS:
                        break;
                    case NonTerminal.TERM:
                        break;
                    case NonTerminal.TERMS:
                        break;
                    case NonTerminal.UNARY:
                        break;
                    case NonTerminal.FACTOR:
                        break;
                    default:
                        break;
                }
                
                Producoes.Add(nt, llp);
            }
        }
    }
}
