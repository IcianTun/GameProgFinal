using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public static SceneName currentScene;
    public SceneName thisScene;

    private void Start()
    {
        currentScene = thisScene;
    }
}

public enum SceneName
{
    MezScene,
    MawinScene,
    TunScene,
    ZezeScene,
    FightScene
}
