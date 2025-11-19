using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI balas;
    public TextMeshProUGUI vida;
    public static UIController Instance;
    private void Start()
    {
        Instance = this;
    }
    public void UpdateTextLife(float life)
    {
        vida.text = "Vida:" + life + "/100"; //Vida:60/100
    }
    public void UpdateTextBullets(int amount)
    {
        balas.text = amount + "/15";
    }

}
