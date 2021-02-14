namespace OneGate.Backend.Core.Base.Database.Repository
{
    public readonly struct QueryLimits
    {
        public int Shift { get; }
        public int Count { get; }

        public QueryLimits(int? shift, int? count)
        {
            Shift = shift ?? 0;
            Count = count ?? 1;
        }
    }
}