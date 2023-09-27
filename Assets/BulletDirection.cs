using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirection : MonoBehaviour
{
    private float rotationY = 0.0f;

    // Función para establecer la rotación en el eje Y
    public void SetRotationY(float yRotation)
    {
        rotationY = yRotation;
    }

    private void OnEnable()
    {
        // Aplica la rotación en el eje Y al objeto (balas, proyectiles, etc.) en este script
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

}