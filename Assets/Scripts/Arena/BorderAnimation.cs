using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderAnimation : MonoBehaviour
{
    public float timeBetweenColorChange;
    public SpriteRenderer topBorder;
    public SpriteRenderer rightBorder;
    public SpriteRenderer leftBorder;
    public SpriteRenderer bottomBorder;
    void Start()
    {
        StartCoroutine(ColorChange(timeBetweenColorChange));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ColorChange (float timeBetween)
    {
        Color newColor = Random.ColorHSV();

        float elapsedTime = 0;
        Color initialColor = topBorder.color;

        while (elapsedTime < timeBetween)
        {
            topBorder.color = Color.Lerp(initialColor, newColor, elapsedTime / timeBetween);
            rightBorder.color = Color.Lerp(initialColor, newColor, elapsedTime / timeBetween);
            leftBorder.color = Color.Lerp(initialColor, newColor, elapsedTime / timeBetween);
            bottomBorder.color = Color.Lerp(initialColor, newColor, elapsedTime / timeBetween);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        topBorder.color = newColor;
        rightBorder.color = newColor;
        leftBorder.color = newColor;
        bottomBorder.color = newColor;

        yield return new WaitForEndOfFrame();

        StartCoroutine(ColorChange(timeBetweenColorChange));
    }
}
