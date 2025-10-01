namespace TEXT_RPG.Scenes
{
    internal class StartScene : Scene
    {
        protected override string Title => "";
        public override string[] Options => ["", "상태 보기", "인벤토리", "랜덤 모험", "마을 순찰하기", "훈련하기", "상점"];

        protected override void HandleInput(int select)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            int select;
            UI.PlayerUI.ShowPlayerInfo();
            UI.UIHelper.WriteOptions();
            select = Manager.SelectAct(SceneType.Status);
            if (select == 0)
            {
                Console.Clear();
                return;
            }
        }
    }
}
