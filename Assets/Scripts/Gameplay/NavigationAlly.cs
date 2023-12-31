using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
    private bool secondTouch;
    private GameObject enemyTarget;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Animator presidentAnimator;
    [SerializeField] private GameObject explosion;
    private int order;
    private enum AllyOptions { MisterOne, MisterTwo, MisterTree, MisterFour}
    [SerializeField]
    private AllyOptions selectedAlly;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        secondTouch = false;
        SetPosition();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        destination = originalPosition;
        isMovingToPowerUp = false;
        isMovingToDie = false;
        powerUpCatched = false;
        SetIdle();
    }

    public void SetIdle()
    {
        DeactiveAnimations();
        animator.SetBool("Combat"+Random.Range(1,3), true);
    }

    public void DeactiveAnimations()
    {
        // Obtén todos los parámetros del Animator
        AnimatorControllerParameter[] parameters = animator.parameters;

        // Recorre los parámetros y establece los booleanos en falso
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    public void DeactivePresidentAnimations()
    {
        // Obtén todos los parámetros del Animator
        AnimatorControllerParameter[] parameters = presidentAnimator.parameters;

        // Recorre los parámetros y establece los booleanos en falso
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                presidentAnimator.SetBool(parameter.name, false);
            }
        }
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
            
            if (_gameManager.GetAblePowerUp() && Input.GetMouseButtonDown(1) && !isMovingToPowerUp) // Verificar si se presionó el botón derecho del mouse
            {
                DeactiveAnimations();
                DeactivePresidentAnimations();
                presidentAnimator.SetBool("Order", true);
                StartCoroutine(PresidentAnimation());
                animator.SetBool("Running", true);
                isMovingToPowerUp = true;
                powerUp = GameObject.FindGameObjectWithTag("Powerup").transform;
                destination = powerUp.position;
                _gameManager.SetAblePowerUp(false);
            }
            if (isMovingToPowerUp && powerUpCatched)
            {
                if (transform.position.x <= 499)
                {
                    isMovingToPowerUp = false;
                    powerUpCatched = false;
                    SetIdle();
                }
            }
//            Debug.Log("Position Agent"+transform.position.x);
            
        }
    }
    private IEnumerator PresidentAnimation()
    {
        yield return new WaitForSeconds(.8f); // Esperar 2 segundos
        DeactivePresidentAnimations();
        presidentAnimator.SetBool("RestIdle"+Random.Range(1,3), true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            DeactiveAnimations();
            animator.SetBool("Pick"+Random.Range(1,3), true);
            DeactivePresidentAnimations();
            presidentAnimator.SetBool("Talk", true);
            _gameManager.SetPowerUp();
            Destroy(other.gameObject);
            StartCoroutine(MoveToBase());
        }
    }

    public void KillThisHuman()
    {
        DeactiveAnimations();
        agent.destination = transform.position;
        animator.SetBool("Death"+Random.Range(1,3), true);
//        Debug.Log("Animacion de muerte");
        explosion.SetActive(true);
        StartCoroutine(DestroyAfterDelay());
    }
    
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1.7f); // Esperar 2 segundos
//        Debug.Log("Ally Catch");
        _gameManager.ChangeAllyText(order-1);
        Destroy(gameObject);
    }

    private IEnumerator MoveToBase()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos
        DeactiveAnimations();
        DeactivePresidentAnimations();
        presidentAnimator.SetBool("RestIdle"+Random.Range(1,3), true);
        animator.SetBool("Running", true);
        powerUpCatched = true;
        destination = originalPosition;
    }

    public void SetDeath(GameObject enemy)
    {
        isMovingToDie = true;
        enemyTarget = enemy;
//        Debug.Log("Ally will Die");
        Vector3 newTarget = enemy.transform.position;
        destination = newTarget;
    }
}
