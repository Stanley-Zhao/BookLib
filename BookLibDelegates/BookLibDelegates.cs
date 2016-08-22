using BookLib.Interface;
namespace BookLib.Delegates
{
    #region Delegates
    public delegate void NoneParaDelegateMethod();
    public delegate int DoBusinessTaskDelegateMethod<T>(IReturnValue<T> resultValue);
    #endregion
}
