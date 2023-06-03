using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniterotationUp : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    private void Rotate()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Rotate();
    }
}
