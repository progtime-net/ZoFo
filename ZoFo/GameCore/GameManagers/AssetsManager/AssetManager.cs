using System.Collections.Generic;

namespace ZoFo.GameCore.GameManagers.AssetsManager;

public class AssetManager
{
    public AssetContainer Zombie = new()
    {
        Animations = ["zombie_damaged", "zombie_walk", "zombie_idle", "zombie_attack", "zombie_death"],
        IdleAnimation = "zombie_walk"
    };

    public AssetContainer Player = new()
    {
        Animations = [ "player_look_down", "player_run_up"],
        IdleAnimation = "player_look_down"
    };
}