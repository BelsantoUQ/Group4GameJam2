using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private GameObject enemyMovement;

    public void SetDamage(float multiplicator)
    {
        enemyMovement.GetComponent<EnemyMovemnt>().SetDamage(multiplicator);
    }
    
}
