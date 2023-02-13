using UnityEngine;

public class CameraOnDeathBehaviour : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.Translate(200 * Time.deltaTime * Vector3.right);
    }
}
