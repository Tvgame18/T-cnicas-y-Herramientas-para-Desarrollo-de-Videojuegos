using UnityEngine;

public class ArmaParcial2 : MonoBehaviour
{
    public GameObject bala;
    public float cadencia = 1;
    public float tiempoDisparo;
    public int amountBullet = 15;
    public int[] cargadores = new int[2];
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
                if (amountBullet > 0)
                {
                    amountBullet--;
                    UIController.Instance.UpdateTextBullets(amountBullet);
                    Instantiate(bala, transform.position, transform.rotation);
                    tiempoDisparo = 0;
                }
            }
        }
        else
        {
            tiempoDisparo += Time.deltaTime;
        }
        if (amountBullet != 15)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                amountBullet = 15;
                UIController.Instance.UpdateTextBullets(amountBullet );
            }
        }
    }
}