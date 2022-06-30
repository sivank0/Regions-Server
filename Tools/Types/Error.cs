namespace Tools.Types;
public class Error
{
    public string? Key { get; }
    public string Value { get; }
    public Error(string? key, string value)
    {
        Key = key;
        Value = value;
    }
}
