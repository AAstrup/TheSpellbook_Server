public interface IUpdatableDeltaTime
{
    void Update(float deltaTime);
    bool HasExpired();
    void End();
}