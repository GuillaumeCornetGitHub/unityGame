using UnityEngine;
using System.Collections;
using System;

public abstract class GameElement : MonoBehaviour, Damageable, Killable
{
    public float life;

  
    public void damage(float damageTaken)
    {
        life -= damageTaken;

        if (life <= 0) kill();
    
    }

    public abstract void kill();
    
}
