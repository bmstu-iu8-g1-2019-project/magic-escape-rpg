using UnityEngine;
using System.Collections;

public class BasicCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    private Vector3 targetPos;
    [SerializeField] private float moveSpeed;
    void Update()
    {
        if (followTarget != null)
        {
            targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
            Vector3 velocity = (targetPos - transform.position) * moveSpeed;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.5f, Time.deltaTime);
        }
    }
}

