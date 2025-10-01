namespace TEXT_RPG.Scenes
{
    internal class StatusScene : Scene
    {
        public override void Show()
        {
            int select;
            UI.PlayerUI.ShowPlayerInfo(Manager.Player);
            UI.UIHelper.WriteOptions(SceneType.Status, Manager.Scenes[SceneType.Status]);
            select = Manager.SelectAct(SceneType.Status);
            if (select == 0)
            {
                Console.Clear();
                return;
            }
        }
    }
}
