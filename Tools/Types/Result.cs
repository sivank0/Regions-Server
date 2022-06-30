namespace Tools.Types;
public class Result
{
    public Error[] Errors { get; }
    public bool IsSucces => Errors.Length == 0;
    public Result(Error[] errors)
    {
        Errors = errors;
    }
    public static Result Succes()
    {
        return new Result(new Error[] { });
    }

    public static Result Failed(string value) => new Result(new Error[] { new Error(null, value) });

    public static Result Failed(Error[] errors) => new Result(errors);
}
