using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private GameObject enemyMovement;
    [SerializeField] private GameObject ally;

    public void SetDamage(float multiplicator)
    {
        enemyMovement.GetComponent<EnemyMovemnt>().SetDamage(multiplicator);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ally"))
        {
            Debug.Log("Ally Catch");
        }
    }
    
}
