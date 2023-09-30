using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationAlly : MonoBehaviour
{
    private Transform powerUp;
    private NavMeshAgent agent;
    private Vector3 destination;
    private Vector3 originalPosition;
    private bool isMovingToPowerUp;
    private bool powerUpCatched;
    private bool isMovingToDie;
    private bool isAbleToMove;
    private GameObject enemyTarget;
    
    [SerializeField] private Animator animator;
    private int order;
    private enum AllyOptions { MisterOne, MisterTwo, MisterTree, MisterFour}
    [SerializeField]
    private AllyOptions selectedAlly;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        SetPosition();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        destination = originalPosition;
        isMovingToPowerUp = false;
        isMovingToDie = false;
        powerUpCatched = false;
    }

    private void SetPosition()
    {
        switch (selectedAlly)
        {
            case AllyOptions.MisterOne:
                order = 4;
                break;
            case AllyOptions.MisterTwo:
                order = 3;
                break;
            case AllyOptions.MisterTree:
                order = 2;
                break;
            case AllyOptions.MisterFour:
                order = 1;
                break;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        agent.destination = destination;
        isAbleToMove = order == FindObjectsOfType<NavigationAlly>().Length;
        if (isAbleToMove)
        {
            if (Input.GetMouseButtonDown(1) && !isMovingToPowerUp) // Verificar si se presionó el botón derecho del mouse
            {
                animator.SetBool("Running", true);
                isMovingToPowerUp = true;
                powerUp = GameObject.FindGameObjectWithTag("Powerup").transform;
                destination = powerUp.position;
            }
            if (isMovingToPowerUp && powerUpCatched)
            {
                if (transform.position.x <= 499)
                {
                    isMovingToPowerUp = false;
                    powerUpCatched = false;
                }
            }
//            Debug.Log("Position Agent"+transform.position.x);
        }

        if (isMovingToDie && !enemyTarget)
        {
            isMovingToDie = false;
            destination = originalPosition;
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StartCoroutine(MoveToBase());
        }
        if (other.CompareTag("Enemy") && isMovingToDie)
        {
            animator.SetBool("Death", true);
            Debug.Log("Be Destoy");
            StartCoroutine(DestroyAfterDelay());
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && isMovingToDie)
        {
            animator.SetBool("Death", true);
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1.3f); // Esperar 2 segundos
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
//        Debug.Log("Ally Catch");
        _gameManager.ChangeAllyText(FindObjectsOfType<NavigationAlly>().Length-1);
        Destroy(gameObject);
    }

    private IEnumerator MoveToBase()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos
        powerUpCatched = true;
        destination = originalPosition;
    }

    public void SetDeath(GameObject enemy)
    {
        isMovingToDie = true;
        enemyTarget = enemy;
//        Debug.Log("Ally will Die");
        destination = enemy.transform.position;
    }
}
