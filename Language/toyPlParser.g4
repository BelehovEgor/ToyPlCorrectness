parser grammar toyPlParser;
options { tokenVocab=toyPlLexer; }

var : ID ;

expr
    : var
    | INT
    | '(' expr int_op expr ')'
    | '(' expr '!' ')'
    ;

int_op
    : '+'
    | '-'
    | '*'
    | '/'
    | '%'
    | '!'
    ;

cond_int_op
    : '='
    | '/='
    | '>'
    | '<'
    | '>='
    | '<='
    ;

cond_bool_op
    : '&&'
    | '||'
    ;

cond
    : '(' expr cond_int_op expr ')'
    | '(' '!' cond ')'
    | '(' cond cond_bool_op cond ')'
    ;
    
statement
    : var ':=' expr
    | '(' statement ';' statement ')'
    | '(' 'if' cond 'then' statement 'else' statement ')'
    | '(' '{' cond '}' 'while' cond 'do' statement ')'
    ;

program 
    : '{' cond '}' statement '{' cond '}' 
    ;
