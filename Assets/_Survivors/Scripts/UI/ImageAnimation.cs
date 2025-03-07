using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float interval = 1f;

    public List<Sprite> sprites;

    int currentIndex = 0;

    WaitForSeconds wait;

    void Start()
    {
        if (image)
            image = GetComponent<Image>();

        image.sprite = sprites[currentIndex];

        wait = new WaitForSeconds(interval);

        StartCoroutine(AnimateRoutine());
    }

    IEnumerator AnimateRoutine()
    {
        while (true)
        {
            yield return wait;

            currentIndex = (currentIndex + 1) % sprites.Count;
            image.sprite = sprites[currentIndex];
        }
    }
}
