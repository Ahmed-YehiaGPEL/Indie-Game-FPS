/* Script: HUDManager.cs
 * Date: July 5,2015
 * Description:
 *  Manages the HUD of the player,
 *  Ammo,Health,Weapons,MEssages
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum GameViewState
{
    Normal,
    Paused,
    Objective
}
[ExecuteInEditMode]
public class HUDManager : MonoBehaviour {
    
    //public GUISkin hudSkin;
    //Here we define rectangles for every area
    [Header("Weapons")]
    public List<Weapon> weapons;
    public float weaponWindowHeight;
    public float weaponWindowWidth;
    public float weaponsOffset;
    private Rect weaponIndicatorRect;
    public GUIContent weaponContent;
    public bool weaponWindowStyle;
    public int weaponWindowStyleName;

    [Header("Player Information")]
    
    public float infoWindowWidth;
    public float infoWindowHeight;
    public GUIContent HPContent;
    public float hpWidth;
    public GUIContent infoContent;
    public bool infoWindowStyle;
    public int infoWindowStyleName;
    private Rect infoIndicatorRect;

    [Header("Inventory")]
    public float messageWindowWidth;
    public float messageWindowHeight;
    private Rect messageWindowRect;
    public GUIContent messageContent;
    public bool messageWindowStyle;
    public int messageWindowStyleName;
    public GUIContent kitContent, adrenalineContent, objectiveContent;
    public float kitWidth, adrWidth;

    [Header("Pause Window")]
    public float pauseWindowWidth;
    public float pauseWindowHeight;
    private Rect pauseWindowRect;
    public GUIContent pauseContent;
    public bool pauseWindowStyle;
    public int pauseWindowStyleName;

    [Header("Objective Window")]
    public float objWindowWidth;
    public float objWindowHeight;
    private Rect objWindowRect;
    public GUIContent objContent;
    public bool objWindowStyle;
    public int objWindowStyleName;

    public WeaponManager _weaponManager;
    public List<Weapon> _wepList;
    public List<GUIContent> contents;
    [System.NonSerialized]
    public bool _contentAdded = false;

    public GameViewState currentState;
    [Header("Gadgets")]

    public Texture flashLightON;
    public Texture flashLightOff;
    public Texture nightVisionOn, nightVisionOff;
    public GadgetManager _gadgetManager;
    public GUISkin gSkin;
    public bool paused;
    public bool objWindow;
    public Color gColor;
    
    [TextArea()]
    public string Objectives;
    [System.NonSerialized]
    public string[] _objs;
    // Use this for initialization
	void Awake()
    {
        _weaponManager = GameObject.FindGameObjectWithTag("WeaponCollection").GetComponent<WeaponManager>();
        _wepList = _weaponManager.weapons;        
    }
    void Start () {
       
	}
    void OnLevelWasLoaded()
    {
        _objs = GetObjectives(Objectives);
    }
    public string[] GetObjectives(string str)
    {
        return str.Split('\n');
    }
	// Update is called once per frame
	void Update () {
     
            if (_contentAdded == false)
            {
                foreach (var item in _wepList)
                {
                    contents.Add(item.GUIInfo);
                }
                _contentAdded = true;
            }
            if (Input.GetButtonDown("Pause") && !objWindow)
            {
                paused = !paused;

            }
            if (Input.GetButtonDown("Objectives") && !paused)
            {
                objWindow = !objWindow;
            }
            if (paused)
            {
                currentState = GameViewState.Paused;
                DoPauseEffects(true);
            }
            else if(!objWindow)
            {
                currentState = GameViewState.Normal;
                DoPauseEffects(false);
            }
            if (objWindow)
            {
                currentState = GameViewState.Objective;
                Screen.lockCursor = true;
                Screen.showCursor = false;
                Time.timeScale = 0;
                Camera.main.GetComponent<Blur>().enabled = true;


            }
            else if(!paused)
            {
                currentState = GameViewState.Normal;
                Time.timeScale = 1;
                Camera.main.GetComponent<Blur>().enabled = false;

            }
       
	}
    void DoPauseEffects(bool _do)
    {
        if (_do)
        {
            Screen.lockCursor = false;
            Screen.showCursor = true;
            Time.timeScale = 0;
            
        }
        else
        {
            Screen.lockCursor = true;
            Screen.showCursor = false;
            Time.timeScale = 1;
        }
        Camera.main.GetComponent<Blur>().enabled = _do;
    }
    void OnGUI()
    {
        GUI.skin = gSkin;
        
        if (currentState == GameViewState.Normal)
        {
            //  GUI.skin = hudSkin;
            //Drawing the weapons
            
            weaponIndicatorRect = new Rect(weaponsOffset, (Screen.height - weaponWindowHeight) / 2, weaponWindowWidth, weaponWindowHeight);
           if(weaponWindowStyle)
                weaponIndicatorRect = GUI.Window(0, weaponIndicatorRect, DrawWindow, weaponContent,gSkin.customStyles[weaponWindowStyleName]);
           else
               weaponIndicatorRect = GUI.Window(0, weaponIndicatorRect, DrawWindow, weaponContent);
            
            
           infoIndicatorRect = new Rect((Screen.width - infoWindowWidth), (Screen.height - infoWindowHeight), infoWindowWidth, infoWindowHeight);
           if(infoWindowStyle)
               infoIndicatorRect = GUI.Window(1, infoIndicatorRect, DrawWindow, infoContent,gSkin.customStyles[infoWindowStyleName]);
           else
               infoIndicatorRect = GUI.Window(1, infoIndicatorRect, DrawWindow,infoContent);

            messageWindowRect = new Rect(0, (Screen.height - messageWindowHeight), messageWindowWidth, messageWindowHeight);
            if (messageWindowStyle)
                messageWindowRect = GUI.Window(2, messageWindowRect, DrawWindow, messageContent, gSkin.customStyles[messageWindowStyleName]);
            else
                messageWindowRect = GUI.Window(2, messageWindowRect, DrawWindow, messageContent);

      
            
        }
        else if (currentState == GameViewState.Paused)
        {
            //Draw Pause
            //We want the rect in the middle of the screen
            pauseWindowRect = new Rect((Screen.width - pauseWindowWidth) / 2, (Screen.height - pauseWindowHeight) / 2, pauseWindowWidth, pauseWindowHeight);
            if (pauseWindowStyle)
                pauseWindowRect = GUI.Window(3, pauseWindowRect, DrawWindow, pauseContent,gSkin.customStyles[pauseWindowStyleName]);
            else
                pauseWindowRect = GUI.Window(3, pauseWindowRect, DrawWindow, pauseContent);
        }
        else if(currentState == GameViewState.Objective)
        {
          
                objWindowRect = new Rect((Screen.width - objWindowWidth) / 2, (Screen.height - objWindowHeight) / 2, objWindowWidth, objWindowHeight);

                if (messageWindowStyle)
                    objWindowRect = GUI.Window(4, objWindowRect, DrawWindow, objContent, gSkin.customStyles[objWindowStyleName]);
                else
                    objWindowRect = GUI.Window(4, objWindowRect, DrawWindow, objContent);
            
        }
    }
    void DrawWindow(int id)
    {
        switch (id)
        {
                //Weapon INdicator
            case 0:
                GUILayout.BeginVertical();
                foreach (var item in contents)
                {
                    Color defColor = GUI.color;
                    if (_weaponManager.selectedWeapon.name == item.text)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white ;
                    }
                    GUILayout.Label(item);
                    GUILayout.FlexibleSpace();
                }
                 if (_gadgetManager._flashOn)
                {
                    GUI.color = Color.green;
                    GUILayout.Label(new GUIContent("Flash Light",flashLightON));

                }
                else
                {
                    GUI.color = Color.white;

                    GUILayout.Label(new GUIContent("Flash Light",flashLightOff));
                }
                if (_gadgetManager._nightOn)
                {
                    GUI.color = Color.green;

                    GUILayout.Label(new GUIContent("Night Vision",nightVisionOn));
                }
                else
                {
                    GUI.color = Color.white;

                    GUILayout.Label(new GUIContent("Night Vision" ,nightVisionOff));
                }
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
               
                break;
                //Info
            case 1:
                GUILayout.BeginHorizontal();
                GUILayout.Label("Agent:");
                GUILayout.Label(GetComponentInParent<Player>()._playerData.PlayerName.Split(' ')[0]);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                
                GUILayout.Label(HPContent,GUILayout.Width(hpWidth));
                GUILayout.Label(GetComponentInParent<Player>()._playerHealthSystem.CurrentHealth.ToString() + '/' + GetComponentInParent<Player>()._playerHealthSystem.MaxHealth.ToString());
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Ammo");
                GUILayout.Label(_weaponManager.selectedWeapon.damageSystem.bullets.ToString());
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Clips");
                GUILayout.Label(_weaponManager.selectedWeapon.damageSystem.clips.ToString());
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Fatal Hits");
                GUILayout.Label(GetComponentInParent<Player>().playerScoreSystem.fatalKill.ToString());
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Hits");
                GUILayout.Label(GetComponentInParent<Player>().playerScoreSystem.totalEnemiesKilled.ToString());
                GUILayout.EndHorizontal();
                break;
                //Message
            case 2:
                //Medikit
                GUILayout.BeginHorizontal();
                GUILayout.Label(kitContent,GUILayout.Width(kitWidth));
                GUILayout.Label(GetComponentInChildren<GadgetManager>().MedKitCount.ToString());
                GUILayout.EndHorizontal();
                //Adrenaline
                GUILayout.BeginHorizontal();
                GUILayout.Label(adrenalineContent,GUILayout.Width(adrWidth));
                GUILayout.Label(GetComponentInChildren<GadgetManager>().AdrenalineShotCount.ToString());
                GUILayout.EndHorizontal();
                //Objective
                GUILayout.BeginHorizontal();
                GUILayout.Label(objectiveContent, GUILayout.Width(adrWidth));
                GUILayout.Label(GetComponentInChildren<GadgetManager>().PickUpsLeft.ToString());
                GUILayout.EndHorizontal();
                break;
            case 3:
                GUILayout.Label("Would you like to exit to MainMenu ?");
                if (GUILayout.Button("Back To Menu"))
                {
                    Application.LoadLevel(0);
                }
                else if (GUILayout.Button("Back To Game"))
                {
                    paused = false;
                }
                break;
            case 4:
                GUILayout.FlexibleSpace();
                foreach (var item in _objs)
                {
                    GUILayout.Label(item);  
                }
                break;
            default:
                break;
        }
    }

    IEnumerator FadeOutText(float time)
    {

        yield return new WaitForSeconds(time);
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        while (gColor.a >0)
        {
            gColor.a -= Time.deltaTime;
            yield return null;

        }
    }
}
/*
 
        //  GUI.skin = hudSkin;
        //Drawing the weapons
        GUILayout.BeginArea(weaponIndicatorRect);
        GUILayout.BeginVertical();
        foreach (var item in weapons)
        {
            GUILayout.Label(item);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        ////Drawing the information area
        GUILayout.BeginArea(infoIndicatorRect);
        GUILayout.BeginHorizontal();
        //Here draw
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        //Drawing the message
        messageWindowRect = new Rect((Screen.width - messageWindowWidth) / 2, 0, messageWindowWidth, messageWindowHeight);
        GUILayout.BeginArea(messageWindowRect);
        
        //Draw the message here
 */