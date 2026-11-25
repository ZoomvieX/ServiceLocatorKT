using UnityEngine;
using System;
using System.Collections.Generic;

public class ServiceLocator : IService
{
    private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public void Register<T>(T service) where T : class
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services[type] = service;
            return;
        }

        _services.Add(type, service);
    }

    public T GetService<T>() where T : class
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        throw new InvalidOperationException($"Service of type {type.Name} is not registered in ServiceLocator.");
    }
}