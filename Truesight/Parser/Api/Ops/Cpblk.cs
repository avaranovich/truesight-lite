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
    // cpblk
    [global::Truesight.Parser.Impl.Ops.OpCodesAttribute(0xfe17)]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public sealed class Cpblk : global::Truesight.Parser.Impl.ILOp
    {
        public override global::Truesight.Parser.Api.IILOpType OpType { get { return global::Truesight.Parser.Api.IILOpType.Cpblk; } }

        internal Cpblk(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader)
            : this(source, reader, global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
        }

        internal Cpblk(global::Truesight.Parser.Impl.MethodBody source, global::System.IO.BinaryReader reader, global::System.Collections.ObjectModel.ReadOnlyCollection<global::Truesight.Parser.Impl.ILOp> prefixes)
            : base(source, AssertSupportedOpCode(reader), (int)reader.BaseStream.Position - global::System.Linq.Enumerable.Sum(global::System.Linq.Enumerable.Select(prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()), prefix => prefix.Size)), prefixes ?? global::XenoGears.Functional.EnumerableExtensions.ToReadOnly(global::System.Linq.Enumerable.Empty<global::Truesight.Parser.Impl.ILOp>()))
        {
            // this is necessary for further verification
            var origPos = reader.BaseStream.Position;

            // verify that we've read exactly the amount of bytes we should
            var bytesRead = reader.BaseStream.Position - origPos;
            global::XenoGears.Assertions.AssertionHelper.AssertTrue(bytesRead == SizeOfOperand);

            // now when the initialization is completed verify that we've got only prefixes we support
            global::XenoGears.Assertions.AssertionHelper.AssertAll(Prefixes, prefix => 
            {
                var unaligned_ok = prefix is Unaligned;
                var volatile_ok = prefix is Volatile;
                return unaligned_ok || volatile_ok || false;
            });
        }

        private static global::System.Reflection.Emit.OpCode AssertSupportedOpCode(global::System.IO.BinaryReader reader)
        {
            var opcode = global::Truesight.Parser.Impl.Reader.OpCodeReader.ReadOpCode(reader);
            global::XenoGears.Assertions.AssertionHelper.AssertNotNull(opcode);
            // cpblk
            global::XenoGears.Assertions.AssertionHelper.AssertTrue(global::System.Linq.Enumerable.Contains(new ushort[]{0xfe17}, (ushort)opcode.Value.Value));

            return opcode.Value;
        }


        public byte Alignment
        {
            get
            {
                var unaligned = global::System.Linq.Enumerable.SingleOrDefault(global::System.Linq.Enumerable.OfType<Unaligned>(Prefixes));
                var defaultAlignment = (byte)global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr));
                return unaligned != null ? unaligned.Alignment : defaultAlignment;
            }
        }

        public bool IsAligned
        {
            get
            {
                return !IsUnaligned;
            }
        }

        public bool IsUnaligned
        {
            get
            {
                return global::System.Linq.Enumerable.Any(global::System.Linq.Enumerable.OfType<Unaligned>(Prefixes));
            }
        }

        public bool IsVolatile
        {
            get
            {
                return global::System.Linq.Enumerable.Any(global::System.Linq.Enumerable.OfType<Volatile>(Prefixes));
            }
        }

        public override global::System.String ToString()
        {
            var offset = OffsetToString(Offset) + ":";
            var prefixSpec = Prefixes.Count == 0 ? "" : ("[" + global::XenoGears.Functional.EnumerableExtensions.StringJoin(Prefixes) + "]");
            var name =  "cpblk";
            var mods = new global::System.Collections.Generic.List<global::System.String>();
            var modSpec = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(mods, mod => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(mod)), ", ");
            var operand = "";

            var parts = new []{offset, prefixSpec, name, modSpec, operand};
            var result = global::XenoGears.Functional.EnumerableExtensions.StringJoin(global::System.Linq.Enumerable.Where(parts, p => global::XenoGears.Functional.EnumerableExtensions.IsNeitherNullNorEmpty(p)), " ");

            return result;
        }
    }
}