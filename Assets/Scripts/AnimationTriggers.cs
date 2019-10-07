using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    public PlayerController player;

    public void CastSpell ()
    {
        player.CastMagic();
    }
}
