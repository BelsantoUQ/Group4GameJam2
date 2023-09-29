using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField]

    private InputAction action;
    private Animator animator;
    private bool firstCamera = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        action.performed += _ => SwitchState();
    }

    private void SwitchState()
    {
        if (firstCamera)
        {
            animator.Play("SecondCamera");
        } else
        {
            animator.Play("FirstCamera");
        }

        firstCamera = !firstCamera;
    }
}
