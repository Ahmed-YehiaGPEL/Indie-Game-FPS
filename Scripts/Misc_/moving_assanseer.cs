using UnityEngine;
using System.Collections;

public class moving_assanseer : MonoBehaviour
{
    public GameObject targetobject;
    public GameObject targetobject2;
    public Vector3 target;
    public Vector3 target2;
    public int maxSpeed = 50;

    public float radius = 0.01f;
    public float timeToTarget = 0.25f;


    public bool goingDown = false;
    public bool goingUp = false;
    public bool canCall;
    // Use this for initialization
    void Start()
    {
        targetobject = GameObject.FindGameObjectWithTag("target");
        targetobject2 = GameObject.FindGameObjectWithTag("target2");

    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            canCall = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            canCall = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canCall)
        {

            if (Input.GetButtonDown("CallElevatorDown") || goingDown == true)
            {
                goingUp = false;
                target = targetobject.transform.position;
                Vector3 velocity1 = target - transform.position;
                //Debug.Log (transform.position.y);
                if (velocity1.magnitude < radius)
                    return;
                velocity1 /= timeToTarget;
                if (velocity1.magnitude > maxSpeed)
                {
                    velocity1.Normalize();
                    velocity1 *= maxSpeed;
                }
                transform.position += velocity1 * Time.deltaTime / 2;
                //float d = Mathf.Atan2 (velocity1.x, velocity1.z) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.Euler (0, d, 0);
                goingDown = true;

            }
        }


    }
    void FixedUpdate()
    {
        if (canCall)
        {
            if (Input.GetButtonDown("CallElevatorUp") || goingUp == true)
            {
                goingDown = false;
                target2 = targetobject2.transform.position;
                Vector3 velocity2 = target2 - transform.position;
                //Debug.Log (transform.position.y);

                if (velocity2.magnitude < (radius))
                    return;
                velocity2 /= timeToTarget;
                if (velocity2.magnitude > maxSpeed)
                {
                    velocity2.Normalize();
                    velocity2 *= maxSpeed;
                }
                transform.position += velocity2 * Time.deltaTime / 2;
                //float d = Mathf.Atan2 (velocity2.x, velocity2.z) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.Euler (0, d, 0);
                goingUp = true;
            }
        }
    }


}

