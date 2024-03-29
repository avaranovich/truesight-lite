using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Truesight.Decompiler.Hir.Core.ControlFlow;
using Truesight.Decompiler.Hir.Core.Expressions;
using Truesight.Decompiler.Hir.Core.Special;
using Truesight.Decompiler.Hir.Traversal;
using XenoGears.Functional;
using XenoGears.Assertions;
using XenoGears.Reflection;
using XenoGears.Strings;
using Truesight.Decompiler.Hir.Traversal.Exceptions;
using Truesight.Decompiler.Hir.TypeInference;
using HirConvert = Truesight.Decompiler.Hir.Core.Expressions.Convert;

namespace Truesight.Decompiler.Hir.Prettyprint
{
    public partial class CSharpPrettyprinter
    {
        private bool IsOneliner(Node node) { return node == null || node is Expression || node is Return || node is Break || node is Continue || node is Label || node is Goto || node is Throw; }

        protected internal override void TraverseNull(Null @null)
        {
            _writer.Write("?");
        }

        protected internal override void TraverseAddr(Addr addr)
        {
            _writer.Write("&");
            Traverse(addr.Target);
        }

        protected internal override void TraverseAssign(Assign ass)
        {
            Traverse(ass.Lhs);
            _writer.Write(" = ");
            Traverse(ass.Rhs);
        }

        protected internal override void TraverseCollectionInit(CollectionInit ci)
        {
            Action<IEnumerable<Expression>> traverseElements = args => args.ForEach((arg, i) =>
            {
                Traverse(arg);
                if (i != args.Count() - 1) _writer.Write(", ");
            });

            Traverse(ci.Ctor);
            _writer.Write("{");
            traverseElements(ci.Elements);
            _writer.Write("}");
        }

        protected internal override void TraverseConditional(Conditional cond)
        {
            Traverse(cond.Test);
            _writer.Write(" ? ");
            Traverse(cond.IfTrue);
            _writer.Write(" : ");
            Traverse(cond.IfFalse);
        }

        protected internal override void TraverseConst(Const @const)
        {
            if (@const.Value == null)
            {
                _writer.Write("null");
            }
            else
            {
                var value = @const.Value.UndecorateNullable();
                if (value.GetType().IsNumeric())
                {
                    _writer.Write(value.ToInvariantString());
                }
                else if (value is bool)
                {
                    _writer.Write(value.ToInvariantString().ToLower());
                }
                else if (value is char)
                {
                    _writer.Write((char)value);
                }
                else if (value is String)
                {
                    _writer.Write(((String)value).Quote());
                }
                else if (value is Type)
                {
                    var t = (Type)value;
                    var s_t = t.GetCSharpRef(ToCSharpOptions.Informative);
                    _writer.Write("typeof({0})", s_t);
                }
                else if (value is FieldInfo)
                {
                    var f = (FieldInfo)value;
                    var s_f = f.GetCSharpRef(ToCSharpOptions.Informative);
                    _writer.Write("fieldof({0})", s_f);
                }
                else if (value is MethodBase)
                {
                    var m = (MethodBase)value;
                    var s_m = m.GetCSharpRef(ToCSharpOptions.Informative);
                    _writer.Write("methodof({0})", s_m);
                }
                else
                {
                    _writer.Write("{");
                    _writer.Write(value);
                    _writer.Write("}");
                }
            }
        }

        protected internal override void TraverseConvert(HirConvert cvt)
        {
            // this clause is necessary to correctly prettyprint boxes
            if (cvt.Type == typeof(Object))
            {
                Traverse(cvt.Source);
            }
            else
            {
                _writer.Write("(");
                _writer.Write(cvt.Type.GetCSharpRef(ToCSharpOptions.Informative));
                _writer.Write(")");
                Traverse(cvt.Source);
            }
        }

        protected internal override void TraverseDeref(Deref deref)
        {
            var t = deref.Type();
            var is_ptr = t == null || !t.IsByRef;
            if (is_ptr) _writer.Write("*");
            Traverse(deref.Target);
        }

        protected internal override void TraverseFld(Fld fld)
        {
            if (fld.Member.IsStatic())
            {
                fld.This.AssertNull();
                _writer.Write(fld.Member.DeclaringType.GetCSharpRef(ToCSharpOptions.Informative));
            }
            else
            {
                Traverse(fld.This);
            }

            var t_this = fld.This.Type();
            var is_ptr = t_this != null && t_this.IsPointer;
            _writer.Write(is_ptr ? "->" : ".");

            _writer.Write(fld.Member.Name);
        }

        protected internal override void TraverseLoophole(Loophole loophole)
        {
            _writer.Write("<");
            _writer.Write(loophole.Tag ?? loophole.Id.ToString());
            _writer.Write(">");
        }

        protected internal override void TraverseObjectInit(ObjectInit oi)
        {
            Action<IEnumerable<MemberInfo>> traverseMembers = mis => mis.ForEach((mi, i) =>
            {
                (mi is FieldInfo || mi is PropertyInfo).AssertTrue();
                _writer.Write("{0} = ", mi);
                Traverse(oi.MemberInits[mi]);
                if (i != mis.Count() - 1) _writer.Write(", ");
            });

            Traverse(oi.Ctor);
            _writer.Write("{");
            traverseMembers(oi.Members);
            _writer.Write("}");
        }

        protected internal override void TraverseOperator(Operator op)
        {
            var opt = op.OperatorType;
            if (op.IsUnary())
            {
                if (!opt.ToString().StartsWith("Post"))
                    _writer.Write(opt.ToCSharpSymbol());

                Traverse(op.Args.First());

                if (opt.ToString().StartsWith("Post"))
                    _writer.Write(opt.ToCSharpSymbol());
            }
            else if (op.IsBinary())
            {
                Traverse(op.Args.First());
                _writer.Write(" " + opt.ToCSharpSymbol() + " ");
                Traverse(op.Args.Second());
            }
            else
            {
                op.Unsupported();
            }
        }

        protected internal override void TraverseProp(Prop prop)
        {
            if (prop.Member.IsStatic())
            {
                prop.This.AssertNull();
                _writer.Write(prop.Member.DeclaringType.GetCSharpRef(ToCSharpOptions.Informative));
            }
            else
            {
                Traverse(prop.This);
            }

            var t_this = prop.This.Type();
            var is_ptr = t_this != null && t_this.IsPointer;
            _writer.Write(is_ptr ? "->" : ".");

            _writer.Write(prop.Member.Name);
        }

        protected internal override void TraverseRef(Ref @ref)
        {
            _writer.Write(@ref.Sym.Name);
        }

        protected internal override void TraverseSizeof(SizeOf @sizeof)
        {
            var t = @sizeof.Type;
            _writer.Write("sizeof({0})", t == null ? "?" : t.GetCSharpRef(ToCSharpOptions.Informative));
        }

        protected internal override void TraverseTypeIs(TypeIs typeIs)
        {
            Traverse(typeIs.Target);
            _writer.Write(" is ");
            _writer.Write(typeIs.Type == null ? "?" : typeIs.Type.GetCSharpRef(ToCSharpOptions.Informative));
        }

        protected internal override void TraverseTypeAs(TypeAs typeAs)
        {
            Traverse(typeAs.Target);
            _writer.Write(" as ");
            _writer.Write(typeAs.Type == null ? "?" : typeAs.Type.GetCSharpRef(ToCSharpOptions.Informative));
        }

        protected internal override void TraverseBreak(Break @break)
        {
            _writer.Write("break");
        }

        protected internal override void TraverseContinue(Continue @continue)
        {
            _writer.Write("continue");
        }

        protected internal override void TraverseGoto(Goto @goto)
        {
            var label = @goto.ResolveLabel();
            _writer.Write("goto " + label.Name);
        }

        protected internal override void TraverseLabel(Label label)
        {
            _writer.Indent--;
            _writer.Write(label.Name);
            _writer.Indent++;
        }

        protected internal override void TraverseReturn(Return @return)
        {
            _writer.Write("return");
            if (@return.Value != null)
            {
                _writer.Write(" ");
                Traverse(@return.Value);
            }
        }

        protected internal override void TraverseThrow(Throw @throw)
        {
            _writer.Write("throw");
            if (@throw.Exception != null)
            {
                _writer.Write(" ");
                Traverse(@throw.Exception);
            }
        }

        protected internal override void TraverseDefault(Default @default)
        {
            var t = @default.Type;
            _writer.Write("default({0})", t == null ? "?" : t.GetCSharpRef(ToCSharpOptions.Informative));
        }
    }
}