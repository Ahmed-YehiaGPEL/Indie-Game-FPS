using UnityEngine;
using System.Collections;

public class MineDetonator : MonoBehaviour {
    public AudioClip detonationSound;
    public GameObject explosionPerfab;
    public bool hasForce = true;
    public float forceToCast = 450.0f;
    public float explosionRadius = 19.5f;
    public float explosionDamage = 3.5f;
    public float fadeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        print(other.transform.tag);
        if (other.transform.root.tag == "Player")
        {
            Explode();
        }
    }

    private void Explode()
    {
        audio.PlayOneShot(detonationSound);
        //Apply force
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(forceToCast, transform.position, explosionRadius, 3.0F);

            hit.SendMessageUpwards("GetDamage", explosionDamage, SendMessageOptions.DontRequireReceiver);



        }
        StartCoroutine(WaitThenInstantiate(fadeTime));
    }
    IEnumerator WaitThenInstantiate(float _time)
    {
        yield return new WaitForSeconds(_time);
        Instantiate(explosionPerfab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, explosionRadius);
    }
}
