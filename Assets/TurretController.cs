using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 5.0f; // Velocidad de rotación de la torreta
    public float minRotation = 0.0f;   // Rotación mínima permitida (en grados)
    public float maxRotation = 160.0f; // Rotación máxima permitida (en grados)
    public BulletDirection bulletDirectionScript; // Referencia al script bulletDirection

    private bool isMouseDown = false;

    void Update()
    {
        // Obtén la posición actual del mouse en pantalla
        Vector3 mousePosition = Input.mousePosition;

        // Convierte la posición del mouse en un rayo desde la cámara
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Declara una variable para almacenar la rotación deseada
        Quaternion desiredRotation;

        // Lanza un rayo desde la cámara y obtén la rotación deseada
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 turretPosition = transform.position;
            Vector3 direction = targetPosition - turretPosition;

            // Calcula la rotación deseada basada en la dirección del rayo
            desiredRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Si no se golpea nada, la rotación deseada es la rotación actual
            desiredRotation = transform.rotation;
        }

        // Limita la rotación en el eje X y Z
        desiredRotation.eulerAngles = new Vector3(0, desiredRotation.eulerAngles.y, 0);

        // Aplica la rotación suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        // Limita la rotación en el eje Y entre minRotation y maxRotation
        float clampedYRotation = Mathf.Clamp(transform.eulerAngles.y, minRotation, maxRotation);
        transform.eulerAngles = new Vector3(0, clampedYRotation, 0);

        // Detecta el clic del mouse
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        // Si se mantiene presionado el clic del mouse, envía la rotación al script bulletDirection
        if (isMouseDown)
        {
            bulletDirectionScript.SetRotationY(transform.eulerAngles.y+10);
            bulletDirectionScript.gameObject.SetActive(true);
            StartCoroutine(DeactivateAfterDelay(.5f));
        }
    }
    
    
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletDirectionScript.gameObject.SetActive(false);
    }
}
