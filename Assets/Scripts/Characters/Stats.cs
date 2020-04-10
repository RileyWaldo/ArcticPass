using UnityEngine;

//namespace ArcticPass.Status
[CreateAssetMenu(fileName = "Stats", menuName = "ArcticPass/Create New Stat", order = 1)]
public class Stats : ScriptableObject
{
    [SerializeField] float health = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] float speed = 4f;

    public float GetHealth()
    {
        return health;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }
}