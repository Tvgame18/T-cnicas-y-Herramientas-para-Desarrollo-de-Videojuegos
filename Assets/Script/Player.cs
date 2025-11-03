using UnityEngine;

public class Player : MonoBehaviour
{


    public float speed = 5.5f;
    public float sensibilidadMouse = 120f;
    public Rigidbody rb;
    public float fuerzaSalto = 5;
    public Transform domo;
    public float xRotation = 5.5f;
    public float staminaMax = 10f;
    private Vector3 direccion;
    public float stamina = 10f;
    private bool consumiendoStamina;
    public bool isGrounded;
    public float lifeMax = 100f;
    public float life = 100f;
    public bool inSight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        MirarMouse();
        Correr();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            }
        }

    }

    private void Correr()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 1.5f;
            consumiendoStamina = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (consumiendoStamina == true)
            {
                speed /= 1.5f;
                consumiendoStamina = false;
            }
        }

        if (consumiendoStamina == true)
        {
            if (direccion != Vector3.zero)
            {

                stamina -= Time.deltaTime;
                if (stamina <= 0)
                {
                    consumiendoStamina = false;
                    speed /= 1.5f;
                }
            }
        }
        else
        {
            if (inSight == false)
            {

                if (stamina < staminaMax)
                {
                    stamina += Time.deltaTime;
                }
                else
                {
                    stamina = staminaMax;
                }
            }
        }
    }

    private void MirarMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensibilidadMouse * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        float mouseY = Input.GetAxisRaw("Mouse Y") * sensibilidadMouse * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30, 25);//
        domo.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void Movimiento()
    {
        // transform.position += Vector3.forward * Time.deltaTime * speed;  <- EJE GLOBAL
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        direccion = transform.forward * vertical + transform.right * horizontal;
        direccion = Vector3.ClampMagnitude(direccion, 1);

        transform.position += direccion * Time.deltaTime * speed;// <- EJE LOCAL
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
