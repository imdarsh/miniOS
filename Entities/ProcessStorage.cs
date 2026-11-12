using System.Diagnostics.Contracts;

class ProcessStorage<T>
{
    private T _data;
    public void Save(T data)
    {
        _data = data;
    }
    public T Get()
    {
        return _data;
    }
}