using UnityEngine;
using System.Collections;

public class TER : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.X))
        {
            Debug.Log("sssss");
            StartCoroutine("Example");
        }
        
	}
    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(5);
        print("WaitAndPrint " + Time.time);
    }
    IEnumerator Example()
    {
        print("Starting " + Time.time);
        yield return WaitAndPrint();
        print("Done " + Time.time);
    }
}
