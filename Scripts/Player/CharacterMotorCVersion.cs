using UnityEngine;
using System.Collections;
using System;
public class CharacterMotorCVersion : MonoBehaviour {
    public bool canControl = true;
    public bool useFixedUpdate = true;

    [NonSerialized]
    Vector3 inputMoveDirection = Vector3.zero;
    [NonSerialized]
    bool inputJump = false;

    [Serializable]
    public class CharacterMotorMovement
    {
        public float maxForwardSpeed = 10.0f;
        public float maxSidewaysSpeed = 10.0f;
        public float maxBackwardsSpeed = 10.0f;

        public AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe(-90, 1), new Keyframe(0, 1), new Keyframe(90, 0));
        public float maxGroundAcceleration  = 30.0f;
	    public float maxAirAcceleration  = 20.0f;

        public float gravity = 10.0f;
        public float maxFallSpeed = 20.0f;

        [NonSerialized]
        CollisionFlags collisionFlags;

    }
    public CharacterMotorMovement motor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
