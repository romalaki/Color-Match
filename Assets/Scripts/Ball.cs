using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [Header("SO")]
    public Color_object[]  objectColors;
    
    [NonSerialized]public Color_object current;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        current = objectColors[Random.Range(0, objectColors.Length)];
        sr.sprite = current.sprite;
    }
    
    private void Update()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.y < -0.1f)
        {
            Destroy(gameObject);
        }
    }
    
}
