using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed;
    public float lifeMax = 100f;
    public float life = 100f;
    public float rangoVision;
    public float anguloVision;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direccion = (player.transform.position - transform.position).normalized;
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        bool dentroAngulo = Vector3.Angle(transform.forward, direccion) < anguloVision;
        player.inSight = distancia < rangoVision;
        if (distancia < rangoVision)
        {
            if (dentroAngulo == true)
            {
                print("Chase");
                // player.inSight = true;
                transform.LookAt(player.transform);
                transform.position += transform.forward * Time.deltaTime * speed;
                if (player.stamina > 0)
                {
                    player.stamina -= Time.deltaTime;
                }
                else
                {
                    player.stamina = 0;
                }
            }
        }
        else
        {
            print("Normal");

        }


    }


    public void TakeDamage(int amountDamage)
    {
        life -= amountDamage;
        print("Damage");
        if (life <= 0)
        {
            print("Dead");
            player.inSight= false;
            Destroy(gameObject);
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