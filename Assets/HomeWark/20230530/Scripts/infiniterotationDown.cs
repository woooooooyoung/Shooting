using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniterotationDown : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    private void Rotate()
    {
        transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Rotate();
    }
}
