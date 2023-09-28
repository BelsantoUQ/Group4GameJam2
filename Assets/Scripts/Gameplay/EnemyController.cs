using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Animator animator;
    
    private Transform mainTurret;
    private bool isAttacking = false;
    private bool hiting = false;

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
        }else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (!hiting)
        {
            animator.SetTrigger("Attack_1");
            hiting = true;
            StartCoroutine(DeactivateAfterDelay(1.2f)); // Desactivar después de 0.2 segundos
        }
    }
    
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainTurret"))
        {
            // Detén el movimiento del enemigo cuando llegue
            moveSpeed = 0;
            // El enemigo está lo suficientemente cerca para atacar
            isAttacking = true;
        }
    }
}