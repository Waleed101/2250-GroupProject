using UnityEngine;

// Unique file type to keep track of player selection between levels and scenes, as well as progression
[CreateAssetMenu(fileName = "Character Type", menuName ="New Character")]
public class CharacterInfo : ScriptableObject
{
    // Character generla info
    new public string name = "";
    public float health, strength, speed, relations;
    private bool _hatEnabled = false;
    
    // Get/Set methods for each variable
    public string GetName() { return name; } // Get method for name 
    public void SetName(string name) { this.name = name; } // Set method for name 
    public float GetHealth() { return health; } // Get method for health
    public void SetHealth(float health) { this.health = health; } // Set method for health
    public float GetStrength() { return strength; } // Get method for strength
    public void SetStrength(float strength) { this.strength = strength; } // Set method for strength
    public float GetSpeed() { return speed; } // Get method for speed 
    public void SetSpeed(float speed) { this.speed = speed; } // Set method for speed 
    public float GetRelations() { return relations; } // Get relations method
    public void SetRelations(float relations) { this.relations = relations; } // Set relations method 
    public bool GetIfHatEnabled() { return _hatEnabled; }
    public void EnableHat() { _hatEnabled = true; }
    public void DisableHat() { _hatEnabled = false; }

    // Easy way to set bandit to appropriate stats
    public void SetBandit()
    {
        SetName("Bandit");
        SetHealth(100);
        SetStrength(30);
        SetSpeed(1.5f);
        SetRelations(20);
        EnableHat();
    }

    // Set hero stats 
    public void SetHero()
    {
        SetName("Hero");
        SetHealth(100);
        SetStrength(20);
        SetSpeed(1f);
        SetRelations(80);
        DisableHat();
    }
}
