using UnityEngine;
using System.Collections;

public enum HitRegion
{
    Head,
    Body,
    Other
}
public class HitListener : MonoBehaviour {
    HitRegion hitRegion;
	// Use this for initialization
	void Start () {
        switch (this.tag)
        {
            case "HitHead":
                this.hitRegion = HitRegion.Head;
                break;
            case "HitBody":
                this.hitRegion = HitRegion.Body;
                    break;
            default:
                    this.hitRegion = HitRegion.Other;
                    break;

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SendHit()
    {
        transform.parent.GetComponent<ShootingTarget>().ApplyHit(this.hitRegion);
    }
    public void GetDamage(float d)
    {
        transform.parent.GetComponent<ShootingTarget>().GetDamage(d);

    }

}
