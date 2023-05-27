using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Camera cam;
    public int multiplier = 2;

    private void Start()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        Debug.Log(bounds.extents);
        //transform.localScale = new Vector2(bounds.extents.x * multiplier, bounds.extents.y * multiplier);
    }
}

public static class CameraExtensions
{
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}