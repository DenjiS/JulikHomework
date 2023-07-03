using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AlarmArea : MonoBehaviour
{
    private Collider _collider;

    public event UnityAction Entered;
    public event UnityAction<Transform> Exited;

    public Bounds Bounds => _collider.bounds;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thiefComponent))
            Entered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thiefComponent))
            Exited?.Invoke(other.transform);
    }
}
