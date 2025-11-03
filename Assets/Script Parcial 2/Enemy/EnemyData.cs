using UnityEngine;

[CreateAssetMenu(fileName = "NuevoEnemigo", menuName = "Datos/Enemigo")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public float lifeMax;
    public float life;
    public float rangoVision;
    public float anguloVision;
    public State enemyState;

    
}
public enum State
{
    Normal,
    Chase,
    Damage,
    Dead
}