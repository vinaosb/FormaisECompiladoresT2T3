﻿using System;
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
            //MATCHED_IF,
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
        public Dictionary<NonTerminal, List<Token.Terminals>> first;
        public Dictionary<NonTerminal, HashSet<Token.Terminals>> Follows { get; set; }


        public Sintatico()
        {
            Producoes = new Dictionary<NonTerminal, List<List<prod>>>();
            initProd();
            first = new Dictionary<NonTerminal, List<Token.Terminals>>();
            Follows = new Dictionary<NonTerminal, HashSet<Token.Terminals>>();
            GenFollows();
            printFirst();
            printFollow();
            ReferenceTable = new Dictionary<prod, List<prod>>();
            initRefTable();

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
                        foreach (Token.Terminals t in Follows[nt]) //sao os terminais do Follow
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
                lt.AddRange(new List<Token.Terminals>(First(p.nonterminal)).FindAll((x) => !lt.Contains(x)));
               // lt.AddRange(fixedFirst(p.nonterminal).FindAll((x) => !lt.Contains(x)));
                if (!First(p.nonterminal).Contains(Token.Terminals.EMPTY))
                    return lt;
                
            }
            
            //lt.Add(Token.Terminals.BASIC);//pra testar so.
            return lt;
        }
        public void calculaFirst()
        {
            List<NonTerminal> naoTerminais = new List<NonTerminal>();
            foreach (var t in Producoes)
            {
                List<Token.Terminals> terminais = new List<Token.Terminals>();
                procuraFirst(t.Key, terminais);
                first.Add(t.Key, terminais);
            }
        }

        public void procuraFirst(NonTerminal naoTerminalAtual, List<Token.Terminals> terminais)
        {
            List<List<prod>> p = Producoes[naoTerminalAtual];
            foreach (var t in p)
            {
                int i = 0;
                foreach (var h in t)
                {
                    if (i == 0)
                    {
                        if (h.nonterminal != NonTerminal.EMPTY)
                        {
                            if (h.terminal == Token.Terminals.EMPTY)
                            {
                                procuraFirst(h.nonterminal, terminais);
                            }
                        }
                        else
                        {
                            terminais.Add(h.terminal);
                        }
                    }
                    i++;
                }
            }
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
                    lt.Add(Token.Terminals.DOLLAR); // $ no primeiro elemento - Regra 1
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
                        for (int i = 0; i < lp.Count; i++)
                        {
                            if (lp[i].nonterminal != NonTerminal.EMPTY && i == lp.Count - 1) // Regra 3.1
                            {
                                if (!Follows.GetValueOrDefault(lp[i].nonterminal).IsSupersetOf(Follows.GetValueOrDefault(nt)))
                                    houveMudancas = true;
                                Follows.GetValueOrDefault(lp[i].nonterminal).UnionWith(Follows.GetValueOrDefault(nt));
                                break;
                            }
                            else if (lp[i].nonterminal != NonTerminal.EMPTY && lp[i + 1].nonterminal != NonTerminal.EMPTY)
                            {
                                HashSet<Token.Terminals> temp = First(lp[i + 1].nonterminal);
                                temp.Remove(Token.Terminals.EMPTY);

                                if (!Follows.GetValueOrDefault(lp[i].nonterminal).IsSupersetOf(temp))
                                    houveMudancas = true;
                                Follows.GetValueOrDefault(lp[i].nonterminal).UnionWith(temp); // Regra 2.2


                                if (!Follows.GetValueOrDefault(lp[i].nonterminal).IsSupersetOf(Follows.GetValueOrDefault(lp[i+1].nonterminal))) // Regra 3.2
                                    houveMudancas = true;
                                Follows.GetValueOrDefault(lp[i].nonterminal).UnionWith(Follows.GetValueOrDefault(lp[i + 1].nonterminal));
                            }
                            else if (lp[i].nonterminal != NonTerminal.EMPTY && lp[i+1].terminal != Token.Terminals.EMPTY) // Regra 2.1
                            {
                                if (Follows.GetValueOrDefault(lp[i].nonterminal).Add(lp[i + 1].terminal))
                                    houveMudancas = true;
                            }
                            
                        }
                        
                    }
                }
            } while (houveMudancas);
        }

        public void printFirst()
        {
            foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                if (nt.Equals(NonTerminal.EMPTY))
                    continue;
                HashSet<Token.Terminals> list = First(nt);
                string term = "";
                foreach (var t in list)
                {
                    term += t.ToString() + ",";
                }
                term = term.Replace("EMPTY", "ɛ");
                Console.WriteLine("First({0}):{1}", nt.ToString(), term);
            }
        }

        public void printFollow()
        {
            Console.Write("\n\n\nFollows:\n");
            foreach (NonTerminal nt in Enum.GetValues(typeof(NonTerminal)))
            {
                if (nt.Equals(NonTerminal.EMPTY))
                    continue;
                string term = "";
                foreach (var follows in Follows.GetValueOrDefault(nt))
                {
                    term += follows.ToString() + ",";
                }
                term = term.Replace("EMPTY", "ɛ");
                Console.WriteLine("Follow({0}): {1}", nt.ToString(), term);
            }
        }
        public bool predictiveParser(List<Token.Tok> toks)
        {
            Console.WriteLine("");
            Console.WriteLine("Parser: (Pilha)");
            Console.WriteLine(String.Format("|{0,-70}|{1,-70}|", "Stack","Matched"));
            Console.WriteLine(String.Format("|{0,70}|{0,70}|", "PROGRAM $"));
            string output = "";
            bool exit=true;
            toks = checkDollarSign(toks);
            Stack<prod> pilha = new Stack<prod>();
            List<prod> newItems = new List<prod>();
            pilha.Push(new prod{nonterminal = NonTerminal.EMPTY, terminal = Token.Terminals.DOLLAR });
            pilha.Push(new prod { nonterminal = NonTerminal.PROGRAM, terminal = Token.Terminals.EMPTY });
            foreach (var token in toks)
            {
                bool searchingTerminal = true;
                while (searchingTerminal)
                {
                    newItems = new List<prod>();
                    newItems.Clear();
                    if (token.t.Equals(pilha.Peek().terminal))
                    {
                        pilha.Pop();
                        searchingTerminal = false;
                        output += token.s + " ";
                        if (token.s == "$")
                            return true;
                    }
                    else if (pilha.Peek().nonterminal.Equals(NonTerminal.EMPTY))
                    {
                        //terminal diferente da entrada
                        pilha.Pop();
                        output += token.s + " ";
                        exit = false;
                        
                    }
                    else //NonTerminal para trocar
                    {
                        NonTerminal nt = pilha.Pop().nonterminal;
                        prod key = new prod{nonterminal=nt,terminal=token.t };
                        newItems = ReferenceTable[key];
                        if (newItems[0].terminal.Equals(Token.Terminals.EMPTY)
                            && newItems[0].nonterminal.Equals(NonTerminal.EMPTY))
                            newItems.Reverse();
                        else
                        {
                            newItems.Reverse();
                            foreach (prod p in newItems)
                            {
                                pilha.Push(p);
                            }
                            newItems.Reverse();//obrigatorio
                        }
                    }
      
                    string st="";
                    foreach (var p in pilha)
                    {
                        if (p.nonterminal.Equals(NonTerminal.EMPTY))
                        {
                            if (p.terminal.Equals(Token.Terminals.ID))
                            {
                                st += p.terminal + " ";
                                continue;
                            }
                            foreach (var t in toks)
                            {
                                if (t.t.Equals(p.terminal))
                                {
                                    st += t.s + " ";
                                    break;
                                }
                            }
                        }
                        else
                            st += p.nonterminal + " ";
                    }
                    //Console.WriteLine(st + "  | " + output);

                    Console.WriteLine(String.Format("|{0,70}|{1,70}|",st , output));
                    if(!exit)
                        return false;
                }
            }

            return true;
        }

        private List<Token.Tok> checkDollarSign(List<Token.Tok> toks)
        {
            if (!toks[toks.Count - 1].t.Equals(Token.Terminals.DOLLAR))
            {
                Token.Tok token = new Token.Tok();
                token.s = "$";
                token.t = Token.Terminals.DOLLAR;
                toks.Add(token);
            }
            return toks;
        }
    }
}
