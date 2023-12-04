using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    float lerpDuration = 1f;
    float startValue = 1f;
    float endValue = 0;
    float delay = 0.5f;

    Color col = Color.black;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        Image img = GetComponent<Image>();
        yield return new WaitForSeconds(delay);
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            col.a = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            img.color = col;
            yield return null;
        }
        col.a = endValue;
        img.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
