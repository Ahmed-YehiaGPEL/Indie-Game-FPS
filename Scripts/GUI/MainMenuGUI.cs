using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Constructions;
using System.Net.Mail;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
/*
 *  Date: 07/01/2015
 *  Author: Ahmed Yehia
 *  Description: The UI for main menu manger
 */
public enum ViewState
{
    Normal,
    Options,
    Credits,
    Data,
    Exit,
    Tutorial
}
[ExecuteInEditMode]
public class MainMenuGUI : MonoBehaviour
{
    public const string MatchEmailPattern =
           @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
               + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
               + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
               + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    #region Buttons
    [Header("Buttons")]
    public float startButtonWidth;
    public float startButtonHeight;
    public GUIContent startBUttonContent;

    private Rect startButtonRect;
    #endregion 
    #region Window
    [Header("Side Window")]
    public float sideWindowWidth;
    public float sideWindowHeight;
    public GUIContent optionsButtonContent, creditsButtonContent, exitButtonsContent,tutorialButtonContent, sideContent;

    private Rect sideWindowRect;
    [Header("Options Window")]
    public float optionsWindowWidth;
    public float optionsWindowHeight;
    public  int qualityLevel;
    public  float backgrounAudioLevel;
    public  float gameAudioLevel;
    public GUIContent saveButtonContent;
    public GUIContent backButtonContent;
    public GUIContent optionsContent;
    
    private Rect optionsWindowRect;

    [Header("TutorialWindow")]
    public float tutorialWindowWidth;
    public float tutorialWindowHeight;
    public GUIContent okButtonContent;
    public GUIContent tutorialContent;
    public GUIContent[] TutorialData;
    
    private Rect tutorialWindowRect;

    [Header("Credits Window")]
    public float creditsWindowWidth;
    public float creditsWindowHeight;
    public GUIContent creditsContent;

    private Rect creditsWindowRect;

    [Header("Exit Window")]
    public float exitWindowWidth;
    public float exitWindowHeight;
    public GUIContent exitContent;
    public GUIContent exitMessage;

    private Rect exitWindowRect;

    [Header("Data Window")]
    public float dataWindowWidth;
    public float dataWindowHeight;
    public GUIContent dataContent;

    private Rect dataWindowRect;
    #endregion
    public ViewState currentState;
    string ToolTip;
    public string playerName, playerEmail;
    public GUISkin gSkin;
    [TextArea]
    public string Credits;
    public int selectedAudioSetting;
    private float tempAudioVolume;
    private int tempQualityLevel;
    //AudioSpeakerMode tempMode;//
    
    private List<Pair<int, AudioSpeakerMode>> modeIntList = new List<Pair<int, AudioSpeakerMode>>
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
    private bool preloaded;
    void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
      
      
        //tempMode = AudioSettings.speakerMode;
        tempAudioVolume = AudioListener.volume;
        tempQualityLevel = QualitySettings.GetQualityLevel();

        //qualityLevel = QualitySettings.GetQualityLevel();
        //gameAudioLevel = AudioListener.volume;
    }

    void FixedUpdate()
    {
        ToolTip = GUI.tooltip;
    }
    // Update is called once per frame
    void Update()
    {
    
        
    }
    void OnGUI()
    {
        Screen.lockCursor = false;
        Screen.showCursor = true;
        GUI.skin = gSkin;
        ToolTip = GUI.tooltip;

        #region SettingUpRectangle

        //Setting up rectangle automaticly
        startButtonRect.x = (Screen.width - startButtonWidth) / 2;
        startButtonRect.y = (Screen.height - startButtonHeight) / 2;
        startButtonRect.width = startButtonWidth;
        startButtonRect.height = startButtonHeight;
        //Setting up window rectangle
        #region Side Window
        sideWindowRect.x = Screen.width - sideWindowWidth;
        sideWindowRect.y = Screen.height - sideWindowHeight;
        sideWindowRect.width = sideWindowWidth;
        sideWindowRect.height = sideWindowHeight;
        #endregion
        #region Options Window
        optionsWindowRect.x = (Screen.width - optionsWindowWidth)/2;
        optionsWindowRect.y = (Screen.height - optionsWindowHeight)/2;
        optionsWindowRect.width = optionsWindowWidth;
        optionsWindowRect.height = optionsWindowHeight;
        /***********************************************/

        #endregion
        #region Tutorial Window
        tutorialWindowRect.x = (Screen.width - tutorialWindowWidth) / 2;
        tutorialWindowRect.y = (Screen.height -tutorialWindowHeight) / 2;
        tutorialWindowRect.width = tutorialWindowWidth;
        tutorialWindowRect.height = tutorialWindowHeight;
        /***********************************************/

        #endregion
        #region Credits Window
        creditsWindowRect.x = (Screen.width - creditsWindowWidth)/2;
        creditsWindowRect.y = (Screen.height - creditsWindowHeight)/2;
        creditsWindowRect.width = creditsWindowWidth;
        creditsWindowRect.height = creditsWindowHeight;
       #endregion
        #region Exit Window
        exitWindowRect.x = (Screen.width - exitWindowWidth)/2;
        exitWindowRect.y = (Screen.height - exitWindowHeight)/2;
        exitWindowRect.width = exitWindowWidth;
        exitWindowRect.height = exitWindowHeight;

        #endregion
        #region Data Window
        dataWindowRect.x = (Screen.width - dataWindowWidth) / 2;
        dataWindowRect.y = (Screen.height - dataWindowHeight) / 2;
        dataWindowRect.width = dataWindowWidth;
        dataWindowRect.height = dataWindowHeight;

        #endregion

        #endregion

        #region Drawing
        
        //Drawing Windows
       switch (currentState)
       {
           case ViewState.Normal:
               GUI.Window(0, sideWindowRect, DrawWindow, sideContent);
               if (GUI.Button(startButtonRect, startBUttonContent))
               {
                   currentState = ViewState.Data;
               }

               break;
           case ViewState.Options:
               GUI.Window(1, optionsWindowRect, DrawWindow, optionsContent);
               break;
           case ViewState.Credits:
               GUI.Window(2, creditsWindowRect, DrawWindow, creditsContent);
               break;
           case ViewState.Exit:
               GUI.Window(3, exitWindowRect, DrawWindow, exitContent);
               break;
           case ViewState.Data:
               GUI.Window(4, dataWindowRect, DrawWindow,dataContent);
               break;
           case ViewState.Tutorial:
               GUI.Window(5, tutorialWindowRect, DrawWindow, tutorialContent);
               break;
       }
       ToolTip = GUI.tooltip;

       GUILayout.Label(ToolTip);
        #endregion
    }
    void DrawWindow(int id)
    {
        switch (id)
        {
            #region MainView
            //Main View
            case 0:
                if (GUILayout.Button(optionsButtonContent))
                {
                    //Switch State
                    currentState = ViewState.Options;
                    preloaded = true;
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(creditsButtonContent))
                {
                    //Switch State
                    currentState = ViewState.Credits;
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(tutorialButtonContent))
                {
                    //Switch State
                    currentState = ViewState.Tutorial;
                }
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(exitButtonsContent))
                {
                    //Switch State
                    currentState = ViewState.Exit;
                }
                GUILayout.FlexibleSpace();
                break;
            #endregion
            #region Options
            //Options view
            case 1:
                if (preloaded == true)
                {
                    gameAudioLevel = tempAudioVolume;
                    qualityLevel = tempQualityLevel;
                    //selectedAudioSetting = IntByMode(tempMode);
                    preloaded = false;
                }
                /*****/
                //Store Temp Values First
                //
                /******/
                GUILayout.BeginHorizontal();
                GUILayout.Label("Quality Level");

                QualitySettings.SetQualityLevel(qualityLevel, true);

                qualityLevel = (int)(GUILayout.HorizontalSlider(qualityLevel, 1, 5));
                GUILayout.EndHorizontal();
                
                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                
                GUILayout.Label("Game audio level:");
                AudioListener.volume = gameAudioLevel;
                
                gameAudioLevel = (GUILayout.HorizontalSlider(gameAudioLevel, 0.0f, 1.0f));
                GUILayout.EndHorizontal();
                
                GUILayout.FlexibleSpace();
                
                //selectedAudioSetting = GUILayout.SelectionGrid(selectedAudioSetting, new string[]{"5.1","7.1","Mono","Prologic","Quad","Raw","Stereo","Surround" }, 2);
                //AudioSettings.speakerMode = ModeByInt(selectedAudioSetting);
                
                GUILayout.BeginVertical();
                
                if (GUILayout.Button("Save"))
                {
                    //save
                    AudioListener.volume = gameAudioLevel;
                    QualitySettings.SetQualityLevel(qualityLevel, true);
                   // AudioSettings.speakerMode = ModeByInt(selectedAudioSetting);
                    tempAudioVolume = AudioListener.volume;
                    tempQualityLevel = QualitySettings.GetQualityLevel();
                   // tempMode = AudioSettings.speakerMode;
                    currentState = ViewState.Normal;
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Back"))
                {
                    AudioListener.volume = tempAudioVolume;
                    QualitySettings.SetQualityLevel(tempQualityLevel, true);
                  //  AudioSettings.speakerMode = tempMode;
                    currentState = ViewState.Normal;
                    preloaded = false;
                }
                GUILayout.FlexibleSpace();  

                GUILayout.EndVertical();
                break;
            #endregion
            //
            #region Credits
            case 2:
                //Credits
                GUILayout.BeginVertical();
                foreach (string item in Credits.Split('\n'))
                {
                    GUILayout.Label(item);
                }
                GUILayout.EndVertical();
                if (GUILayout.Button("Back"))
                {
                    currentState = ViewState.Normal;
                }
                break;
            case 3:
                GUILayout.Label(exitMessage);
                
                GUILayout.BeginVertical();
                if (GUILayout.Button("Yes"))
	            {
                    //Apply exit procedure
                    Application.Quit();
	            }
                GUILayout.FlexibleSpace();
                if(GUILayout.Button("No"))
                {
                    currentState = ViewState.Normal;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                
                break;
            #endregion
            #region Data
            case 4:
                GUILayout.BeginHorizontal();
                GUILayout.Label("Name");
                playerName = GUILayout.TextField(playerName);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("E-Mail");
                playerEmail = GUILayout.TextField(playerEmail);
                GUILayout.EndHorizontal();
                if(!IsEmail(playerEmail))
                        GUILayout.Label("Invalid Email",gSkin.customStyles[0]);

                if(GUILayout.Button("Start"))
                {
                    if (playerName != string.Empty)
                    {

                        if (IsEmail(playerEmail))
                        {
                            //Start
                            GameObject.FindGameObjectWithTag("DataHolder").GetComponent<PlayerData>().PlayerName = playerName;
                            GameObject.FindGameObjectWithTag("DataHolder").GetComponent<PlayerData>().PlayerEmail = playerEmail;
                            Debug.Break();
                            Application.LoadLevel(Application.loadedLevel+1);
                        }
                    
                         
                   }
                }//
                if (GUILayout.Button("Back"))
                {
                    currentState = ViewState.Normal;
                }
                break;
            #endregion
            #region Tutorial
            case 5:
                foreach (var item in TutorialData)
                {
                    GUILayout.Label(item);
                    GUILayout.FlexibleSpace();
                }
                if(GUILayout.Button(okButtonContent))
                {
                    currentState = ViewState.Normal;
                }
                break;
            #endregion
            default:
                break;
        }
    }
    public static bool IsEmail(string email)
    {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
  AudioSpeakerMode ModeByInt(int no)
    {
        foreach (var item in modeIntList)
        {
            if (item.First == no)
            {
                return item.Second;
            }
        }
      //Return Stereo ByDefault
        return AudioSpeakerMode.Stereo;
    }
  int IntByMode(AudioSpeakerMode mode)
  {
      foreach (var item in modeIntList)
      {
          if (item.Second == mode)
          {
              return item.First;
          }
      }
      //Return Stereo ByDefault
      return 2;
  }
}
