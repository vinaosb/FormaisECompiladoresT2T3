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
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.ID });
                        lp.Add(new prod { nonterminal = NonTerminal.LOCS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.LOCS:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENBRKT});
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEBRKT });
                        lp.Add(new prod { nonterminal = NonTerminal.LOCS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.BOOL:                        
                        lp.Add(new prod { nonterminal = NonTerminal.JOIN, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL2, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.BOOL2:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OR });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });                       
                        llp.Add(lp);
                        break;
                    case NonTerminal.JOIN:
                        lp.Add(new prod { nonterminal = NonTerminal.EQUALITY, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.JOIN2, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.JOIN2:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.AND });
                        lp.Add(new prod { nonterminal = NonTerminal.JOIN, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.EQUALITY:
                        lp.Add(new prod { nonterminal = NonTerminal.REL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EQUALITY2, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.EQUALITY2:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EQ });
                        lp.Add(new prod { nonterminal = NonTerminal.EQUALITY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.NE });
                        lp.Add(new prod { nonterminal = NonTerminal.EQUALITY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.REL:
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.REL2, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.REL2:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.LT });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.LE });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.GT });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.GE });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.EXPR:
                        lp.Add(new prod { nonterminal = NonTerminal.TERM, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPRS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.EXPRS:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.ADD });
                        lp.Add(new prod { nonterminal = NonTerminal.TERM, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.MINUS });
                        lp.Add(new prod { nonterminal = NonTerminal.TERM, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EXPR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.TERM:
                        lp.Add(new prod { nonterminal = NonTerminal.UNARY, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.TERMS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.TERMS:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.MULTIPLY });
                        lp.Add(new prod { nonterminal = NonTerminal.UNARY, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.TERMS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.DIVIDE });
                        lp.Add(new prod { nonterminal = NonTerminal.UNARY, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.TERMS, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.UNARY:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.NOT });
                        lp.Add(new prod { nonterminal = NonTerminal.UNARY, terminal = Token.Terminals.EMPTY });                        
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.MINUS });
                        lp.Add(new prod { nonterminal = NonTerminal.UNARY, terminal = Token.Terminals.EMPTY });                        
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.FACTOR, terminal = Token.Terminals.EMPTY });
                        llp.Add(lp);
                        break;
                    case NonTerminal.FACTOR:
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.OPENPARENT });
                        lp.Add(new prod { nonterminal = NonTerminal.BOOL, terminal = Token.Terminals.EMPTY });
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.CLOSEPARENT });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.LOC, terminal = Token.Terminals.MINUS });                        
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.NUM});
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.REAL });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.TRUE });
                        llp.Add(lp);
                        lp = new List<prod>();
                        lp.Clear();
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.FALSE });
                        llp.Add(lp);
                        break;
                        
                    default:
                        break;
                }
                
                Producoes.Add(nt, llp);
            }
        }
    }
}
