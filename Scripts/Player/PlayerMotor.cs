/**
 * Script:  PlayerMotor.cs
 * Date:    July 26
 * Author:  Ahmed Yehia
 * Description:
 *  Represents the player motor system, controls movement and other operations
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMotor : MonoBehaviour {

    //private List<Weapon> playerWeapons;
    bool _forward, _backward, _right, _left;
    public float forwardSpeed, backwardSpeed, rightSpeed, leftSpeed, angleSpeed;
	// Use this for initialization
	void Awake()
    {
        // playerWeapons.AddRange(GetComponentsInChildren<Weapon>());
    }
    void Start () {
	
	}
	// Update is called once per frame
	void Update () {



        _forward = Input.GetButton("Forward");
        _backward = Input.GetButton("Backward");
        _right = Input.GetButton("Right");
        _left = Input.GetButton("Left");

        if (_forward && _backward || _right && _left)
        {
            return;
        }
        else
        {
            if (_forward)
            {
                if(_left)
                    rigidbody.AddRelativeForce(new Vector3(-1,0,1) * angleSpeed);
                if (_right)
                    rigidbody.AddRelativeForce(new Vector3(1, 0, 1) * angleSpeed);
                else
                    rigidbody.AddRelativeForce(Vector3.forward * forwardSpeed);
            }
            if (_backward)
            {
                if(_left)
                    rigidbody.AddRelativeForce(new Vector3(-1,0,-1) * angleSpeed);
                if(_right)
                    rigidbody.AddRelativeForce(new Vector3(1, 0, -1) * angleSpeed);
                else
                    rigidbody.AddForce(Vector3.back * backwardSpeed);
            }
            if (_left)
            {
                rigidbody.AddForce(Vector3.left * leftSpeed);
            }
            if (_right)
            {
                rigidbody.AddForce(Vector3.right * rightSpeed);
            }
        }
	}

}
