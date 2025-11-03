using UnityEngine;

public class Bala : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public int damage;
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemigo enemyParcial1 = other.GetComponent<Enemigo>();
        if (enemyParcial1 != null)
        {
            enemyParcial1.TakeDamage(damage);
        }
        else
        {
            Soldier enemyParcial2 = other.GetComponent<Soldier>();
            if (enemyParcial2 != null)
            {
                enemyParcial2.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
