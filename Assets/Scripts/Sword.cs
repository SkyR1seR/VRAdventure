using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Damage;

    [NonSerialized]public bool isMoved;

    Vector3 OldPosition;
    // Start is called before the first frame update
    void Start()
    {
        this.tag = "Sword";
        OldPosition = transform.position;
        StartCoroutine(CheckMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckMove()
    {
        while (true)
        {
            if (Vector3.Distance(OldPosition, transform.position) > 0.2f)
            {
                isMoved = true;
            }
            else
            {
                isMoved = false;
            }

            OldPosition = transform.position;


            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
