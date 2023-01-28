using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    private NavMeshAgent nmagent;
    private Transform t;
    public GameObject player;
    [SerializeField] private GameObject face;
    public float mobDistanceRun = 15.0f;
    public float mobDistanceAttack = 3.0f;
    private float rotationSpeed = 5f; // adjust this value to control how fast the enemy rotates
    [SerializeField] private GameObject attackModel;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (attackModel == null) attackModel = gameObject;
        nmagent = GetComponent<NavMeshAgent>();
        t = transform;
        anim = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            if (face != null) face.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("/Interactibles/Zombie/HappyFace.png");
            return;
        }

        float playerDistance = Vector3.Distance(t.position, player.transform.position);

        // Run

        if (playerDistance <= mobDistanceRun)
        {
            Vector3 dirToPlayer = t.position - player.transform.position;
            Vector3 destination = t.position - dirToPlayer;
            // Vector3 direction = HealthSystem.player.transform.position - t.position;
            nmagent.SetDestination(destination);
        }

        if (Vector3.Distance(player.transform.position, t.position) < mobDistanceAttack)
        {
            nmagent.isStopped = true;
        }
        else
        {
            nmagent.isStopped = false;
        }

        // Attack

        if (playerDistance <= mobDistanceAttack)
        {
            StartCoroutine(AttackLoop());
        }
        else
        {
            StopCoroutine(AttackLoop());
            anim.StopPlayback();
        }

        t.eulerAngles = new Vector3(0, t.eulerAngles.y, t.eulerAngles.z);
    }

    IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(1);
        attackModel.GetComponent<AttackingScript>().Fire();
    }
}
