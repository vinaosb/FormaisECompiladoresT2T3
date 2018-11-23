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
            STMTX,
            MATCHED_IF,
            OPEN_IF,
            OPEN_IF2,
            LOC,
            LOCS,
            BOOL,
            BOOL2,
            JOIN,
            JOIN2,
            EQUALITY,
            EQUALITY2,
            REL,
            REL2,
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
            public String terminal;
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
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = null});
                        llp.Add(lp);
                        break;
                    case NonTerminal.BLOCK:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "{"});
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = null});
                        lp.Add(new prod { nonterminal = NonTerminal.STMTS, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "}" });
                        llp.Add(lp);
                        break;
                    case NonTerminal.DECLS:
                        lp.Add(new prod { nonterminal = NonTerminal.DECL, terminal = null});
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = null});
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = null});
                        llp.Add(lp);
                        break;
                    case NonTerminal.DECL:
                        lp.Add(new prod { nonterminal = NonTerminal.TYPE, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "id" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ";" });
                        llp.Add(lp);
                        break;
                    case NonTerminal.TYPE:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "basic"});
                        lp.Add(new prod { nonterminal = NonTerminal.TYPES, terminal = null });
                        llp.Add(lp);
                        break;
                    case NonTerminal.TYPES:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "[" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "num" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "]" });
                        lp.Add(new prod { nonterminal = NonTerminal.TYPES, terminal = null });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = null });
                        llp.Add(lp);
                        break;
                    case NonTerminal.STMTS:
                        lp.Add(new prod { nonterminal = NonTerminal.STMT, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTS, terminal = null });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = null });
                        llp.Add(lp);
                        break;
                    case NonTerminal.STMT:
                        lp.Add(new prod { nonterminal = NonTerminal.LOC, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "=" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = ";" });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.OPEN_IF, terminal = null });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "while" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "(" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ")" });
                        lp.Add(new prod { nonterminal = NonTerminal.STMT, terminal = null });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "do" });
                        lp.Add(new prod { nonterminal = NonTerminal.STMT, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "while" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "(" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ")" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ";"});
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "break" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ";" });                    
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = null });
                        llp.Add(lp);
                        break;
                    case NonTerminal.STMTX:
                        lp.Add(new prod { nonterminal = NonTerminal.LOC, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "=" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = ";" });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();                        
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "while" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "(" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ")" });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = null });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "do" });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "while" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "(" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ")" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ";" });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "break" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ";" });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = null });
                        llp.Add(lp);
                        break;
                    /* MATCHED_IF desconsiderar
                    case NonTerminal.MATCHED_IF:
                        break; */
                    case NonTerminal.OPEN_IF:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "if" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "(" });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = ")" });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "then"});
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = null });
                        lp.Add(new prod { nonterminal = NonTerminal.OPEN_IF2, terminal = null });
                        llp.Add(lp);
                        break;
                    case NonTerminal.OPEN_IF2:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = "else" });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = null });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = null });
                        llp.Add(lp);
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
