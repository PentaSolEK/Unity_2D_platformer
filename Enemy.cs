using UnityEngine;
using UnityEngine.Events;
public class Monsters : Counter
{
    [SerializeField] protected int lives = 3;

    public UnityEvent Killed;

    public virtual void GetDamage()
    {
        lives --;
        if (lives < 1)
        {
            Die();
        }
    }


    public virtual void Die()
    {
        Destroy(this.gameObject);
        Killed?.Invoke();
    }


}
