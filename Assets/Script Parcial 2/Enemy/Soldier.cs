using UnityEngine;
using TMPro;

public class Soldier : MonoBehaviour
{
    public float speed;
    public float lifeMax = 100f;
    public float life = 100f;
    public float rangoVision;
    public LayerMask layerDetectable;
    public float anguloVision;
    public State myState;
    public PlayerParcial2 player;
    public EnemyData data;
    public float velocidadGiro;
    public TextMeshPro textoCabeza;
    public bool iSawPlayer;
    float damageTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        player = FindAnyObjectByType<PlayerParcial2>();
        speed = data.speed;
        lifeMax = data.lifeMax;
        life = data.life;
        rangoVision = data.rangoVision;
        anguloVision = data.anguloVision;
        myState = data.enemyState;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (damageTime <= 0)
        {
            textoCabeza.text = myState.ToString();
        }
        else
        {
            damageTime -= Time.deltaTime;
        }
        Vector3 direccion = (player.transform.position+Vector3.up - transform.position).normalized;
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        bool dentroAngulo = Vector3.Angle(transform.forward, direccion) < anguloVision;

        bool objetoVisible = false;
        if (iSawPlayer == true)
        {
            myState = State.Chase;
            SeguirPlayer(direccion);
            transform.position += transform.forward * Time.deltaTime * speed;
            return;

        }
        else
        {
            print("Normal");
        }
        if (Physics.Raycast(transform.position, direccion, out RaycastHit hit, rangoVision, layerDetectable))
        {
            if (hit.transform.gameObject == player.gameObject)
            {
                print("Player a la vista!");
                objetoVisible = true;
                Debug.DrawRay(transform.position, direccion * rangoVision, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, direccion * rangoVision, Color.red);

            }
        }

        player.inSight = distancia < rangoVision;
        if (distancia < rangoVision)
        {
            if (dentroAngulo == true)

            {
                if (objetoVisible == true)
                {
                    print("Chase");
                    iSawPlayer = true;
                    // player.inSight = true;
                    myState = State.Chase;
                    SeguirPlayer(direccion);
                    //transform.LookAt(player.transform);
                    transform.position += transform.forward * Time.deltaTime * speed;
                }
            }
        }
       


    }

    public void SeguirPlayer(Vector3 direccion)
    {
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadGiro * Time.deltaTime);
    }
    public void TakeDamage(int amountDamage)
    {
        life -= amountDamage;
        print("Damage");
        damageTime = 2;
        myState = State.Damage;
        textoCabeza.text = myState.ToString();
        if (life <= 0)
        {
            print("Dead");
            myState = State.Dead;
            textoCabeza.text = myState.ToString();
            player.inSight = false;
            Destroy(gameObject,2);
            this.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoVision);

        Vector3 anguloDerecha = Quaternion.Euler(0, anguloVision, 0) * transform.forward;
        Vector3 anguloIzquierda = Quaternion.Euler(0, -anguloVision, 0) * transform.forward;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + anguloDerecha * rangoVision);
        Gizmos.DrawLine(transform.position, transform.position + anguloIzquierda * rangoVision);

    }
}
