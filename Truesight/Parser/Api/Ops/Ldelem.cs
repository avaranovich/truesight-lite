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
    // ldelem.i1, ldelem.u1, ldelem.i2, ldelem.u2, ldelem.i4, ldelem.u4, ldelem.i8, ldelem.i, ldelem.r4, ldelem.r8, ldelem.ref, ldelem
    [global::Truesight.Parser.Impl.Ops.OpCodesAttribute(0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9a, 0xa3)]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public sealed class Ldelem : global::Truesight.Parser.Impl.ILOp
    {
        public override global::Truesight.Parser.Api.IILOpType OpType { get { return global::Truesight.Parser.Api.IILOpType.Ldelem; } }

        private readonly int _typeToken;

        internal Ldelem(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader)
            : this(source, reader, global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
        }

        internal Ldelem(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader, global::System.Collections.ObjectModel.ReadOnlyCollection<global::Truesight.Parser.Impl.ILOp> prefixes)
            : base(source, AssertSupportedOpCode(reader), (int)reader.BaseStream.Position - global::System.Linq.Enumerable.Sum(global::System.Linq.Enumerable.Select(prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()), prefix => prefix.Size)), prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
            // this is necessary for further verification
            var origPos = reader.BaseStream.Position;

            // initializing _typeToken
            switch((ushort)OpSpec.OpCode.Value)
            {
                case 0x90: //ldelem.i1
                case 0x91: //ldelem.u1
                case 0x92: //ldelem.i2
                case 0x93: //ldelem.u2
                case 0x94: //ldelem.i4
                case 0x95: //ldelem.u4
                case 0x96: //ldelem.i8
                case 0x97: //ldelem.i
                case 0x98: //ldelem.r4
                case 0x99: //ldelem.r8
                case 0x9a: //ldelem.ref
                    _typeToken = default(int);
                    break;
                case 0xa3: //ldelem
                    _typeToken = ReadMetadataToken(reader);
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
            // ldelem.i1, ldelem.u1, ldelem.i2, ldelem.u2, ldelem.i4, ldelem.u4, ldelem.i8, ldelem.i, ldelem.r4, ldelem.r8, ldelem.ref, ldelem
            global::XenoGears.Assertions.AssertionHelper.AssertTrue(global::System.Linq.Enumerable.Contains(new ushort[]{0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9a, 0xa3}, (ushort)opcode.Value.Value));

            return opcode.Value;
        }


        public global::System.Type Type
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x90: //ldelem.i1
                        return typeof(sbyte);
                    case 0x91: //ldelem.u1
                        return typeof(byte);
                    case 0x92: //ldelem.i2
                        return typeof(short);
                    case 0x93: //ldelem.u2
                        return typeof(ushort);
                    case 0x94: //ldelem.i4
                        return typeof(int);
                    case 0x95: //ldelem.u4
                        return typeof(uint);
                    case 0x96: //ldelem.i8
                        return typeof(long);
                    case 0x97: //ldelem.i
                        return typeof(global::System.IntPtr);
                    case 0x98: //ldelem.r4
                        return typeof(float);
                    case 0x99: //ldelem.r8
                        return typeof(double);
                    case 0x9a: //ldelem.ref
                        return typeof(global::System.Object);
                    case 0xa3: //ldelem
                        return TypeFromToken(_typeToken);
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public int TypeToken
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x90: //ldelem.i1
                    case 0x91: //ldelem.u1
                    case 0x92: //ldelem.i2
                    case 0x93: //ldelem.u2
                    case 0x94: //ldelem.i4
                    case 0x95: //ldelem.u4
                    case 0x96: //ldelem.i8
                    case 0x97: //ldelem.i
                    case 0x98: //ldelem.r4
                    case 0x99: //ldelem.r8
                    case 0x9a: //ldelem.ref
                        return default(int);
                    case 0xa3: //ldelem
                        return _typeToken;
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public override global::System.String ToString()
        {
            var offset = OffsetToString(Offset) + ":";
            var prefixSpec = Prefixes.Count == 0 ? "" : ("[" + global::XenoGears.Functional.EnumerableExtensions.StringJoin(Prefixes) + "]");
            var name =  "ldelem";
            var mods = new global::System.Collections.Generic.List<global::System.String>();
            var modSpec = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(mods, mod => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(mod)), ", ");
            var operand = ((Type != null ? TypeToString(Type) : null) ?? (("0x" + TypeToken.ToString("x8"))));

            var parts = new []{offset, prefixSpec, name, modSpec, operand};
            var result = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(parts, p => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(p)), " ");

            return result;
        }
    }
}