using TEXT_RPG.Manager;
using TEXT_RPG.UI;

namespace TEXT_RPG
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);
            UIHelper.SetInitDesign();

            GameManager.Instance.Run();
        }
    }
}
