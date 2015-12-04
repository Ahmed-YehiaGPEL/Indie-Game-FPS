using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GameEnd : MonoBehaviour {

    public GameObject dataHolder, playerRef;
    public Text _textInSequence;
    public float _score, _health, _minutes,_seconds;
    public string _name, _email;
    public Rect _windowRect;
    public GUISkin gSkin;
	// Use this for initialization
	void Start () {
	
	}
	
    void OnLevelWasLoaded(int levelNum)
    {

        dataHolder = GameObject.FindGameObjectWithTag("DataHolder");
        
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerRef.SetActive(false);
        _score = playerRef.GetComponent<Player>().playerScoreSystem.score;
        _health = playerRef.GetComponent<Player>()._playerHealthSystem.CurrentHealth;
        _minutes = dataHolder.GetComponent<ScoreManager>().minutes;
        _seconds = dataHolder.GetComponent<ScoreManager>().seconds;
        _name = dataHolder.GetComponent<PlayerData>().PlayerName;
        _email = dataHolder.GetComponent<PlayerData>().PlayerEmail;
       
        
    }
	// Update is called once per frame
	void Update () {
        
        
    }
    void OnGUI()
    {
        GUI.skin = gSkin;

        _windowRect.x = (Screen.width  - _windowRect.width)/ 2;
        _windowRect.y = (Screen.height - _windowRect.height) /2;

        _windowRect = GUILayout.Window(0, _windowRect, DrawWindow, "Game Over");

    }
    void DrawWindow(int id)
    {
        GUILayout.BeginVertical();

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Name:");
        GUILayout.FlexibleSpace();
        GUILayout.Label(_name);
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Email:");
        GUILayout.FlexibleSpace();
        GUILayout.Label(_email);
        GUILayout.EndHorizontal();
        
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Score:");
        GUILayout.FlexibleSpace();
        GUILayout.Label(_score.ToString());
        GUILayout.EndHorizontal();
        
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Mission Time:");
        GUILayout.Label(_minutes.ToString("00") + ':' + _seconds.ToString("00"));
        GUILayout.EndHorizontal();


        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Health:");
        GUILayout.Label(_health.ToString());
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Back to Main Menu"))
        {
            DestroyImmediate(dataHolder.gameObject);
            DestroyImmediate(playerRef.gameObject);
            Application.LoadLevel(0);
        }

    }
}
