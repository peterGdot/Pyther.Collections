namespace Pyther.Collections.Generic
{
    [Flags]
    public enum GraphFlags : byte
    {
        None = 0,
        Weighted = 1,           // The graph allow edge weights
        AllowSelfLoops = 2,     // The nodes allow self loops
        AllowDoubleNodes = 4    // Allow adding nodes with the same value more than once
    }
}
