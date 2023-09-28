using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovemnt : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    
    private Transform mainTurret;
    private bool isAttacking = false;

    private void Start()
    {
        mainTurret = GameObject.Find("MainTurret").transform;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            // Calcula la dirección hacia la MainTurret
            Vector3 moveDirection = (mainTurret.position - transform.position).normalized;

            // Rota hacia la dirección del movimiento
            transform.LookAt(mainTurret);

            // Mueve al enemigo hacia la MainTurret
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void SetSpeed(float proviedSpeed)
    {
        moveSpeed = proviedSpeed;
    }
    public void SetAttacking(bool active)
    {
        isAttacking = active;
    }
}
