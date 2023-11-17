using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class Enemy : MonoBehaviour
{
    public int HP = 50;
    private int _raysCount=50;
    [SerializeField] private int _distance = 9;
    private float _angle = 45;
    [SerializeField] private Vector3 _offset;
    private Transform _target;
    private NavMeshAgent _ai;


    Vector3 OldPosition;

    Animator animator;

    private void Start()
    {
        _ai = GetComponent<NavMeshAgent>();
        _target = FindObjectOfType<Player>().transform;
        OldPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    public void Damage()
    {
        _target.GetComponent<PlayerController>().HP--;
    }
    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 position = transform.position + _offset;
        if (Physics.Raycast(position, dir, out hit, _distance))
        {
            if (hit.transform == _target)
            {
                result = true;
                Debug.DrawLine(position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(position, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(position, dir * _distance, Color.red);
        }
        return result;
    }

    bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;

        for (int i = 0; i < _raysCount; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += +_angle * Mathf.Deg2Rad / _raysCount;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir)) b = true;
            }
        }

        if (a || b) result = true;
        return result;
    }

    private void OnDrawGizmosSelected()
    {
        RayToScan();
    }

    bool attacked = false;

    IEnumerator godTimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isDamage", false);
        attacked = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && !attacked)
        {
            if (other.GetComponent<Sword>().isMoved)
            {
                attacked = true;
                StartCoroutine(godTimer());
                animator.SetBool("isDamage",true);
                HP -= other.GetComponent<Sword>().Damage;
                if (HP <= 0)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
    void Update()
    {
        if (OldPosition != transform.position)
        {
            animator.SetBool("isRun", true);
            OldPosition = transform.position;
        }
        else
        {
            animator.SetBool("isRun", false);
        }



        if (Vector3.Distance(transform.position, _target.position) < _distance)
        {
            if (RayToScan())
            {
                _ai.enabled = true;
                _ai.SetDestination(_target.position);

                if (_ai.remainingDistance < 2 && _ai.remainingDistance != 0)
                {

                    animator.SetBool("isAttack", true);
                }
                else
                {
                    animator.SetBool("isAttack", false);
                }
            }
            else
            {
                animator.SetBool("isAttack", false);
            }
        }
        
    }
}
