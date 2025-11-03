using UnityEngine;

public class PlayerParcial2 : MonoBehaviour
{


    public float speed = 5.5f;
    private float initSpeed = 5.5f;
    public float sensibilidadMouse = 120f;
    public Transform domo;
    public float xRotation;
    private Vector3 direccion;
    public float lifeMax = 100f;
    public float life = 100f;
    public bool inSight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        MirarMouse();
        Agachar();
       domo.transform.position = transform.position;

    }

   
    private void MirarMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensibilidadMouse * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

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
    private void Agachar()
    {
        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 tamañoActual= transform.localScale;
            tamañoActual.y = 0.5f;
            speed = initSpeed * 0.25f;
            transform.localScale = tamañoActual;
        }
        if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            Vector3 tamañoActual = transform.localScale;
            tamañoActual.y = 1f;
            speed = initSpeed;
            transform.localScale = tamañoActual;
        }
    }
    


   
}
