using Caliburn.Micro;
using Orbit.Services;
using Orbit.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Orbit
{
    public class OrbitBootstrapper: BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public OrbitBootstrapper() : base(true)
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            DisplayRootViewFor<OrbitViewModel>();
        }

        protected override void Configure()
        {
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            _container.Singleton<IPumpService, XmlPumpService>();

            _container.RegisterPerRequest(typeof(OrbitViewModel), null, typeof(OrbitViewModel));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
