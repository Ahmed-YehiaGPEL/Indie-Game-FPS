using UnityEngine;
using System.Collections;

public class SirenDetonation : MonoBehaviour {
    public AudioClip sirenClip;
    public AudioClip countDown;
    public GameObject explosionPerfabs;
    public float waitTime;
    public float Radious;
    
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitThenDetonate());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator WaitThenDetonate()
    {
        audio.PlayOneShot(sirenClip);
        yield return new WaitForSeconds(waitTime);
        audio.PlayOneShot(countDown);
        yield return new WaitForSeconds(countDown.length);
        audio.Stop();
        explosionPerfabs.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radious);
        foreach (var item in colliders)
        {
            item.SendMessageUpwards("GetDamage", 10000.0f, SendMessageOptions.DontRequireReceiver);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, Radious);
    }
}
