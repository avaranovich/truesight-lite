using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Truesight.Decompiler.Hir;
using Truesight.Decompiler.Hir.Core.ControlFlow;
using Truesight.Decompiler.Pipeline.Flow.Cfg;
using XenoGears.Functional;
using XenoGears.Assertions;

namespace Truesight.Decompiler.Pipeline.Flow.Scopes
{
    internal class ComplexScope : IScope<Block>
    {
        private readonly IScope _parent;
        public IScope Parent { get { return _parent; } }

        private readonly Block _block;
        Node IScope.Hir { get { return Hir; } }
        public Block Hir { get { return _block; } }

        private readonly BaseControlFlowGraph _localCfg;
        public BaseControlFlowGraph LocalCfg { get { return _localCfg; } }
        public ReadOnlyCollection<ControlFlowBlock> Pivots { get { return Seq.Empty<ControlFlowBlock>().ToReadOnly(); } }
        public ReadOnlyCollection<Offspring> Offsprings { get; private set; }

        public static ComplexScope Decompile(IScope parent, BaseControlFlowGraph cfg) { return new ComplexScope(parent, cfg); }
        private ComplexScope(IScope parent, BaseControlFlowGraph cfg)
        {
            _parent = parent.AssertNotNull();
            _localCfg = cfg;
            var offsprings = new List<Offspring>();

            var adjacentToFinish = cfg.Vedges(null, cfg.Finish).Select(e => e.Source).ToReadOnly();
            var lastInFlow = cfg.Cflow()[adjacentToFinish.Max(v => cfg.Cflow().IndexOf(v))];
            if (lastInFlow != cfg.Start)
            {
                var flow = lastInFlow.MkArray().Closure(cfg.Vertices,
                    (vin, vout) => vout != cfg.Start && cfg.Vedge(vout, vin) != null);
                _localCfg = cfg.CreateView(flow, (e, vcfg) =>
                {
                    if (e.Source == cfg.Start)
                    {
                        vcfg.InheritStart(cfg.Start);
                        vcfg.AddEigenEdge(e.ShallowClone());
                    }
                    else if (e.Target == cfg.Finish)
                    {
                        if (e.Source == lastInFlow) vcfg.InheritFinish(cfg.Finish);
                        vcfg.AddEigenEdge(e.ShallowClone());
                    }
                    else
                    {
                        (cfg.Vedges(e.Source, null).Count() == 2).AssertTrue();
                        offsprings.Add(new Offspring(this, e.Source, e.Target));
                    }
                });
            }

            Offsprings = offsprings.ToReadOnly();
            _block = BlockScope.Decompile(this, _localCfg).Hir;
        }

        public String Uri
        {
            get { return Parent.Uri + " :: " + "complex"; }
        }

        public override String ToString()
        {
            var lite = Uri.Replace("complex :: ", "");
            lite = lite.Replace("body :: ", "");
            return "{" + lite + "}";
        }
    }
}