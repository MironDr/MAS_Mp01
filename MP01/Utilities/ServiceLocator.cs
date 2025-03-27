namespace MP01.Utilities;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<T>(T instance) where T : class
    {
        _services[typeof(T)] = instance;
    }

    public static T Get<T>() where T : class
    {
        return _services[typeof(T)] as T ?? throw new Exception($"Service {typeof(T).Name} not found!");
    }
}