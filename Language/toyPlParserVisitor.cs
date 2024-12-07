//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Language\\toyPlParser.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="toyPlParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface ItoyPlParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar([NotNull] toyPlParser.VarContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] toyPlParser.ExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.int_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInt_op([NotNull] toyPlParser.Int_opContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.cond_int_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCond_int_op([NotNull] toyPlParser.Cond_int_opContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.cond_bool_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCond_bool_op([NotNull] toyPlParser.Cond_bool_opContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.cond"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCond([NotNull] toyPlParser.CondContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] toyPlParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="toyPlParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] toyPlParser.ProgramContext context);
}
