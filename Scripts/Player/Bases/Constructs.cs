/**
 * Script:  Constructs.cs
 * Date:    July 27
 * Author:  Ahmed Yehia
 * Description:
 *  Holds game classes for constructions
 */
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;




namespace Constructions
{

    [Serializable]  
    public class HealthSystem
    {
        public int MaxHealth;
        public int CurrentHealth;
        public int medKitValue;
        public AudioClip medKitSound, adrenalineSound;
        public float adrenalineTime;
        public List<MonoBehaviour> adrenalineEffect;
        public bool isAdrenaline;
        public float maxAdrSpeed;
    }
    [Serializable]
    public class PlayerAudioSystem
    {
        public List<AudioClip> footSteps;
        public float footStepsTimeNormal, footStepsTimeAim, scale;
        public bool isStepping;
        public List<AudioClip> damageAudioClips;
        public AudioClip dieAudioClip;
    }
    [Serializable]
    public class PlayerDamageSystem
    {
        public Image hitHolder;
        public float timeToFade;
    }
    [Serializable]
    public class PlayerScoreSystem
    {
        [NonSerialized]
        public int score;

        public int fatalKill;
        public int totalEnemiesKilled;
    }
    public enum ScoreType
    {
        FatalKill,
        NormalHit,
        Other
    }
}

