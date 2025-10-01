namespace TEXT_RPG.Scenes
{
    internal abstract class Scene
    {
        protected GameManager Manager => GameManager.Instance;

        public abstract void Show();
    }
}
