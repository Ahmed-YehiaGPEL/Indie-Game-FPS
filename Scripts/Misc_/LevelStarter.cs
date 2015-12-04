using UnityEngine;
using System.Collections;

public class LevelStarter : MonoBehaviour {
    public int pickupCount;
    [TextArea(3,10)]
    public string levelObjectives;
    public string[] _ob;
	// Use this for initialization
	void Awake()
    {
        _ob = levelObjectives.Split('\n');
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<HUDManager>().Objectives = levelObjectives;
    }
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
