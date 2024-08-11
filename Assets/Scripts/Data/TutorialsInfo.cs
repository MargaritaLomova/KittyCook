using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialsInfo", menuName = "Kitty Cook/Create tutorials info")]
public class TutorialsInfo : ScriptableObject
{
    public List<TutorialInfo> Tutorials = new List<TutorialInfo>();

    private static TutorialsInfo mInstance;
    public static TutorialsInfo Get
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = Resources.Load("Configs/TutorialsInfo") as TutorialsInfo;
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
public class TutorialInfo
{
    public int LevelToShowIndex;
    public string SpeakerName;
    public string[] Phrases;
    public AnimatorController SpeakerAnimatorController;
    public Sprite StartSpeakerIcon;
}