namespace TEXT_RPG.Scenes
{
    internal class StatusScene : Scene
    {
        protected override string Title => "상태 보기";
        public override string[] Options => ["나가기"];

        protected override void HandleInput(int select)
        {
            if (select == 0) { Manager.ChangeScene(SceneType.Start); }
        }

        public override void Show()
        {
            int select;
            UI.PlayerUI.ShowPlayerInfo();
            UI.UIHelper.WriteOptions();
            select = Manager.SelectAct();

            HandleInput(select);
        }
    }
}
