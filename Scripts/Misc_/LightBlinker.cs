/**
 * Script:  LigthBlinker.cs
 * Date:    August 15
 * Author:  Ahmed Yehia
 * Description:
 *  Light blinking on/off over time for specific blinks number
 */
using UnityEngine;
using System.Collections;

public class LightBlinker : MonoBehaviour {

    public int minmumBlinks;
    public int maximumBlinks;
    public float timeBetweenFlashes;
    public float maxTimeBetweenBlinks = 2.0f, minTimeBetweenBlinks = 6.0f;

    float randomThreshold;
    private Light _lightSource;
    bool isFlashing = false;
    float timer = 0.0f;
    // Use this for initialization
	void Start () {
        _lightSource = GetComponent<Light>();
        randomThreshold = Random.Range(minTimeBetweenBlinks, maxTimeBetweenBlinks);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isFlashing)
        {
            timer += Time.deltaTime;
        }
        if(timer >randomThreshold )
        {
            timer = 0.0f;
            RandomLightBlink(minmumBlinks, maximumBlinks);
        }
	}
    void RandomLightBlink(int minBlinks,int maxBlinks)
    {
        int blinkTimes = UnityEngine.Random.Range(minBlinks, maxBlinks);
        isFlashing = true;
        for (int i = 0; i < blinkTimes; i++)
        {
                StartCoroutine(LightBlinkENU(timeBetweenFlashes));
        }
        isFlashing = false;
        timer = Random.Range(minTimeBetweenBlinks, maxTimeBetweenBlinks);

    }
    IEnumerator LightBlinkENU(float timeBetweenFlashes)
    {
        _lightSource.enabled = true;
        yield return new WaitForSeconds(timeBetweenFlashes);
        _lightSource.enabled = false;
    }
}
