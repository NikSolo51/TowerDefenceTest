namespace CodeBase.Services.Update
{
    public interface ILateUpdatable : ICacheUpdate
    {
        void LateUpdateTick();
    }
}