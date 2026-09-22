using UnityEditor;
using UnityEngine;

public class Orbital : MonoBehaviour
{
    //Waypoints
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    //Movement settings
    [SerializeField] private float travelDuration = 3f;

    //Scoring
    [SerializeField] private int interceptScorValue = 10;
    [SerializeField] private int hazardPenaltyValue = 5;
    [SerializeField] private string hazardZoneTag = "HazardZone";

    private float progress = 0f;
    private bool movingToB = true;

    public void OnEnable()
    {
        Debug.Log($"[OrbitalTarget]{gameObject.name} enabled - drone is activated.");
    }

    public void OnDisable()
    {
        Debug.Log($"[OrbitalTarget]{gameObject.name} disabled - drone is deactivated.");
    }

    private void Update()
    {
        if (pointA == null || pointB == null)
        {
            return;
        }


        float step = Time.deltaTime / Mathf.Max(travelDuration, 0.0001f);
        progress += movingToB ? step : -step;
        progress = Mathf.Clamp01(progress);

        transform.position = Vector3.Lerp(pointA.position, pointB.position, progress);

        if (progress >= 1f || progress <= 0f)
        {
            movingToB = !movingToB;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"[OrbitalTarget]{gameObject.name} intercepted by click.");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(hazardZoneTag))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(-hazardPenaltyValue);
            }

            Debug.Log($"[OrbitalTarget]{gameObject.name} entered hazard zone:{other.name}");
        }
    }


}