using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManagerSO", menuName = "Scriptable Objects/InputManagerSO")]
public class InputManagerSO : ScriptableObject
{

    Controls misControles;

    public event Action OnSaltar;
    public event Action<Vector2> OnMove;

    private void OnEnable()
    {
        misControles = new Controls();
        misControles.Gameplay.Enable();

        misControles.Gameplay.Saltar.started += Saltar;
        misControles.Gameplay.Mover.performed += Mover;
        misControles.Gameplay.Mover.canceled += Mover;
    }

    private void Mover(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void Saltar(InputAction.CallbackContext ctx)
    {
        OnSaltar?.Invoke();
    }
}
