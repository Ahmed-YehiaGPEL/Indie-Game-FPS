/**
 * Script:  DataLoader.cs
 * Date:    August 24
 * Author:  Ahmed Yehia
 * Description:
 *  Loads the game settings data 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLoader : MonoBehaviour {

    public float audioLevel;
    public int qualityLevel;
    public AudioSpeakerMode speakerMode;
    int speakerModeInt;
    List<Pair<int, AudioSpeakerMode>> modeIntList = new List<Pair<int, AudioSpeakerMode>>
    {
        new Pair<int, AudioSpeakerMode>(0, AudioSpeakerMode.Mode5point1),
        new Pair<int, AudioSpeakerMode>(1, AudioSpeakerMode.Mode7point1),
        new Pair<int, AudioSpeakerMode>(2, AudioSpeakerMode.Mono),
        new Pair<int, AudioSpeakerMode>(3, AudioSpeakerMode.Prologic),
        new Pair<int, AudioSpeakerMode>(4, AudioSpeakerMode.Quad),
        new Pair<int, AudioSpeakerMode>(5, AudioSpeakerMode.Raw),
        new Pair<int, AudioSpeakerMode>(6, AudioSpeakerMode.Stereo),
        new Pair<int, AudioSpeakerMode>(7, AudioSpeakerMode.Surround)
    };

    
	// Use this for initialization
	void Start () {
        audioLevel = PlayerPrefs.GetFloat("AudioLevel");
        qualityLevel = PlayerPrefs.GetInt("QualityLevel");
        speakerModeInt = PlayerPrefs.GetInt("SpeakerMode");

        foreach (var item in modeIntList)
        {
            if (item.First == speakerModeInt)
            {
                speakerMode = item.Second;
            }
        }
        SetData();

	}
	
    void SetData()
    {
        //AudioSettings.speakerMode = speakerMode;
        AudioListener.volume = audioLevel;
        QualitySettings.SetQualityLevel(qualityLevel);
    }
}
