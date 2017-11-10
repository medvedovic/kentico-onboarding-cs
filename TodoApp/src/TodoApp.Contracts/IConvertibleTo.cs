namespace TodoApp.Contracts
{
    public interface IConvertibleTo<out TType> where TType: class
    {
        TType Convert();
    }
}
