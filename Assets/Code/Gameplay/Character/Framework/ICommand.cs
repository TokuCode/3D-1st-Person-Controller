namespace Code.Gameplay.Character.Framework
{
    public interface ICommand<T> where T : unmanaged
    {
        void Execute(T payload);
    }
    
    public interface IRequest<T> where T : unmanaged
    {
        T Get();
    }
}