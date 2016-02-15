﻿using UnityEngine;
using System;

/// <summary>
/// Script for handling health of units in combat
/// </summary>
/// Author: Jordan (GitHub: @skorlir)
/// 2/10/16
public class CombatHealth : MonoBehaviour {
    /// <summary>
    /// Public maximum HP
    /// </summary>
    public const int MAX_HP = 1;

    /// <summary>
    /// Public current HP
    /// </summary>
    public int hp = MAX_HP;

    /// <summary>
    /// Private struct for representing health change type
    /// </summary>
    // FUTURE(jordan): consider a HealthChangeType like 'boost' that can increase HP past MAX, for instance
    private enum HealthChangeType {
        Heal,
        Damage
    }

    /// <summary>
    /// Generalized private health diff calculation function
    /// </summary>
    /// <param name="changeType">HealthChangeType representing type of change</param>
    /// <param name="amount">Integer change</param>
    /// <returns>|change in HP|</returns>
    private int CalculateDeltaHealth (HealthChangeType changeType, int amount) {
        int diff = 0;

        switch (changeType) {
            case HealthChangeType.Damage:
                diff = hp + amount > MAX_HP ? MAX_HP - hp : amount;
                break;
            case HealthChangeType.Heal:
                diff = hp - amount < 0 ? 0 : amount;
                break;
            default:
                // NOTE(jordan): use UnityEngine's Debug, not System's Console.WriteLine
                Debug.Log(string.Format("<color=red>Error:</color> Unrecognized HealthChangeType: {0}", changeType));
                break;
        }

        return diff;
    }

    /// <summary>
    /// Damage this unit
    /// </summary>
    /// <param name="damageAmount">Integer amount of damage to do</param>
    public void Damage (int damageAmount) {
        int healthChange = CalculateDeltaHealth(HealthChangeType.Damage, damageAmount);

        hp -= healthChange;

        if (hp == 0) {
            Die();
        }
    }

    /// <summary>
    /// Heal this unit
    /// </summary>
    /// <param name="healAmount">Integer amount of healing to do</param>
    public void Heal (int healAmount) {
        int healthChange = CalculateDeltaHealth(HealthChangeType.Heal, healAmount);

        hp += healthChange;
    }

    /// <summary>
    /// Perform cleanup and any other pre-death actions, then destroy this gameObject
    /// </summary>
    private void Die () {
        // NOTE(jordan): this will later be more sophisticated; eg, add in an animation, maybe
        Destroy(gameObject);
    }
}
