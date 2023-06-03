using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniterotationLeft : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    private void Rotate()
    {
        transform.Rotate(Vector3.left, turnSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Rotate();
    }
}
