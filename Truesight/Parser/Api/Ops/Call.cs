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
    // call, calli, callvirt
    [global::Truesight.Parser.Impl.Ops.OpCodesAttribute(0x28, 0x29, 0x6f)]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public sealed class Call : global::Truesight.Parser.Impl.ILOp
    {
        public override global::Truesight.Parser.Api.IILOpType OpType { get { return global::Truesight.Parser.Api.IILOpType.Call; } }

        private readonly int _methodToken;
        private readonly int _signatureToken;

        internal Call(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader)
            : this(source, reader, global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
        }

        internal Call(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader, global::System.Collections.ObjectModel.ReadOnlyCollection<global::Truesight.Parser.Impl.ILOp> prefixes)
            : base(source, AssertSupportedOpCode(reader), (int)reader.BaseStream.Position - global::System.Linq.Enumerable.Sum(global::System.Linq.Enumerable.Select(prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()), prefix => prefix.Size)), prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
            // this is necessary for further verification
            var origPos = reader.BaseStream.Position;

            // initializing _methodToken
            switch((ushort)OpSpec.OpCode.Value)
            {
                case 0x28: //call
                case 0x6f: //callvirt
                    _methodToken = ReadMetadataToken(reader);
                    break;
                case 0x29: //calli
                    _methodToken = default(int);
                    break;
                default:
                    throw global::XenoGears.Assertions.AssertionHelper.Fail();
            }

            // initializing _signatureToken
            switch((ushort)OpSpec.OpCode.Value)
            {
                case 0x28: //call
                case 0x6f: //callvirt
                    _signatureToken = default(int);
                    break;
                case 0x29: //calli
                    _signatureToken = ReadMetadataToken(reader);
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
                var constrained_ok = prefix is Constrained;
                var tail_ok = prefix is Tail;
                return constrained_ok || tail_ok || false;
            });
        }

        private static global::System.Reflection.Emit.OpCode AssertSupportedOpCode(global::System.IO.BinaryReader reader)
        {
            var opcode = global::Truesight.Parser.Impl.Reader.OpCodeReader.ReadOpCode(reader);
            global::XenoGears.Assertions.AssertionHelper.AssertNotNull(opcode);
            // call, calli, callvirt
            global::XenoGears.Assertions.AssertionHelper.AssertTrue(global::System.Linq.Enumerable.Contains(new ushort[]{0x28, 0x29, 0x6f}, (ushort)opcode.Value.Value));

            return opcode.Value;
        }


        public bool IsVirtual
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x28: //call
                    case 0x29: //calli
                        return default(bool);
                    case 0x6f: //callvirt
                        return true;
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public global::System.Reflection.MethodBase Method
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x28: //call
                    case 0x6f: //callvirt
                        return MethodBaseFromToken(_methodToken);
                    case 0x29: //calli
                        return MethodBaseFromSignature(SignatureFromToken(_signatureToken));
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public int MethodToken
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x28: //call
                    case 0x6f: //callvirt
                        return _methodToken;
                    case 0x29: //calli
                        return default(int);
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public global::System.Byte[] Signature
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x28: //call
                    case 0x6f: //callvirt
                        return default(global::System.Byte[]);
                    case 0x29: //calli
                        return SignatureFromToken(_signatureToken);
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public int SignatureToken
        {
            get
            {
                switch((ushort)OpSpec.OpCode.Value)
                {
                    case 0x28: //call
                    case 0x6f: //callvirt
                        return default(int);
                    case 0x29: //calli
                        return _signatureToken;
                    default:
                        throw global::XenoGears.Assertions.AssertionHelper.Fail();
                }
            }
        }

        public global::System.Type Constraint
        {
            get
            {
                return global::System.Linq.Enumerable.Single(global::System.Linq.Enumerable.OfType<Constrained>(Prefixes)).Type;
            }
        }

        public bool IsTail
        {
            get
            {
                return global::System.Linq.Enumerable.Any(global::System.Linq.Enumerable.OfType<Tail>(Prefixes));
            }
        }

        public override global::System.String ToString()
        {
            var offset = OffsetToString(Offset) + ":";
            var prefixSpec = Prefixes.Count == 0 ? "" : ("[" + global::XenoGears.Functional.EnumerableExtensions.StringJoin(Prefixes) + "]");
            var name =  "call";
            var mods = new global::System.Collections.Generic.List<global::System.String>();
            mods.Add(IsVirtual ? "virt" : "");
            var modSpec = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(mods, mod => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(mod)), ", ");
            var operand = ((Method != null ? MethodBaseToString(Method) : null) ?? ((OpSpec.OpCode.Value == 0x29 /*calli*/ ? (Signature != null ? ByteArrayToString(Signature) : ("0x" + _signatureToken.ToString("x8"))) : ("0x" + _methodToken.ToString("x8")))));

            var parts = new []{offset, prefixSpec, name, modSpec, operand};
            var result = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(parts, p => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(p)), " ");

            return result;
        }
    }
}