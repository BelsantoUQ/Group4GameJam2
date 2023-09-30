using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovemnt : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float attackPosition = 123f;
    [SerializeField] private GameObject rikayon;
    [SerializeField] private bool running;
    [SerializeField] private float lifePoints;
    private float auxSpeed;
    private Transform mainTurret;
    private bool isAttacking = false;
    

    private void Start()
    {
        lifePoints = 100f;
        mainTurret = GameObject.Find("MainTurret").transform;
        rikayon.GetComponent<EnemyRikayon>().SetRunning(running);
        auxSpeed = moveSpeed;
    }

    private void Update()
    {
        if (lifePoints>0 && !isAttacking && transform.position.x > attackPosition)
        {
            // Calcula la dirección hacia la MainTurret
            Vector3 moveDirection = (mainTurret.position - transform.position).normalized;

            // Rota hacia la dirección del movimiento
            transform.LookAt(mainTurret);

            // Mueve al enemigo hacia la MainTurret
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        if (lifePoints>0 && transform.position.x <= attackPosition)
        {
            rikayon.GetComponent<EnemyRikayon>().SetAttack(true);
            moveSpeed = 0;
            //Debug.Log("Start Attack");
            isAttacking = true;
        }
    }

    public void SetDamage(float damageMultip)
    {
        lifePoints -= (5*(damageMultip));

        if (lifePoints<1)
        {
            rikayon.GetComponent<EnemyRikayon>().SetDie();
            StartCoroutine(DestroyAfterDelay(1.2f)); 
        }
        else
        {
            rikayon.GetComponent<EnemyRikayon>().SetDamage();
            moveSpeed = 0;
            StartCoroutine(ReturnStateAfterDelay(.7f));
        }
    }
    
    private IEnumerator ReturnStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
       // Debug.Log("Speed: "+auxSpeed);
        moveSpeed = auxSpeed;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
    public void SetSpeed(float providedSpeed)
    {
        moveSpeed = providedSpeed;
    }
    
    public void SetAttacking(bool active)
    {
        isAttacking = active;
    }
}
