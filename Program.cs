using TEXT_RPG.UI;

namespace TEXT_RPG
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);
            UIHelper.SetInitDesign();

            // run
            while (true)
            {
                Console.Clear();
                UIHelper.WriteHeader();
                GameManager.Instance.ResetHeaderText();
                GameManager.Instance.CurrentScene.Show();
            }
        }
    }
}
