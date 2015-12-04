/**
 * Script:  Player.cs
 * Date:    July 27
 * Author:  Ahmed Yehia
 * Description:
 *  Represents the player  system
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using Constructions;//Our special namespace *-*


public class Player : MonoBehaviour {

    public PlayerData _playerData;
    public HealthSystem _playerHealthSystem;
    public GameObject DeadPlayerObject;
    [HideInInspector]
    public PlayerAudioSystem audioSystem;
    public PlayerDamageSystem playerDamageSystem;
    public PlayerScoreSystem playerScoreSystem;
    public CharacterMotor charMotorRef;

	// Use this for initialization
	void Start () {
        
        _playerData = (GameObject.FindGameObjectWithTag("DataHolder").GetComponent<PlayerData>());
       // _playerData.gameObject.AddComponent<Player>();
        
	}
	void OnLevelWasLoaded(int level)
    {
        transform.position = GameObject.FindGameObjectWithTag("PlayerStartPoint").transform.position;
        GetComponentInChildren<HUDManager>().GetObjectives(GameObject.FindGameObjectWithTag("PlayerStartPoint").GetComponent<LevelStarter>().levelObjectives);
        GetComponentInChildren<GadgetManager>().PickUpsLeft = GameObject.FindGameObjectWithTag("PlayerStartPoint").GetComponent<LevelStarter>().pickupCount;
    }
	// Update is called once per frame
	void Update () {
        if (_playerHealthSystem.CurrentHealth <= 0)
        {

            KillPlayer();

        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if(!audioSystem.isStepping && GetComponent<CharacterController>().isGrounded)
                if(GetComponentInChildren<Weapon>().isAiming)
                    StartCoroutine(PlayWalkSound(audioSystem.footStepsTimeAim,audioSystem.scale));
                else
                    StartCoroutine(PlayWalkSound(audioSystem.footStepsTimeNormal,audioSystem.scale));
        }
        if (Input.GetButtonDown("UseMedKit"))
        {
            StartCoroutine(UseMedKit());

        }
        if (Input.GetButtonDown("UseAdrShot"))
        {
            StartCoroutine(UseAdrenalineShot());
        }
	}
    IEnumerator UseMedKit()
    {
        if (GetComponentInChildren<GadgetManager>().MedKitCount > 0 && _playerHealthSystem.CurrentHealth != _playerHealthSystem.MaxHealth)
        {
            GetComponentInChildren<GadgetManager>().MedKitCount--;
            audio.PlayOneShot(_playerHealthSystem.medKitSound);
            yield return new WaitForSeconds(_playerHealthSystem.medKitSound.length);
            GetHealth(_playerHealthSystem.medKitValue);
        }
        else
            yield return null;
    }
    IEnumerator UseAdrenalineShot()
    {
        if (GetComponentInChildren<GadgetManager>().AdrenalineShotCount >0 && !_playerHealthSystem.isAdrenaline)
        {
            _playerHealthSystem.isAdrenaline = true;
            GetComponentInChildren<GadgetManager>().AdrenalineShotCount--;
            audio.PlayOneShot(_playerHealthSystem.adrenalineSound);
            yield return new WaitForSeconds(_playerHealthSystem.adrenalineSound.length);
            foreach (var  item in _playerHealthSystem.adrenalineEffect)
            {
                item.enabled = true;
            }
            GetComponentInChildren<Weapon>().isAdrenaline = true;
            Debug.Log("Weapon AD done, now finishing ");
            print(GetComponent<CharacterMotor>().name);
            GetComponent<CharacterMotor>().movement.maxForwardSpeed = _playerHealthSystem.maxAdrSpeed;
            yield return new WaitForSeconds(_playerHealthSystem.adrenalineTime);
            GetComponentInChildren<Weapon>().isAdrenaline = false;

            _playerHealthSystem.isAdrenaline = false;
            GetComponent<CharacterMotor>().movement = charMotorRef.movement;
            foreach (var item in _playerHealthSystem.adrenalineEffect)
            {
                item.enabled = false;
            }

        }
        else
        {
            yield return null;
        }

    }
    void FixedUpdate()
    {
        if (!(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            if (audioSystem.isStepping == true)
            {
                audioSystem.isStepping = false;
            }
        }
        int totHits = this.playerScoreSystem.totalEnemiesKilled;
        int fatal = this.playerScoreSystem.fatalKill;
        int health = this._playerHealthSystem.CurrentHealth;
        int ammo = this.GetComponentInChildren<Weapon>().damageSystem.clips;
        this.playerScoreSystem.score += GameObject.FindGameObjectWithTag("DataHolder").GetComponent<ScoreManager>().CalculateScore(totHits, health, ammo, fatal);
    }
  public  void GetHealth(int HealthPoints)
    {
        this._playerHealthSystem.CurrentHealth += HealthPoints;
        if (this._playerHealthSystem.CurrentHealth > _playerHealthSystem.MaxHealth)
        {
            this._playerHealthSystem.CurrentHealth = _playerHealthSystem.MaxHealth ;
        }
        
        if (_playerHealthSystem.CurrentHealth > _playerHealthSystem.MaxHealth)
        {
            _playerHealthSystem.CurrentHealth = _playerHealthSystem.MaxHealth;
        }
    }
  public void KillPlayer()
  {
      audio.PlayOneShot(audioSystem.dieAudioClip);
      Instantiate(DeadPlayerObject, transform.position, Quaternion.identity);
      this.active = false;
      
      //Destroy(gameObject);
      //Send score to final data
  }
   public void GetDamage(int DamagePoints)
    {
        this._playerHealthSystem.CurrentHealth -= DamagePoints;
        if (_playerHealthSystem.CurrentHealth <= 0)
        {
            KillPlayer();
        }
        else
        {
            DoDamageEffect();
        }
    }
    
   IEnumerator PlayWalkSound(float time,float scale)
   {
       audioSystem.isStepping = true;

       audio.PlayOneShot(audioSystem.footSteps[UnityEngine.Random.Range(0, audioSystem.footSteps.Count - 1)], scale);

        yield return new WaitForSeconds(time);
        audioSystem.isStepping = false;
   }
   public void DoDamageEffect()
    {
        int targetClip = UnityEngine.Random.Range(0, audioSystem.damageAudioClips.Count-1);

       audio.PlayOneShot(audioSystem.damageAudioClips[targetClip]);
       StartCoroutine(DamageColor1());
    }
    IEnumerator DamageColor1()
   {
       playerDamageSystem.hitHolder.color = Color.Lerp(new Color(255, 0, 0, 1.0f), new Color(255, 0, 0, 0.0f), Time.deltaTime * playerDamageSystem.timeToFade);

       yield return new WaitForSeconds(playerDamageSystem.timeToFade);       

       playerDamageSystem.hitHolder.color = Color.Lerp(new Color(255, 0, 0, 0.0f), new Color(255, 0, 0, 1.0f), Time.deltaTime * playerDamageSystem.timeToFade);
   }

   public void Score(ScoreType _scType)
   {
       switch (_scType)
       {
           case ScoreType.FatalKill:
               this.playerScoreSystem.fatalKill++;
               playerScoreSystem.totalEnemiesKilled++;
               break;
           case ScoreType.NormalHit:
               playerScoreSystem.totalEnemiesKilled++;
               break;
           default:
               playerScoreSystem.totalEnemiesKilled++;
               break;
       }
   }

}
