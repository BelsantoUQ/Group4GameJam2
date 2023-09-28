using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRikayon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool hiting = false;
    
    // Update is called once per frame
    void Update()
    {
        if (hiting)
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

    public void SetAttack(bool active)
    {
        hiting = active;
    }

}
