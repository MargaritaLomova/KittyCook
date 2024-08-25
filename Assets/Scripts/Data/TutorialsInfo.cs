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
    public Phrase[] Phrases;
    public AnimatorController SpeakerAnimatorController;
    public Sprite StartSpeakerIcon;
}

[Serializable]
public class Phrase
{
    public string PhraseText;
    [SerializeField]
    private string HighlightedObjectName;
    public bool HasHighlight => !string.IsNullOrEmpty(HighlightedObjectName);

    public Vector2 HighlightPosition
    {
        get
        {
            if (!string.IsNullOrEmpty(HighlightedObjectName))
            {
                var refObject = GameObject.Find(HighlightedObjectName);
                return refObject.transform.position;
            }
            else
            {
                return Vector2.zero;
            }
        }
        private set
        {

        }
    }
}