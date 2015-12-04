using UnityEngine;
using System.Collections;

public enum ItemType
{
    Ammo,
    MedKit,
    AdrenalineShot,
    MedAndAdrenaline,
    PickupObjective
}
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour {

    public ItemType itemType;
    public int Amount, adrAmount, medAmount;
    public bool hasAudio;
    public AudioClip tookClip;
    public float cutThreshold = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            switch (itemType)
            {
                case ItemType.Ammo:
                    other.transform.GetComponentInChildren<Weapon>().damageSystem.clips += Amount;
                    break;
                case ItemType.MedKit:
                    other.transform.GetComponentInChildren<GadgetManager>().MedKitCount += Amount;
                    break;
                case ItemType.AdrenalineShot:
                    other.transform.GetComponentInChildren<GadgetManager>().AdrenalineShotCount += Amount;
                    break;
                case ItemType.MedAndAdrenaline:
                    other.transform.GetComponentInChildren<GadgetManager>().AdrenalineShotCount += adrAmount;
                    other.transform.GetComponentInChildren<GadgetManager>().AdrenalineShotCount += medAmount;
                    break;
                case ItemType.PickupObjective:
                    other.transform.GetComponentInChildren<GadgetManager>().PickUpsLeft--;
                    break;
            }
            if (hasAudio)
                StartCoroutine(WaitAndKill());
            else
            {
                try
                {
                    Destroy(this.gameObject.transform.parent.gameObject);

                }
                catch
                {
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
    IEnumerator WaitAndKill()
    {
        audio.PlayOneShot(tookClip);
        yield return new WaitForSeconds(tookClip.length - cutThreshold);

        try
        {
            Destroy(this.gameObject.transform.parent.gameObject);

        }
        catch
        {
            Destroy(this.gameObject);
        }
    }
}

