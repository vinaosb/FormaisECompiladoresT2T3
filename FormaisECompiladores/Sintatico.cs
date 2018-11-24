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
        public Dictionary<prod, List<prod>> ReferenceTable { get; set; }
        public Dictionary<NonTerminal, HashSet<Token.Terminals>> Follows { get; set; }


        public Sintatico()
        {
            Producoes = new Dictionary<NonTerminal, List<List<prod>>>();
            initProd();
            ReferenceTable = new Dictionary<prod, List<prod>>();
            initRefTable();
            Follows = new Dictionary<NonTerminal, HashSet<Token.Terminals>>();
            GenFollows();
            printFirst();
            printFollow();


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
                        lp.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.SEPARATOR });
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
                        lp.Add(new prod { nonterminal = NonTerminal.LOC, terminal = Token.Terminals.EMPTY });                        
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
        private void initRefTable()
        {
            List<List<prod>> llp;
            //Token.Terminals t;
            foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                bool hasEpson = false;
                if (nt.Equals(NonTerminal.EMPTY))//pois nao queremos calcular o First do Empty
                    continue;
                llp = Producoes[nt];
                //Cada lp eh uma producao NT -> X
                foreach(List<prod> lp in llp)
                {
                    List<Token.Terminals> lt = getFirstFromProd(lp);
                    foreach(Token.Terminals t in lt)
                    {
                        if (t.Equals(Token.Terminals.EMPTY)) {
                            hasEpson = true;
                            continue;//pois epson nao entra na tabela de analise
                        }

                        if(!ReferenceTable.ContainsKey(new prod { nonterminal = nt, terminal = t }))//so por enquanto
                            ReferenceTable.Add(new prod { nonterminal = nt, terminal = t }, lp);

                    }
                    if (hasEpson)
                    {
                        lt.Clear();
                        lt = fixedFollow(nt);
                        foreach (Token.Terminals t in lt) //sao os terminais do Follow
                        {
                            List<prod> lpEpson = new List<prod>();
                            lpEpson.Add(new prod { nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.EMPTY });
                            ReferenceTable.Add(new prod { nonterminal = nt, terminal = t }, lpEpson);
                        }
                    }

                }
            }
        }

        private List<Token.Terminals> getFirstFromProd(List<prod> lp)
        {
            List<Token.Terminals> lt = new List<Token.Terminals>();
            
            //Esse metodo usa o metodo First para pegar o First da producao a direita
            //ex: S -> ABc
            //vai retornar o first(A), se no first(A) tiver epson, inclui o first(B)
            if (lp[0].nonterminal.Equals(NonTerminal.EMPTY))
            {                
                lt.Add(lp[0].terminal);
                return lt;
            }
            //else First
            foreach(prod p in lp)
            {
                if (p.nonterminal.Equals(NonTerminal.EMPTY))
                {
                    lt.Add(p.terminal);
                    return lt;
                }
                //lt = fixedFirst(p.nonterminal);
                lt.AddRange(fixedFirst(p.nonterminal).FindAll((x) => !lt.Contains(x)));
                if (!fixedFirst(p.nonterminal).Contains(Token.Terminals.EMPTY))
                    return lt;
            }
            
            //lt.Add(Token.Terminals.BASIC);//pra testar so.
            return lt;
        }
        private List<Token.Terminals> fixedFirst(NonTerminal n)
        {
            List<Token.Terminals> first = new List<Token.Terminals>();
            switch(n){
                case(NonTerminal.PROGRAM):
                    first.Add(Token.Terminals.OPENBRACE);
                    break;
                case (NonTerminal.BLOCK):
                    first.Add(Token.Terminals.OPENBRACE);
                    break;
                case (NonTerminal.DECLS):
                    first.Add(Token.Terminals.BASIC);
                    first.Add(Token.Terminals.EMPTY);
                    break;
                case (NonTerminal.DECL):
                    first.Add(Token.Terminals.BASIC);
                    break;
                case (NonTerminal.TYPE):
                    first.Add(Token.Terminals.BASIC);
                    break;
                case (NonTerminal.TYPES):
                    first.Add(Token.Terminals.OPENBRKT);
                    first.Add(Token.Terminals.EMPTY);
                    break;
                case (NonTerminal.STMTS):
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.IF);
                    first.Add(Token.Terminals.WHILE);
                    first.Add(Token.Terminals.DO);
                    first.Add(Token.Terminals.BREAK);
                    first.Add(Token.Terminals.OPENBRACE);
                    first.Add(Token.Terminals.EMPTY);
                    break;
                case (NonTerminal.STMT):
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.IF);
                    first.Add(Token.Terminals.WHILE);
                    first.Add(Token.Terminals.DO);
                    first.Add(Token.Terminals.BREAK);
                    first.Add(Token.Terminals.OPENBRACE);
                    break;
                case (NonTerminal.STMTX):
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.WHILE);
                    first.Add(Token.Terminals.DO);
                    first.Add(Token.Terminals.BREAK);
                    first.Add(Token.Terminals.OPENBRACE);
                    break;
                case NonTerminal.OPEN_IF:
                    first.Add(Token.Terminals.IF);
                  
                    break;
                case NonTerminal.OPEN_IF2:
                    first.Add(Token.Terminals.ELSE);
                    first.Add(Token.Terminals.EMPTY);      
                   
                    break;
                case NonTerminal.LOC:
                    first.Add(Token.Terminals.ID);
                 
                    break;
                case NonTerminal.LOCS:
                    first.Add(Token.Terminals.OPENBRKT);
                    first.Add(Token.Terminals.EMPTY);
                  
                    break;
                case NonTerminal.BOOL:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.BOOL2:
                    first.Add(Token.Terminals.OR);
                    first.Add(Token.Terminals.EMPTY);
                    
                    break;
                case NonTerminal.JOIN:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.JOIN2:
                    first.Add(Token.Terminals.AND);
                    first.Add(Token.Terminals.EMPTY);
           
                    break;
                case NonTerminal.EQUALITY:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.EQUALITY2:
                    first.Add(Token.Terminals.EQ);
                    first.Add(Token.Terminals.NE);
                    first.Add(Token.Terminals.EMPTY);
            
                    break;
                case NonTerminal.REL:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.REL2:
                    first.Add(Token.Terminals.LT);
                    first.Add(Token.Terminals.LE);
                    first.Add(Token.Terminals.GT);
                    first.Add(Token.Terminals.GE);
                    first.Add(Token.Terminals.EMPTY);
                  
                    break;
                case NonTerminal.EXPR:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.EXPRS:
                    first.Add(Token.Terminals.ADD);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.EMPTY);
           
                    break;
                case NonTerminal.TERM:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.TERMS:
                    first.Add(Token.Terminals.MULTIPLY);
                    first.Add(Token.Terminals.DIVIDE);
                    first.Add(Token.Terminals.EMPTY);
          
                    break;
                case NonTerminal.UNARY:
                    first.Add(Token.Terminals.NOT);
                    first.Add(Token.Terminals.MINUS);
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;
                case NonTerminal.FACTOR:
                    first.Add(Token.Terminals.OPENPARENT);
                    first.Add(Token.Terminals.ID);
                    first.Add(Token.Terminals.NUM);
                    first.Add(Token.Terminals.REAL);
                    first.Add(Token.Terminals.TRUE);
                    first.Add(Token.Terminals.FALSE);
                    break;

                default:
                    break;
            }
            return first;
        }
        private List<Token.Terminals> fixedFollow(NonTerminal n)
        {
            List<Token.Terminals> follow = new List<Token.Terminals>();
            switch (n)
            {
                case (NonTerminal.PROGRAM):
                    follow.Add(Token.Terminals.DOLLAR);
                    break;
                case (NonTerminal.BLOCK):
                    follow.Add(Token.Terminals.DOLLAR);
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.IF);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.CLOSEBRACE);
                    break;
                case (NonTerminal.DECLS):
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.IF);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.CLOSEBRACE);
                    break;
                case (NonTerminal.DECL):
                    follow.Add(Token.Terminals.BASIC);
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.IF);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.CLOSEBRACE);
                    break;
                case (NonTerminal.TYPE):
                    follow.Add(Token.Terminals.ID);
                    break;
                case (NonTerminal.TYPES):
                    follow.Add(Token.Terminals.ID);
                    break;
                case (NonTerminal.STMTS):
                    follow.Add(Token.Terminals.CLOSEBRACE);    
                    break;
                case (NonTerminal.STMT):
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.IF);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.CLOSEBRACE);
                    break;
                case (NonTerminal.STMTX):
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.ELSE);
                    follow.Add(Token.Terminals.CLOSEBRACE);
                    follow.Add(Token.Terminals.IF);
                    break;
                case NonTerminal.OPEN_IF:
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.IF);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.CLOSEBRACE);

                    break;
                case NonTerminal.OPEN_IF2:
                    follow.Add(Token.Terminals.ID);
                    follow.Add(Token.Terminals.IF);
                    follow.Add(Token.Terminals.WHILE);
                    follow.Add(Token.Terminals.DO);
                    follow.Add(Token.Terminals.BREAK);
                    follow.Add(Token.Terminals.OPENBRACE);
                    follow.Add(Token.Terminals.CLOSEBRACE);

                    break;
                case NonTerminal.LOC:
                    follow.Add(Token.Terminals.EQ);
                    follow.Add(Token.Terminals.MULTIPLY);
                    follow.Add(Token.Terminals.DIVIDE);
                    follow.Add(Token.Terminals.ADD);
                    follow.Add(Token.Terminals.MINUS);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.ASSERT);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    break;
                case NonTerminal.LOCS:
                    follow.Add(Token.Terminals.EQ);
                    follow.Add(Token.Terminals.MULTIPLY);
                    follow.Add(Token.Terminals.DIVIDE);
                    follow.Add(Token.Terminals.ADD);
                    follow.Add(Token.Terminals.MINUS);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.ASSERT);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);

                    break;
                case NonTerminal.BOOL:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    break;
                case NonTerminal.BOOL2:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);

                    break;
                case NonTerminal.JOIN:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    follow.Add(Token.Terminals.OR);
                    break;
                case NonTerminal.JOIN2:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    follow.Add(Token.Terminals.OR);

                    break;
                case NonTerminal.EQUALITY:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.AND);
                    break;
                case NonTerminal.EQUALITY2:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.AND);

                    break;
                case NonTerminal.REL:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.EQ);
                    break;
                case NonTerminal.REL2:
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.EQ);

                    break;
                case NonTerminal.EXPR:
                    follow.Add(Token.Terminals.EQ);                
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    break;
                case NonTerminal.EXPRS:
                    follow.Add(Token.Terminals.EQ);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);

                    break;
                case NonTerminal.TERM:
                    follow.Add(Token.Terminals.EQ);
                 
                    follow.Add(Token.Terminals.ADD);
                    follow.Add(Token.Terminals.MINUS);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    break;
                case NonTerminal.TERMS:
                    follow.Add(Token.Terminals.EQ);

                    follow.Add(Token.Terminals.ADD);
                    follow.Add(Token.Terminals.MINUS);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);

                    break;
                case NonTerminal.UNARY:
                    follow.Add(Token.Terminals.EQ);
                    follow.Add(Token.Terminals.MULTIPLY);
                    follow.Add(Token.Terminals.DIVIDE);
                    follow.Add(Token.Terminals.ADD);
                    follow.Add(Token.Terminals.MINUS);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);           
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    break;
                case NonTerminal.FACTOR:
                    follow.Add(Token.Terminals.EQ);
                    follow.Add(Token.Terminals.MULTIPLY);
                    follow.Add(Token.Terminals.DIVIDE);
                    follow.Add(Token.Terminals.ADD);
                    follow.Add(Token.Terminals.MINUS);
                    follow.Add(Token.Terminals.LT);
                    follow.Add(Token.Terminals.LE);
                    follow.Add(Token.Terminals.GT);
                    follow.Add(Token.Terminals.GE);
                    follow.Add(Token.Terminals.NE);
                    follow.Add(Token.Terminals.AND);
                    follow.Add(Token.Terminals.OR);
                    follow.Add(Token.Terminals.SEPARATOR);
                    follow.Add(Token.Terminals.CLOSEBRKT);
                    follow.Add(Token.Terminals.CLOSEPARENT);
                    break;

                default:
                    break;
            }
            return follow;
        }

        private HashSet<Token.Terminals> First(NonTerminal nt)
        {
            HashSet<Token.Terminals> ret = new HashSet<Token.Terminals>();

            List<List<prod>> llp = Producoes.GetValueOrDefault(nt);

            foreach (var lp in llp)
            {
                int i = -1;
                do
                {
                    i++;
                    if (lp[i].terminal != Token.Terminals.EMPTY)
                    {
                        ret.Add(lp[i].terminal);
                        continue;
                    }

                    ret.UnionWith(First(lp[i].nonterminal));

                } while (NextHasEmpty(lp[i].nonterminal));

            }

            if (ret.Count == 0)
                ret.Add(Token.Terminals.EMPTY);

            return ret;
        }

        private bool NextHasEmpty(NonTerminal nt)
        {
            List<List<prod>> llp = Producoes.GetValueOrDefault(nt);
            
            foreach (var lp in llp)
                if (lp.Contains(new prod { terminal = Token.Terminals.EMPTY, nonterminal = NonTerminal.EMPTY }))
                    return true;
            return false;
        }

        private void GenFollows()
        {
            Follows.Clear();
            foreach(NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                HashSet<Token.Terminals> lt = new HashSet<Token.Terminals>();
                if (nt.Equals(NonTerminal.PROGRAM))
                    lt.Add(Token.Terminals.DOLLAR); // $ no primeiro elemento
                Follows.Add(nt, lt);
            }
            bool houveMudancas;

            do
            {
                houveMudancas = false;

                foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
                {
                    foreach(var lp in Producoes.GetValueOrDefault(nt))
                    {
                        if (lp.Count > 2)
                        {
                            for (int i = 0; i < lp.Count -1; i++)
                            {
                                if (lp[i].nonterminal != NonTerminal.EMPTY && lp[i+1].nonterminal != NonTerminal.EMPTY)  // Regra 2
                                {
                                    Follows.GetValueOrDefault(lp[i].nonterminal).Add(Token.Terminals.EMPTY);
                                    if (!Follows.GetValueOrDefault(lp[i].nonterminal).IsSupersetOf(First(lp[i + 1].nonterminal)))
                                    {
                                        Follows.GetValueOrDefault(lp[i].nonterminal).UnionWith(First(lp[i + 1].nonterminal)); // B <- First(c)
                                        houveMudancas = true;
                                    }
                                    Follows.GetValueOrDefault(lp[i].nonterminal).Remove(Token.Terminals.EMPTY);

                                    if (First(lp[i+1].nonterminal).Contains(Token.Terminals.EMPTY) && 
                                        !Follows.GetValueOrDefault(lp[i].nonterminal).IsSupersetOf(Follows.GetValueOrDefault(nt))) // Regra 3.2
                                    {
                                        Follows.GetValueOrDefault(lp[i].nonterminal).UnionWith(Follows.GetValueOrDefault(nt));
                                        houveMudancas = true;
                                    }

                                    if (i == lp.Count-2) // Regra 3.1
                                    {
                                        if (!Follows.GetValueOrDefault(lp[i+1].nonterminal).IsSupersetOf(Follows.GetValueOrDefault(nt)))
                                        {
                                            Follows.GetValueOrDefault(lp[i + 1].nonterminal).UnionWith(Follows.GetValueOrDefault(nt));
                                            houveMudancas = true;
                                        }
                                    }
                                }

                                else if (lp[i].nonterminal != NonTerminal.EMPTY && lp[i+1].terminal != Token.Terminals.EMPTY)
                                {
                                    if (Follows.GetValueOrDefault(lp[i].nonterminal).Add(lp[i + 1].terminal))
                                        houveMudancas = true;
                                }
                            }
                        }

                        if (lp.Count == 2) // Regra 3.1
                        {
                            if (lp[1].nonterminal != NonTerminal.EMPTY)
                            {
                                if (lp[0].nonterminal != NonTerminal.EMPTY) // regra 2
                                {
                                    Follows.GetValueOrDefault(lp[0].nonterminal).Add(Token.Terminals.EMPTY);
                                    if (!Follows.GetValueOrDefault(lp[0].nonterminal).IsSupersetOf(First(lp[1].nonterminal)))
                                    {
                                        Follows.GetValueOrDefault(lp[0].nonterminal).UnionWith(First(lp[1].nonterminal));
                                        houveMudancas = true;
                                    }
                                    Follows.GetValueOrDefault(lp[0].nonterminal).Remove(Token.Terminals.EMPTY);
                                }
                                if (!Follows.GetValueOrDefault(lp[1].nonterminal).IsSupersetOf(Follows.GetValueOrDefault(nt)))
                                    houveMudancas = true;
                                Follows.GetValueOrDefault(lp[1].nonterminal).UnionWith(Follows.GetValueOrDefault(nt));
                            }
                            if (lp[1].terminal != Token.Terminals.EMPTY && lp[0].nonterminal != NonTerminal.EMPTY) // Regra 2
                                if (Follows.GetValueOrDefault(lp[0].nonterminal).Add(lp[1].terminal))
                                    houveMudancas = true;
                        }

                        if (lp.Count <= 2) // Regra 3.1
                            if (lp[0].nonterminal != NonTerminal.EMPTY)
                            {
                                if (!Follows.GetValueOrDefault(lp[0].nonterminal).IsSupersetOf(Follows.GetValueOrDefault(nt)))
                                    houveMudancas = true;
                                Follows.GetValueOrDefault(lp[0].nonterminal).UnionWith(Follows.GetValueOrDefault(nt));
                            }
                    }
                }
            } while (houveMudancas);
        }

        public void printFirst()
        {
            foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                HashSet<Token.Terminals> list = First(nt);
                string term = "";
                foreach (var t in list)
                {
                    term += t.ToString() + ",";
                }
                Console.WriteLine("First({0}):{1}", nt.ToString(), term);
            }
        }

        public void printFollow()
        {
            Console.Write("\n\n\nFollows:\n");
            foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                string term = "";
                foreach (var follows in Follows.GetValueOrDefault(nt))
                {
                    term += follows.ToString() + ",";
                }
                Console.WriteLine("Follow({0}): {1}", nt.ToString(), term);
            }
        }
    }
}
