namespace CodeBase.Services.Update
{
    public interface IUpdatable : ICacheUpdate
    {
        void UpdateTick();
    }
}