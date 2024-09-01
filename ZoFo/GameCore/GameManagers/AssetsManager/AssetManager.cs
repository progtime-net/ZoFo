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
        Animations = [ "player_look_down", "player_run_up", "player_run_down", "player_run_right",
        "player_run_left", "player_run_right_up", "player_run_left_up", "player_run_right_down",
        "player_run_left_down","player_running","player_idle" ],
        IdleAnimation = "player_idle"
    };
}