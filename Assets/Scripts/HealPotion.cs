using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HealPotion : MonoBehaviour
{
    public int HealPoints = 2;
    // Start is called before the first frame update
    public void Heal()
    {
        FindObjectOfType<PlayerController>().HP += HealPoints;
        Destroy(gameObject);
    }
}
