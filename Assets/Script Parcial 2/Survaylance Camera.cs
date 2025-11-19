using System.Collections;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Transform Visor;
    public Transform rotor;

    [Header("Parametros")]
    public float anguloVision;
    public float radioVision;
    public float velocidadGiro;
    

    public LayerMask layerDetectable;

    public bool playerVisible;
    public bool check;
   

    public float vidaCamara;



    // Update is called once per frame
    void Update()
    {

        Vector3 direccion = (player.position - transform.position).normalized;
        direccion.y = 0;

        bool dentroDistancia = Vector3.Distance(transform.position, player.position) < radioVision; 

        bool dentroAngulo = Vector3.Angle(rotor.forward, direccion) < anguloVision;

        bool objetoVisible = false;

        if (Physics.Raycast(rotor.position, direccion, out RaycastHit hit, radioVision, layerDetectable))
        {
            if (hit.transform == player)
            {
                print("Player a la vista!");
                objetoVisible = true;
                Debug.DrawRay(transform.position, direccion * radioVision, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, direccion * radioVision, Color.red);

            }
        }

        playerVisible = dentroDistancia && dentroAngulo && objetoVisible;

        if (playerVisible == true)
        {
            SeguirPlayer(direccion);
            if (Physics.Raycast(rotor.position, Visor.up, out RaycastHit hitCannon, radioVision, layerDetectable))
            {
                Debug.DrawRay(rotor.position, Visor.up * radioVision, Color.black);
                if (hitCannon.transform == player)
                {
                    Avisar();
                    
                }
            }
        }
        else
        {
            Patrulla();
        }
    }
    public void Avisar()
    {
        Soldier[] soldados = FindObjectsOfType<Soldier>();
        foreach (var item in soldados)
        {
            item.myState = State.Chase;
            item.iSawPlayer = true;
            print(item);
        }
    }


    public void Patrulla()
    {
        float y = rotor.eulerAngles.y;
        if (y > 180f) y -= 360f; // ← esta línea es la única “magia”: normaliza.

        if (!check)
        {
            rotor.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);
            if (y >= 60f) check = true;
        }
        else
        {
            rotor.Rotate(Vector3.up * -velocidadGiro * Time.deltaTime);
            if (y <= -60f) check = false;
        }
    }

    public void SeguirPlayer(Vector3 direccion)
    {
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion, Vector3.up);
        rotor.rotation = Quaternion.RotateTowards(rotor.rotation, rotacionObjetivo, velocidadGiro * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioVision);


        Vector3 anguloDerecha = Quaternion.Euler(0, anguloVision, 0) * rotor.forward;
        Vector3 anguloIzquierda = Quaternion.Euler(0, -anguloVision, 0) * rotor.forward;

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(rotor.position, rotor.position + anguloDerecha * radioVision);
        Gizmos.DrawLine(rotor.position, rotor.position + anguloIzquierda * radioVision);


    }
}
