using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private GameObject enemyMovement;
    private GameManager _gameManager;
    public void SetDamage(float multiplicator)
    {
        enemyMovement.GetComponent<EnemyMovemnt>().SetDamage(multiplicator);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ally"))
        {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            Debug.Log("Ally Catch");
            _gameManager.ChangeAllyText(FindObjectsOfType<NavigationAlly>().Length-1);
            enemyMovement.GetComponent<EnemyMovemnt>().ChangeTarget(other.gameObject);
            other.gameObject.GetComponent<NavigationAlly>().SetDeath(enemyMovement);
        }
    }
    
}
