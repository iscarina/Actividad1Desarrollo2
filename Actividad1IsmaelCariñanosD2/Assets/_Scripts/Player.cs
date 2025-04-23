using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    private Animator anim;

    private Vector3 direccionMovimiento;
    private Vector3 direccionInput;
    private Vector3 velocidadVertical;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform camara;
    [SerializeField] private InputManagerSO inputManager;
    [SerializeField] private float factorGravedad;
    [SerializeField] private float alturaSalto;

    [Header("Detección suelo")]
    [SerializeField] private Transform pies;
    [SerializeField] private float radioDetection;
    [SerializeField] private LayerMask queEsSuelo;

    private void OnEnable()
    {
        inputManager.OnSaltar += Saltar;
        inputManager.OnMove += Mover;
    }

    //Solo se ejecutara cuando se actualize el input de movimiento
    private void Mover(Vector2 ctx)
    {
        direccionInput = new Vector3(ctx.x, 0, ctx.y);
    }

    private void Saltar()
    {
        if (EstoyEnSuelo())
        {
            velocidadVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaSalto);
            anim.SetTrigger("jump");
        } 
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        direccionMovimiento = camara.forward * direccionInput.z + camara.right * direccionInput.x;
        direccionMovimiento.y = 0;
        controller.Move(direccionMovimiento * velocidadMovimiento * Time.deltaTime);
        anim.SetFloat("velocidad", controller.velocity.magnitude);

        if (direccionMovimiento.sqrMagnitude > 0)
        {
            RotarHaciaDestino();
        }

        //Si hemos aterrizado
        if (EstoyEnSuelo() && velocidadVertical.y < 0)
        {
            velocidadVertical.y = 0; //Reseteo velocidad vertical
            anim.ResetTrigger("jump");
        }

        AplicarGravedad();

    }

    private void RotarHaciaDestino()
    {
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
        transform.rotation = rotacionObjetivo;
    }

    private bool EstoyEnSuelo()
    {
        return Physics.CheckSphere(pies.position, radioDetection, queEsSuelo);
    }

    private void AplicarGravedad()
    {
        velocidadVertical.y += factorGravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pies.position, radioDetection);
    }

}
