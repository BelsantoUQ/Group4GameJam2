using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private GameObject rikayon;
    [SerializeField] private GameObject enemyMovement;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainTurret"))
        {
            // Detén el movimiento del enemigo cuando llegue
            enemyMovement.GetComponent<EnemyMovemnt>().SetSpeed(0f);
            // El enemigo está lo suficientemente cerca para atacar
            enemyMovement.GetComponent<EnemyMovemnt>().SetAttacking(true);
            rikayon.GetComponent<EnemyRikayon>().SetAttack(true);
        }
        Debug.Log("Trigger");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colision");
        if (other.gameObject.CompareTag("MainTurret"))
        {
            Debug.Log("Colision");
            // Detén el movimiento del enemigo cuando llegue
            enemyMovement.GetComponent<EnemyMovemnt>().SetSpeed(0f);
            // El enemigo está lo suficientemente cerca para atacar
            enemyMovement.GetComponent<EnemyMovemnt>().SetAttacking(true);
            rikayon.GetComponent<EnemyRikayon>().SetAttack(true);
        }
    }
}