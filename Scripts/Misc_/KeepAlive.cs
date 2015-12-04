using UnityEngine;
using System.Collections;

public class KeepAlive : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        //if(!(Application.loadedLevel == 0 && gameObject.name == "Player"))
        DontDestroyOnLoad(gameObject);
	}
}
