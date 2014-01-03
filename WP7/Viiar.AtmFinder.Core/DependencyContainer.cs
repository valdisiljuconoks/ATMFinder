using System;
using Ninject;

namespace Viiar.AtmFinder.Core
{
    public static class DependencyContainer
    {
        private static readonly IKernel kernel = new StandardKernel();

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }

        public static void Initialize(Action<IKernel> init)
        {
            InvokeActionOnKernel(init);
        }

        public static void Uninitialize(Action<IKernel> init)
        {
            InvokeActionOnKernel(init);
        }

        private static void InvokeActionOnKernel(Action<IKernel> init)
        {
            if (init == null)
            {
                throw new ArgumentNullException("init");
            }

            init.Invoke(kernel);
        }
    }
}
