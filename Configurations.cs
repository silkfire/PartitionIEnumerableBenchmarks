namespace PartitionIEnumerableBenchmarks
{
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Environments;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Loggers;
    using BenchmarkDotNet.Reports;

    public class PowerPlanModeConfig : ManualConfig
    {
        public PowerPlanModeConfig()
        {
            Options = ConfigOptions.DisableLogFile;

            AddColumnProvider(DefaultColumnProviders.Instance);
            AddLogger(ConsoleLogger.Default);
            AddJob(Job.Default.WithPowerPlan(PowerPlan.UserPowerPlan));

            WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Percentage));
        }
    }
}
