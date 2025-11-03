using UnityEngine;

public class Arma : MonoBehaviour
{
    public GameObject bala;
    public float cadencia = 1;
    public float tiempoDisparo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tiempoDisparo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempoDisparo >= cadencia)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bala, transform.position, transform.rotation);
                tiempoDisparo = 0;
            }
        }
        else
        {
            tiempoDisparo += Time.deltaTime;
        }
    }
}
