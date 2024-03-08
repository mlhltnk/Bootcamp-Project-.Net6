using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //INTERCEPTOR: ARAYA GİREN DEMEKTİR.
        //invocation(business metotları(add,update,delete vs)); iş tarafındaki metodumuz(Bizim şuan için örneğimizde add metodu için işlem yaptık)
        protected virtual void OnBefore(IInvocation invocation) { }    //metodun öncesinde çalış
        protected virtual void OnAfter(IInvocation invocation) { }    //metodun sonrasında çalış (ŞUAN ÇALIŞMIYOR)
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }  //(ŞUAN ÇALIŞMIYOR)
        protected virtual void OnSuccess(IInvocation invocation) { }   //(ŞUAN ÇALIŞMIYOR)
        public override void Intercept(IInvocation invocation)  //(ŞUAN ÇALIŞMIYOR)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
