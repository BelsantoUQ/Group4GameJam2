using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRikayon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool hiting = false;
    private bool isAttacking = false;
    private bool isRunning = false;
    private int move = 2;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si el enemigo está atacando, llama a la función Attack().
        if (isAttacking)
        {
            Attack();
        }
        else
        {
            // Si no está atacando y no está corriendo y tiene movimiento establecido.
            if (!isRunning && move > 0)
            {
                // Activa una animación de caminata o sigilo dependiendo del valor de 'move'.
                if (move == 3)
                {
                    animator.SetTrigger("Sneak_Cycle_1");
                }
                else
                {
                    animator.SetTrigger("Walk_Cycle_" + move);
                }
                isRunning = true;
                StartCoroutine(DeactivateMoveAfterDelay(1.2f)); // Desactivar después de 0.2 segundos
            }
        }
    }
    
    private void Attack()
    {
        // Si el enemigo no está actualmente en el proceso de golpear, realiza un ataque.
        if (!hiting)
        {
            move = 0; // deja de ejecutar la animacion de caminar o correr
            animator.SetTrigger("Attack_" + Random.Range(1, 4));
            hiting = true;
            StartCoroutine(DeactivateHitAfterDelay(1.2f)); // Desactivar después de 1.2 segundos
        }
    }
    
    private IEnumerator DeactivateMoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isRunning = false;
    }
    
    private IEnumerator DeactivateHitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _gameManager.SetLifePoint(-10);
        hiting = false;
    }

    public void SetAttack(bool active)
    {
        // Activa o desactiva el modo de ataque del enemigo.
        isAttacking = active;
    }

    public void SetRunning(bool running)
    {
        // Configura si el enemigo está corriendo o no y cambia su estado de movimiento.
        move = running ? 1 : 2;
    }

    public void SetDie()
    {
        // Configura el estado del enemigo como muerto, desactivando la acción de ataque y movimiento.
        isAttacking = false;
        isRunning = false;
        move = 0;
        animator.SetTrigger("Die");
    }
    
    public void SetDamage()
    {
        // Configura el estado del enemigo como dañado, interrumpiendo cualquier acción actual y ejecutando una animación de daño.
        bool aux = isAttacking;
        isAttacking = false;
        hiting = true;
        isRunning = true;
        animator.SetTrigger("Take_Damage_" + Random.Range(1, 4));
        StartCoroutine(ReturnStateAfterDelay(.7f, aux)); // Desactivar después de 0.7 segundos
    }

    private IEnumerator ReturnStateAfterDelay(float delay, bool hitingAux)
    {
        yield return new WaitForSeconds(delay);
        hiting = false;
        isRunning = false;
        isAttacking = hitingAux;
    }
    
}
