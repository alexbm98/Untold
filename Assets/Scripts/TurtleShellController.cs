using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TurtleShellController : MonoBehaviour
{
    public static TurtleShellController Instance { get; private set; }

    public NavMeshAgent agent;
    public int vidas;
    private Transform player;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < enemies.Capacity; i++)
        {*/
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            vidas = 2;
            tag = "Enemy";
        //}
    }

    // Update is called once per frame
    void Update()
    {
        HandleIdle();
        HandleMovement();
        HandleDie();
    }

    void OnDestroy()
    {
        PlayerManager.Instance.enemigosDerrotados++;
    }

    public void HandleIdle()
    {
        //for (int i = 0; i < enemies.Capacity; i++)
        //{
            float distancia = Mathf.Sqrt(Mathf.Pow((transform.position.x - player.position.x), 2) + Mathf.Pow((transform.position.y - player.position.y), 2) + Mathf.Pow((transform.position.z - player.position.z), 2));

            if (distancia < 15)
            {
                animator.SetBool("jugadorCerca", true);
            }
            else
            {
                animator.SetBool("jugadorCerca", false);
            }
        //}
    }

    public static explicit operator TurtleShellController(GameObject v)
    {
        throw new NotImplementedException();
    }

    public void HandleMovement()
    {
        if (!animator.GetBool("muere"))
        {
            agent.destination = player.position;
            animator.SetBool("seMueve", true);
        }
    }

    public void HandleDie()
    {
        if (vidas <= 0)
        {
            animator.SetBool("muere", true);
            agent.enabled = false;
            Destroy(this.gameObject, 4);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject != null)
        {
            if (collision.CompareTag("Weapon") && PlayerManager.Instance.animatorP.GetBool("atacar01"))
            {
                vidas--;
                animator.SetBool("esGolpeado", true);
            }
            else
            {
                if (collision.CompareTag("Player") && !animator.GetBool("muere"))
                {
                    animator.SetBool("atacar", true);

                    if (!PlayerManager.Instance.animatorP.GetBool("defender"))
                    {
                        PlayerManager.Instance.currentHealth -= 10f;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (this.gameObject != null)
        {
            if (collision.CompareTag("Weapon"))
            {
                animator.SetBool("esGolpeado", false);
            }
            else
            {
                if (collision.CompareTag("Player"))
                {
                    animator.SetBool("atacar", false);
                }
            }
        }
    }
}
