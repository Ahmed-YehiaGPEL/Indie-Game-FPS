using UnityEngine;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Security;

public class ScoreManager : MonoBehaviour {
    public bool _count;
    float timer = 0.0f;
    public float minutes, seconds;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_count)
        {
            timer += Time.deltaTime;
        }
        minutes = Mathf.Floor(timer / 60.0f);
        seconds = Mathf.Floor(timer % 60);

	}
    
    void OnGUI()
    {
        if(Application.loadedLevel != 0 && Application.loadedLevel != 4)
        GUILayout.Label("Mission time:"+minutes.ToString("00")+ ':' + seconds.ToString("00"));
    }
    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            _count = true;
        }
    }
    public int CalculateScore(int _totalEnemies,int _currentHealth,int _currentAmmo,int fatalHits)
    {
        float _score = 0;
        _score = (( _totalEnemies-fatalHits) / (timer)) * ((_currentHealth + _currentAmmo + fatalHits));
        return (int)_score;
    }
}
