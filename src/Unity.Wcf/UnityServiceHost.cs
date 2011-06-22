
namespace Unity.Wcf
{
    using System;
    using System.ServiceModel;
    using Microsoft.Practices.Unity;

    public class UnityServiceHost : ServiceHost
    {
        private readonly IUnityContainer _container;

        public UnityServiceHost(IUnityContainer container)
        {
            _container = container;
        }

        public UnityServiceHost(IUnityContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _container = container;
        }

        protected override void OnOpening()
        {
            Description.Behaviors.Add(new UnityInstanceProviderServiceBehavior(_container));
            base.OnOpening();
        }

    }
}