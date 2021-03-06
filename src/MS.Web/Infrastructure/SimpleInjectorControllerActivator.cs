﻿using System;
using Microsoft.AspNet.Mvc;
using SimpleInjector;

namespace MS.Web.Infrastructure
{
    internal sealed class SimpleInjectorControllerActivator : IControllerActivator
    {
        private readonly Container _container;

        public SimpleInjectorControllerActivator(Container container)
        {
            _container = container;
        }

        public object Create(ActionContext context, Type controllerType)
        {
            return _container.GetInstance(controllerType);
        }
    }
}