namespace PhantomEngine
{
    public interface ICallbackBase
    {
        void OnEnter();
        
        void OnUpdate();
        
        void OnExit();
    }
}