//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Truesight.Parser.Api.Ops
{
    // ldarg.0, ldarg.1, ldarg.2, ldarg.3, ldarg.s, ldarg
    [global::Truesight.Parser.Impl.Ops.OpCodesAttribute(0x02, 0x03, 0x04, 0x05, 0x0e, 0xfe09)]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public sealed class Ldarg : global::Truesight.Parser.Impl.ILOp
    {
        public override global::Truesight.Parser.Api.IILOpType OpType { get { return global::Truesight.Parser.Api.IILOpType.Ldarg; } }

        private readonly int? _constValue;
        private readonly bool _useConstValue;
        private readonly int _value;

        internal Ldarg(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader)
            : this(source, reader, global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
        }

        internal Ldarg(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader, global::System.Collections.ObjectModel.ReadOnlyCollection<global::Truesight.Parser.Impl.ILOp> prefixes)
            : base(source, AssertSupportedOpCode(reader), (int)reader.BaseStream.Position - global::System.Linq.Enumerable.Sum(global::System.Linq.Enumerable.Select(prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()), prefix => prefix.Size)), prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
            // this is necessary for further verification
            var origPos = reader.BaseStream.Position;

            // initializing _constValue
            switch((ushort)OpSpec.OpCode.Value)
            {
                case 0x02: //ldarg.0
                    _constValue = 0;
                    break;
                case 0x03: //ldarg.1
                    _constValue = 1;
                    break;
                case 0x04: //ldarg.2
                    _constValue = 2;
                    break;
                case 0x05: //ldarg.3
                    _constValue = 3;
                    break;
                case 0x0e: //ldarg.s
                case 0xfe09: //ldarg
                    _constValue = default(int?);
                    break;
                default:
                    throw global::XenoGears.Assertions.AssertionHelper.Fail();
            }

            // initializing _useConstValue
            switch((ushort)OpSpec.OpCode.Value)
            {
                case 0x02: //ldarg.0
                case 0x03: //ldarg.1
                case 0x04: //ldarg.2
                case 0x05: //ldarg.3
                    _useConstValue = true;
                    break;
                case 0x0e: //ldarg.s
                case 0xfe09: //ldarg
                    _useConstValue = default(bool);
                    break;
                default:
                    throw global::XenoGears.Assertions.AssertionHelper.Fail();
            }

            // initializing _value
            switch((ushort)OpSpec.OpCode.Value)
            {
                case 0x02: //ldarg.0
                case 0x03: //ldarg.1
                case 0x04: //ldarg.2
                case 0x05: //ldarg.3
                case 0xfe09: //ldarg
                    _value = _useConstValue ? _constValue.Value : ReadI4(reader);
                    break;
                case 0x0e: //ldarg.s
                    _value = _useConstValue ? _constValue.Value : ReadI1(reader);
                    break;
                default:
                    throw global::XenoGears.Assertions.AssertionHelper.Fail();
            }

            // verify that we've read exactly the amount of bytes we should
            var bytesRead = reader.BaseStream.Position - origPos;
            global::XenoGears.Assertions.AssertionHelper.AssertTrue(bytesRead == SizeOfOperand);

            // now when the initialization is completed verify that we've got only prefixes we support
            global::XenoGears.Assertions.AssertionHelper.AssertAll(Prefixes, prefix => 
            {
                return false;
            });
        }

        private static global::System.Reflection.Emit.OpCode AssertSupportedOpCode(global::System.IO.BinaryReader reader)
        {
            var opcode = global::Truesight.Parser.Impl.Reader.OpCodeReader.ReadOpCode(reader);
            global::XenoGears.Assertions.AssertionHelper.AssertNotNull(opcode);
            // ldarg.0, ldarg.1, ldarg.2, ldarg.3, ldarg.s, ldarg
            global::XenoGears.Assertions.AssertionHelper.AssertTrue(global::System.Linq.Enumerable.Contains(new ushort[]{0x02, 0x03, 0x04, 0x05, 0x0e, 0xfe09}, (ushort)opcode.Value.Value));

            return opcode.Value;
        }


        public global::System.Reflection.ParameterInfo Arg
        {
            get
            {
                if (Source.Method == null || Source.Args == null)
                {
                    return null;
                }
                else
                {
                    if (Source.Method.IsStatic)
                    {
                        return Source.Args[_value];
                    }
                    else
                    {
                        return _value == 0 ? null : Source.Args[_value - 1];
                    }
                };
            }
        }

        public int Index
        {
            get
            {
                return _value;
            }
        }

        public override global::System.String ToString()
        {
            var offset = OffsetToString(Offset) + ":";
            var prefixSpec = Prefixes.Count == 0 ? "" : ("[" + global::XenoGears.Functional.EnumerableExtensions.StringJoin(Prefixes) + "]");
            var name =  "ldarg";
            var mods = new global::System.Collections.Generic.List<global::System.String>();
            var modSpec = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(mods, mod => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(mod)), ", ");
            var operand = Arg != null ? ParameterInfoToString(Arg) : Index.ToString();

            var parts = new []{offset, prefixSpec, name, modSpec, operand};
            var result = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(parts, p => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(p)), " ");

            return result;
        }
    }
}