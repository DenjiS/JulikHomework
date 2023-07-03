using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AlarmArea : MonoBehaviour
{
    public UnityAction Entered;
    public UnityAction<Transform> Exited;

    private Collider _collider;

    public Bounds Bounds => _collider.bounds;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Entered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        Exited?.Invoke(other.transform);
    }
}
