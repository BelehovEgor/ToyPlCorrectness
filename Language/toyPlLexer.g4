// DELETE THIS CONTENT IF YOU PUT COMBINED GRAMMAR IN Parser TAB
lexer grammar toyPlLexer;

ASSIGN : ':=' ;

EQ : '=' ;
NOT_EQ : '/=';
GT : '>';
LW : '<';
GTEQ : '>=';
LWEQ : '<=';

SEMI : ';' ;
LPAREN : '(' ;
RPAREN : ')' ;
SUM_OP : '+';
MINUS_OP : '-';
TIMES_OP : '*';
DIV_OP : '/';
MOD_OP : '%';
AND_OP : '&&';
OR_OP : '||';
NOT : '!' ;
LCOND : '{' ;
RCOND : '}' ;

IF : 'if';
THEN : 'then';
ELSE : 'else';

WHILE : 'while';
DO : 'do';


INT : [0-9]+ ;
ID: [a-zA-Z_][a-zA-Z_0-9]* ;
WS: [ \t\n\r\f]+ -> skip ;
