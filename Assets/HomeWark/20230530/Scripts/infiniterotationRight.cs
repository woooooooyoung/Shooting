using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniterotationRight : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    private void Rotate()
    {
        transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Rotate();
    }
}
