namespace Util
{
    public interface IDeepCloneable<out T>
    {
        T DeepClone();
    }
}
