using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Dust : MonoBehaviour
{
    public GameObject player;
    private bool isWalking
    {
        get
        {
            newPos = transform.position;  // each frame track the new position
            velocity = (newPos - prevPos) / Time.fixedDeltaTime;  // velocity = dist/time
            prevPos = newPos;  // update position for next frame calculation

            return (velocity.x > 0 || velocity.x < 0) || (velocity.z > 0 || velocity.z < 0);
        }
    }
    private ParticleSystem ps;
    Vector3 prevPos;
    Vector3 newPos;
    Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log($"isWalking = {isWalking}, velocity = {velocity}");
        if (isWalking)
        {
            ps.Play();
        }
    }
}