namespace CodeBase.Services.Update
{
    public interface IFixedUpdatable : ICacheUpdate
    {
        void FixedUpdateTick();
    }
}