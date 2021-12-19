namespace Util
{
    public interface IShallowCloneable<out T>
    {
        // ReSharper disable once UnusedMemberInSuper.Global
        T ShallowClone();
    }
}
