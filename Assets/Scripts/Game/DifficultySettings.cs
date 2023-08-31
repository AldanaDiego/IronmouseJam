using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DifficultySettings : Singleton<DifficultySettings>
{
    private const int ULTIMATE = (int) Consts.Difficulties.ULTIMATE;
    private const int HARD = (int) Consts.Difficulties.HARD;
    //private const int NORMAL = (int) Consts.Difficulties.NORMAL;

    public float GetBulletSpeed()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 10.5f,
            HARD => 8.5f,
            _ => 7f,
        };
    }

    public float GetEnemyAttackSpeed()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 2f,
            HARD => 3.5f,
            _ => 4f
        };
    }

    public float GetRockObstacleSpeed()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 13f,
            HARD => 10f,
            _ => 8.5f
        };
    }

    public float GetObstacleSpawnCooldown()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 2.5f,
            HARD => 2.75f,
            _ => 3.25f
        };
    }

    public float GetStageTotalTime()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 120f,
            HARD => 90f,
            _ => 75f
        };
    }

    public float GetEnemySpawnTime()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 3f,
            HARD => 15f,
            _ => 25f
        };
    }

    public float GetMultipleObstacleChance()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => 0.45f,
            HARD => 0.2f,
            _ => 0f
        };
    }

    public string GetDifficultyString()
    {
        return PlayerPrefs.GetInt("Difficulty") switch
        {
            ULTIMATE => LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "TITLE_DIFFICULTY_ULTIMATE"),
            HARD => LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "TITLE_DIFFICULTY_HARD"),
            _ => LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringDB", "TITLE_DIFFICULTY_NORMAL")
        };
    }
}
