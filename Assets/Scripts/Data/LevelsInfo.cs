using KittyCook.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsInfo", menuName = "Kitty Cook/Create levels info")]
public class LevelsInfo : ScriptableObject
{
    public List<LevelInfo> Levels = new List<LevelInfo>();

    private static LevelsInfo mInstance;
    public static LevelsInfo Get
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = Resources.Load("Configs/LevelsInfo") as LevelsInfo;
            }

            return mInstance;
        }
        set
        {
            mInstance = value;
        }
    }
}

[Serializable]
public class LevelInfo
{
    public int Index;
    public int TimeOfDayInSeconds;
    public GuestController[] AvailableGuests;
    public RecipeInfo[] AvailableRecipes;
    public int MaxGuestsCount;
}