using UnityEngine;
using System.Collections;

public class LevelCheckOut : MonoBehaviour {

    public bool CanGo;
    public bool playerIn;
     Player _playerRef;
	// Use this for initialization
	void Start () {
        _playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider Other)
    {
        if (Other.transform.root.tag == "Player")
        {
            if (Other.transform.GetComponentInChildren<GadgetManager>().PickUpsLeft == 0)
            {
                CanGo = true;
                if (CanGo)
                {
                    if (Input.GetButtonDown("Action"))
                    {
                        
                        Application.LoadLevel(Application.loadedLevel + 1);
                        print("Yes");
                    }
                }
            }
        
        }
    }
    void OnTriggerStay(Collider Other)
    {
        if (Other.transform.root.tag == "Player")
        {
            playerIn = true;
            if (CanGo)
            {
                if (Input.GetButtonDown("Action"))
                {
                    
                    Application.LoadLevel(Application.loadedLevel + 1);
                    
                    print("Yes");
                }
            }
        }
     
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.transform.root.tag == "Player")
        {
            playerIn = false;
           
        }
    }
    void OnGUI()
    {
        GUI.color = Color.red;
        if (playerIn && CanGo)
        {
            GUI.Label(new Rect((Screen.width / 2), 3, 200, 200), "Press Action Button to Next level");
        }
        else if (playerIn && !CanGo)
        {
            GUI.Label(new Rect((Screen.width / 2), 3, 200, 200), "Finish collecting items !");
        }
    }
}
