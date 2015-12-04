using UnityEngine;
using System.Collections;

public class DeadPlayer : MonoBehaviour {

    public float waitTime;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitAndKill());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitAndKill()
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(4);
        //Do Load Effect;
    }

}
