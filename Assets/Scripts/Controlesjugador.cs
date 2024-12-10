using UnityEngine;

public class Controlesjugador : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;  //[SerializeFIeld] se usa para mostrar en Unity las variables privadas.
    [SerializeField] private float sensibilidadMouse = 100f; // Sensibilidad para la rotación del ratón
    [SerializeField] private float gravedad = 9.8f; // Intensidad de la gravedad
    [SerializeField] private float salto = 2f; // Fuerza de salto

    private CharacterController characterController;
    private float rotacionY = 0f; // Control rotación eje Y
    private Vector3 velocidadVertical; // Control velocidad eje Y (gravedad y salto)

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        // Rotación con el mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        rotacionY += mouseX; // Acumula rotación eje Y
        transform.rotation = Quaternion.Euler(0f, rotacionY, 0f); // Aplica la rotación al jugador

        // Movimiento con teclado relativo a la rotación del jugador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimiento = (transform.right * horizontal + transform.forward * vertical).normalized;
        movimiento *= velocidad; // Escala velocidad

        // Gravedad y salto
        if (characterController.isGrounded)
        {
            velocidadVertical.y = -0.5f; // Pequeño empuje hacia abajo para mantener el contacto con el suelo

            if (Input.GetButtonDown("Jump"))
            {
                velocidadVertical.y = Mathf.Sqrt(salto * 2f * gravedad); // Fórmula para calcular la fuerza de salto
            }
        }
        else
        {
            velocidadVertical.y -= gravedad * Time.deltaTime; // Aplica gravedad mientras está en el aire, TimedeltaTime usa los FPS
        }

        // Combina movimiento horizontal y vertical
        Vector3 movimientoFinal = movimiento + velocidadVertical;
        characterController.Move(movimientoFinal * Time.deltaTime); // Mueve al jugador

            void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Salida"))
            {
                Debug.Log("¡Enhorabuena! Has salido con éxito."); // Muestra un mensaje en la consola
                                                                  // Aquí puedes añadir lógica adicional para mostrar un mensaje en pantalla o cambiar de escena.
            }
        }
        } 
}
