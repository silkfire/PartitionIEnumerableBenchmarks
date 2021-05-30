namespace PartitionIEnumerableBenchmarks
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using MoreLinq;

    using System.Collections.Generic;
    using System.Linq;

    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class PartitionIEnumerableBenchmarks
    {
        [Params(100, 200, 500, 1_000)]
        //[Params(100)]
        public int PartitionSize { get; set; }

        private readonly IEnumerable<int> IEnumerable = Enumerable.Range(0, 10_000).ToList();
        private static readonly PartitionIEnumerable Partitioner = new();

        [Benchmark]
        public void PartitionClassic()
        {
            var partitionedData = Partitioner.PartitionClassic(IEnumerable, PartitionSize);
        }

        [Benchmark]
        public void PartitionJodrell()
        {
            var partitionedData = Partitioner.PartitionJodrell(IEnumerable, PartitionSize);
        }

        [Benchmark]
        public void PartitionNeo2()
        {
            var partitionedData = Partitioner.PartitionNeo2(IEnumerable, PartitionSize);
        }

        [Benchmark(Baseline = true)]
        public void PartitionLinq()
        {
            var partitionedData = Partitioner.PartitionLinq(IEnumerable, PartitionSize);
        }

        [Benchmark]
        public void ChunkDotNet6()
        {
            var partitionedData = IEnumerable.Chunk(PartitionSize);
        }

        [Benchmark]
        public void MoreLinqBatch()
        {
            var partitionedData = IEnumerable.Batch(PartitionSize);
        }




        //[Benchmark]
        //public void PartitionClassicToList()
        //{
        //    var partitionedData = Partitioner.PartitionClassic(IEnumerable, PartitionSize).Select(ii => ii.ToList()).ToList();
        //}

        //[Benchmark]
        //public void PartitionJodrellToList()
        //{
        //    var partitionedData = Partitioner.PartitionJodrell(IEnumerable, PartitionSize).Select(ii => ii.ToList()).ToList();
        //}

        //[Benchmark]
        //public void PartitionNeo2ToList()
        //{
        //    var partitionedData = Partitioner.PartitionNeo2(IEnumerable, PartitionSize).Select(ii => ii.ToList()).ToList();
        //}

        //[Benchmark(Baseline = true)]
        //public void PartitionLinqToList()
        //{
        //    var partitionedData = Partitioner.PartitionLinq(IEnumerable, PartitionSize).Select(ii => ii.ToList()).ToList();
        //}

        //[Benchmark]
        //public void ChunkDotNet6ToList()
        //{
        //    var partitionedData = IEnumerable.Chunk(PartitionSize).Select(ii => ii.ToList()).ToList();
        //}

        //[Benchmark]
        //public void MoreLinqBatchToList()
        //{
        //    var partitionedData = IEnumerable.Batch(PartitionSize).Select(ii => ii.ToList()).ToList();
        //}
    }
}
