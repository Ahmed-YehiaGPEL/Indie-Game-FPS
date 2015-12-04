using UnityEngine;
using System.Collections;
using Constructions;
public class ShootingTarget : MonoBehaviour {

    public Transform HeadSphere, BodyBox;
    private GameObject _player;
    public int headScore, bodyScore;
    public float health;
    public GameObject deadTarget;
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
       
        if (health <= 0)
        {
            
            Instantiate(deadTarget, transform.parent.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
    public void GetDamage(float _points)
    {
        health -= _points;
    }
    public void ApplyHit(HitRegion hitR)
    {
        switch (hitR)
        {
            case HitRegion.Head:
                _player.GetComponent<Player>().Score(ScoreType.FatalKill);
                break;
            case HitRegion.Body:
                _player.GetComponent<Player>().Score(ScoreType.NormalHit);
                break;
            default:
                _player.GetComponent<Player>().Score(ScoreType.Other);
                break;
        }
    }
    
}
