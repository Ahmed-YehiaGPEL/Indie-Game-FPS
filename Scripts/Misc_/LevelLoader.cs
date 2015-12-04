using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public string levelName;
    public Color fadeColor;
    public float length;
	// Use this for initialization
	void Start () {
        LevelLoadFade.FadeAndLoadLevel(levelName, fadeColor, length);
    	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {

    }
}
