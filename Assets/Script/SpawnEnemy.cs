using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject prefabEnemigo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if(FindAnyObjectByType<Enemigo>()== null)
            {
                Instantiate(prefabEnemigo, transform.position, transform.rotation);
            } 
        }
    }
}
