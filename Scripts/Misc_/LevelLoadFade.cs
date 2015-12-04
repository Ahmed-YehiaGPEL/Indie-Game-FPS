using UnityEngine;
using System.Collections;

public class LevelLoadFade : MonoBehaviour {

public static void FadeAndLoadLevel (string level, Texture2D fadeTexture,float fadeLength)
{
	if (fadeTexture == null)
		FadeAndLoadLevel(level, Color.white, fadeLength);

	GameObject fade = new GameObject("Fade");
    fade.AddComponent<LevelLoadFade>();
	fade.AddComponent<GUITexture>();
	fade.transform.position = new Vector3 (0.5f, 0.5f, 1000f);
	fade.guiTexture.texture = fadeTexture;
    
    fade.GetComponent<LevelLoadFade>().DoFade(level, fadeLength, false);
}

public static void FadeAndLoadLevel (string level, Color color , float fadeLength)
{
	var fadeTexture = new Texture2D (1, 1);
	fadeTexture.SetPixel(0, 0, color);
	fadeTexture.Apply();
	
	GameObject fade = new GameObject ("Fade");
	fade.AddComponent<LevelLoadFade>();
	fade.AddComponent<GUITexture>();
	fade.transform.position = new Vector3 (0.5f, 0.5f, 1000);
	fade.guiTexture.texture = fadeTexture;

	DontDestroyOnLoad(fadeTexture);
	fade.GetComponent<LevelLoadFade>().DoFade(level, fadeLength, true);
}
IEnumerator AS(float time,float fadeLength)
{
    while (time < fadeLength)
    {
        time += Time.deltaTime;
        Color temp2 = guiTexture.color;
        temp2.a = Mathf.InverseLerp(0.0f, fadeLength, (float)time);
        guiTexture.color = temp2;
        yield return null;
    }
}
IEnumerator ASS(float time, float fadeLength)
{
    while (time < fadeLength)
    {
        time += Time.deltaTime;
        Color temp2 = guiTexture.color;
        temp2.a = Mathf.InverseLerp(fadeLength, 0.0f, (float)time);
        guiTexture.color = temp2;
        yield return null;
    }
}
void DoFade (string level, float fadeLength, bool destroyTexture )
{
	// Dont destroy the fade game object during level load
	DontDestroyOnLoad(gameObject);

	// Fadeout to start with
    Color temp1 = guiTexture.color;
    temp1.a = 0.0f;
    guiTexture.color = temp1;

	// Fade texture in
	var time = 0.0;

    //while (time < fadeLength)
    //{
    //    time += Time.deltaTime;
    //    Color temp2 = guiTexture.color;
    //    temp2.a = Mathf.InverseLerp(0.0f, fadeLength, (float)time);
    //    guiTexture.color = temp2;
        
    //}
    AS((float)time, fadeLength);
    Color temp3 = guiTexture.color;
    temp3.a = 1;
    guiTexture.color = temp3;

	// Complete the fade out (Load a level or reset player position)
	Application.LoadLevel(level);
	
	// Fade texture out
	time = 0.0;
    //while (time < fadeLength)
    //{
    //    time += Time.deltaTime;
    //    Color temp4 = guiTexture.color;
    //    temp4.a = Mathf.InverseLerp(fadeLength, 0.0f, (float)time);
    //    guiTexture.color = temp4;
    //}
    ASS((float)time, fadeLength);
    Color temp5 = guiTexture.color;
    temp5.a = 0.0f;
    guiTexture.color = temp5;

	Destroy (gameObject);

	// If we created the texture from code we used DontDestroyOnLoad,
	// which means we have to clean it up manually to avoid leaks
	if (destroyTexture)
		Destroy (guiTexture.texture);
}
}
