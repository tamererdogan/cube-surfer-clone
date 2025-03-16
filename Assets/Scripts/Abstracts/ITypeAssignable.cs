namespace Abstracts
{
    public interface ITypeAssignable<out T>
    {
        T GetObjectType();
    }
}