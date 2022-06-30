namespace Tools.Types;
public class Page<TValue>
{
    public TValue[] Values { get; }
    public Int32 TotalRows { get; }
    public Page(TValue[] values, Int32 totalRows)
    {
        Values = values;
        TotalRows = totalRows;
    }
}
