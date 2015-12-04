/*
 * Script Name: GadgetManager.cs
 * Author: Ahmed Yehia
 * Date: 14/8/2015
 * Description:
 *  Controls player gadgets such as FlashLight, Night Vision, Radio etc.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GadgetManager : MonoBehaviour {
    public List<MonoBehaviour> _nightVisionEffects;
    public Light flashLight;
    public Color curColor;
    public float Alpha;
    public float smoothness;
    public int MedKitCount, AdrenalineShotCount;

    public bool _flashOn = false, _nightOn = false;
    public int PickUpsLeft;
	// Use this for initialization
	void Start () {
        foreach (var item in _nightVisionEffects)
        {
            item.enabled = false;
        }
        flashLight.gameObject.SetActive(false);
        curColor = RenderSettings.ambientLight;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("ToggleFlashLight"))
        {
            _flashOn = !_flashOn;
            flashLight.gameObject.SetActive(!flashLight.gameObject.active);
        }
        if (Input.GetButtonDown("ToggleNightVision"))
        {
            _nightOn = !_nightOn;
            foreach (var item in _nightVisionEffects)
            {
                item.enabled = !item.enabled;
            
            }

            if (_nightOn)
            {
                Color cur = Color.Lerp(curColor, new Color(53, 53, 53, 255), Time.deltaTime * smoothness);
                
                RenderSettings.ambientLight = cur;
            }
            else
            {
                RenderSettings.ambientLight = curColor;
            }
        }
	}
}
