using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment
{
    public void Move(GameObject gameObject, float speed, Vector2 vector2)
    {
        gameObject.transform.position += (Vector3)vector2 * speed* Time.deltaTime;
    }
}
