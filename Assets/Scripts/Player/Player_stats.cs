using UnityEngine;

public class Player_stats : MonoBehaviour
{
    public int credits;
    public int health;

    public int zombie_damage;

    public void spendCredits(int credits)
    {
        if(credits > this.credits)
        {
            print("No alcanza");
            return;
        }
        this.credits -= credits;
    }
    
    public void earnCredits(int credits)
    {
        this.credits += credits;
    }
    
    public void getDamage()
    {
        if(zombie_damage > health)
        {
            Die();
            health = 0;
            return;
        }
        health -= zombie_damage;
    }
    private void Die(){
        print("Player death");
    }
}
