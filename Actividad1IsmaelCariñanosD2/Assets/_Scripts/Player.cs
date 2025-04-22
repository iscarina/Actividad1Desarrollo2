using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    private float inputH;
    private float inputV;

    private CharacterController controller;

    private Animator anim;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform camara;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        Vector3 dirMov = (camara.forward * inputV + camara.right * inputH).normalized;
        dirMov.y = 0;
        controller.Move(dirMov * velocidadMovimiento * Time.deltaTime);
        anim.SetFloat("velocidad", controller.velocity.magnitude);
        if (inputH != 0 || inputV != 0)
        {
            RotarHaciaDestino(dirMov);
        }
    }

    private void RotarHaciaDestino(Vector3 destino)
    {
        Quaternion rotacionObjetivo = Quaternion.LookRotation(destino);
        transform.rotation = rotacionObjetivo;
    }

}
