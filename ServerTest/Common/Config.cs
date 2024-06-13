namespace Server3
{

    [Flags]
    public enum AppType
    {
        // Global 和 None 值相同， Global 只用于读取配置
        None = 0,
        Global = 0,
        DbMgr = 1,
        GameMgr = 1 << 1,
        SpaceMgr = 1 << 2,

        Login = 1 << 3,
        Game = 1 << 4,
        Space = 1 << 5,
        Robot = 1 << 6,

        Mgr = GameMgr | SpaceMgr,
        All = DbMgr | GameMgr | SpaceMgr | Login | Game | Space,
    }

}