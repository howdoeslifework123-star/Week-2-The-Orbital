using UnityEngine;

public class TurretMechanics : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Alignment Verification")]
    [SerializeField] private float alignmentThreshold = 0.98f; 

    
    private void Update()
    {
        if (target == null) return;

       
        Vector3 directionToTarget = (target.position - transform.position).normalized;

       
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

       
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        
        float alignment = Vector3.Dot(transform.forward, directionToTarget);
        bool isLockedOn = alignment > alignmentThreshold;

        Debug.DrawLine(transform.position, target.position, isLockedOn ? Color.red : Color.yellow);

        if (isLockedOn)
        {
            Debug.Log($"[TurretTracker] Target lock confirmed on {target.name} (dot = {alignment:F3})");
        }
    }
}