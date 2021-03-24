using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of main enemy; unlike normal one it'll steal coins, has less health, and returns to the beacon after a bit of time
public class EnemyStealer : Enemy
{
    // Lower starting health
    public float startHealth = 50f;
    private Transform _currentTarget;
    public Transform spawnLocation;

    public override void Start()
    {
        // Get current spawn location
        spawnLocation = GameObject.FindGameObjectWithTag("Enemy Spawn").transform;
        
        // Set appropraite health and start
        base.Start();
        base.SetMaxHealth(startHealth);
        base.SetCurrentHealth(startHealth);

        // Current movement target is the player
        _currentTarget = player.transform;
    }

    // Overriddedn method to move
    public override void Move()
    {
        // Identical to the parent, just moves slightly faster
        Vector3 playerPos = _currentTarget.position;
        playerPos.x += xDir; playerPos.z += zDir;
        Vector3 pos = Vector3.MoveTowards(transform.position, playerPos, movementSpeed * Time.deltaTime * 1.5f);
        Vector3 diff = base.Absolute(transform.position) - base.Absolute(pos);
        diff.y = Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.GetPosition().y + 0.05f;
        transform.position = pos;
        transform.LookAt(_currentTarget);

        // Check to see if they've been they're for the allocated time
        if (base.GetTimeSinceEntry() > 5f)
        {
            // Set target to go back home
            _currentTarget = spawnLocation;

            // Turn on walking animation
            base.SetMotion(true);
        }
    }

    // Overriden affect player method
    public override void AffectPlayer()
    {
        // Check to see if they've taken time since their last kill
        if ((Time.time - timeSinceLast) > timeToNextKill)
        {
            // Nothing here now because they don't collect coins until level 2
            print("No coins to steal!!!");
        }
    }
}
