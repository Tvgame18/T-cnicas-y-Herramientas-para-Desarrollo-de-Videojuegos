using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform camara;
    void Start()
    {
        camara = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camara);
    }
}
