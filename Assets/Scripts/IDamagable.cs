using UnityEngine;

public interface IDamagable
{
    int life { get; set;}
    void ApplyDamage(int damage);
}
