using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
  public IEnumerator FadeTo(GameObject gameObj,float alphaValue,float alphaTime)
    {
        float alpha = gameObj.guiTexture.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / alphaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, alphaValue, t));
            gameObj.guiTexture.color = newColor;
            print("f " + t.ToString());
            yield return null;
        }
    }
}
