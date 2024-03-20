using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();    //stopwatch: kronometre demektir.
        }


        protected override void OnBefore(IInvocation invocation)   //metotun önünde kronmetreyi başlatıyorum
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)        //bitene kadar geçen süreyi hesaplıyorum
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");     //geçen süre verdiğimiz üreden yüksekse consola yaz.
            }
            _stopwatch.Reset();
        }
    }
}
