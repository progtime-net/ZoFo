namespace ZoFo.GameCore.GameManagers.AssetsManager;

public static class AssetManager
{
    public static AssetContainer Zombie = new()
    {
        Animations = { "zombie_damaged", "zombie_walk", "zombie_idle", "zombie_attack", "zombie_death" },
        IdleAnimation = "zombie_walk"
    };

    public static AssetContainer Player = new()
    {
        Animations = { "player_look_down" },
        IdleAnimation = "player_look_down"
    };
}