using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {
    public List<Weapon> weapons;
    public Weapon selectedWeapon;
    
    [HideInInspector]
    public int selectedWeaponID;

    private GameObject weaponCollection;
    
    private List<KeyCode> _keyCodes = new List<KeyCode>();
    void Awake()
    {
        //Let's Find our weapons *_*
        weaponCollection = GameObject.FindGameObjectWithTag("WeaponCollection");
        //Let's Fill It Up!
        _keyCodes.AddRange(new KeyCode[] {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9});
    }
	// Use this for initialization
	void Start () {
        weapons.AddRange(weaponCollection.GetComponentsInChildren<Weapon>());
        //Now we have our weapons
        selectedWeapon = weapons[0];
     
        selectedWeaponID = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("ChangeWeapon"))
        {
            if (!(selectedWeapon.isReloading || selectedWeapon.isShooting))
            {
                ChangeWeapon();
            }
        }
        else
        {
            float _delta = Input.GetAxis("Mouse ScrollWheel");
            if (_delta >0f)
            {
                if (!(selectedWeapon.isReloading || selectedWeapon.isShooting))
                {
                    //Up
                    ChangeWeapon();
                }
            }
            else if( _delta < 0f)
            {
                if (!(selectedWeapon.isReloading || selectedWeapon.isShooting))
                {
                    //Down
                    ChangeWeapon(true);
                }
            }
            else
            {

                foreach (KeyCode Key in _keyCodes)
                {
                    if (!(selectedWeapon.isReloading || selectedWeapon.isShooting))
                    {
                        if (Input.GetKeyDown(Key))
                        {
                            int targetWeapon = _keyCodes.FindIndex(0, item => item == Key);  // Note the lambda expressions !
                           if (targetWeapon <= weapons.Count-1)
                           {
                               ChangeWeapon(targetWeapon);
                           }
                        }
                    }
                }
            }
        }
	}
    public void ChangeWeapon()
    {
        int tempID = selectedWeaponID;
        weapons[tempID].gameObject.SetActive(false);
        if (!(selectedWeaponID + 1 >= weapons.Count))
        {
            selectedWeaponID++;
        }
        else
        {
            selectedWeaponID = 0;
        }
        weapons[selectedWeaponID].gameObject.SetActive(true);
        selectedWeapon = weapons[selectedWeaponID];
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ReticleChangerCVersion>().ReticleType = selectedWeapon.ReticleName;
        //selectedWeapon.gameObject.SetActive(true);
        Debug.Log("Changed weapons to " + selectedWeaponID);
    }
    public void ChangeWeapon(bool decrement)
    {
        if (decrement)
        {
            int tempID = selectedWeaponID;
            weapons[tempID].gameObject.SetActive(false);
            if (!(selectedWeaponID - 1 < 0))
            {
                selectedWeaponID--;
            }
            else
            {
                selectedWeaponID = weapons.Count -1;
            }
            weapons[selectedWeaponID].gameObject.SetActive(true);
            selectedWeapon = weapons[selectedWeaponID];
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ReticleChangerCVersion>().ReticleType = selectedWeapon.ReticleName;
            //selectedWeapon.gameObject.SetActive(true);
            Debug.Log("Changed weapons to " + selectedWeaponID);
        }
        else
        {
            ChangeWeapon();
        }
    }
    public void ChangeWeapon(int weaponID)
    {
        int tempID = selectedWeaponID;
        weapons[tempID].gameObject.SetActive(false);

        selectedWeapon = weapons[weaponID];
        selectedWeaponID = weaponID;
        selectedWeapon.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ReticleChangerCVersion>().ReticleType = selectedWeapon.ReticleName;

    }
}
