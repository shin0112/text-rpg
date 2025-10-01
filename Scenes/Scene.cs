namespace TEXT_RPG.Scenes
{
    internal abstract class Scene
    {
        protected GameManager Manager => GameManager.Instance;
        protected abstract string Title { get; }
        public abstract string[] Options { get; }

        public abstract void Show();
        protected abstract void HandleInput(int select);
    }
}
