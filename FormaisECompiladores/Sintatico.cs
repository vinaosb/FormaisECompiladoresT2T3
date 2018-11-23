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
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = Token.Terminals.EMPTY});
                        llp.Add(lp);
                        break;
                    case NonTerminal.BLOCK:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENBRACE});
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = Token.Terminals.EMPTY});
                        lp.Add(new prod { nonterminal = NonTerminal.STMTS, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEBRACE });
                        llp.Add(lp);
                        break;
                    case NonTerminal.DECLS:
                        lp.Add(new prod { nonterminal = NonTerminal.DECL, terminal = Token.Terminals.EMPTY});
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = Token.Terminals.EMPTY});
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY});
                        llp.Add(lp);
                        break;
                    case NonTerminal.DECL:
                        lp.Add(new prod { nonterminal = NonTerminal.TYPE, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.ID });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.SEPARATOR });
                        llp.Add(lp);
                        break;
                    case NonTerminal.TYPE:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.BASIC });
                        lp.Add(new prod { nonterminal = NonTerminal.TYPES, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.TYPES:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENBRKT });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.NUM });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEBRKT });
                        lp.Add(new prod { nonterminal = NonTerminal.TYPES, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.STMTS:
                        lp.Add(new prod { nonterminal = NonTerminal.STMT, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.STMT:
                        lp.Add(new prod { nonterminal = NonTerminal.LOC, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.ASSERT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = Token.Terminals.SEPARATOR });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.OPEN_IF, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.WHILE });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals. OPENPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.STMT, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.DO });
                        lp.Add(new prod { nonterminal = NonTerminal.STMT, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals. WHILE});
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals. CLOSEPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.SEPARATOR });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals. BREAK });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.SEPARATOR });                    
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.STMTX:
                        lp.Add(new prod { nonterminal = NonTerminal.LOC, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.ASSERT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.DECLS, terminal = Token.Terminals. SEPARATOR });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();                        
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.WHILE });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.DO });
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.WHILE });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.SEPARATOR});
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.BREAK });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.SEPARATOR });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.BLOCK, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    /* MATCHED_IF desconsiderar
                    case NonTerminal.MATCHED_IF:
                        break; */
                    case NonTerminal.OPEN_IF:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.IF });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.THEN});
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.OPEN_IF2, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.OPEN_IF2:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.ELSE});
                        lp.Add(new prod { nonterminal = NonTerminal.STMTX, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
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
