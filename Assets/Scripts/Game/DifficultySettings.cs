using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : Singleton<DifficultySettings>
{
    private const int ULTIMATE = (int) Consts.Difficulties.ULTIMATE;
    private const int HARD = (int) Consts.Difficulties.HARD;
    private const int NORMAL = (int) Consts.Difficulties.NORMAL;

    public float GetBulletSpeed()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 10.5f,
            HARD => 8.5f,
            NORMAL => 6.5f,
            _ => 6.5f,
        };
    }
}
