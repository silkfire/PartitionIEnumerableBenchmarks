namespace PartitionIEnumerableBenchmarks
{
    using BenchmarkDotNet.Running;

    public static class App
    {

        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PartitionIEnumerableBenchmarks>(new PowerPlanModeConfig());
        }
    }
}
