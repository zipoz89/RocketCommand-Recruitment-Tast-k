public interface IPoolableObject
{
    public void OnGenerated();
    public void OnSpawned();
    public void OnReturned();
}