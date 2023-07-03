using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoorwayInteraction : MonoBehaviour
{
    [SerializeField] private Transform _rotatingDoor;
    [SerializeField] private Transform _entrancedFinder;

    [SerializeField] private float _duration = 1.0f;
    [Range(1, 180)][SerializeField] private float _openAngle = 90.0f;

    private void OnTriggerEnter(Collider other)
    {
        _entrancedFinder.transform.position = other.transform.position;

        if (_entrancedFinder.localPosition.z > 0)
            Open();
        else
            Open(isReversed: true);
    }

    private void OnTriggerExit(Collider other)
    {
        Close();
    }

    private void Open(bool isReversed = false)
    {
        float currentAngle = Quaternion.Angle(Quaternion.identity, _rotatingDoor.rotation);
        float openAngle = isReversed ? _openAngle * -1 : _openAngle;

        if (currentAngle == openAngle)
            return;

        _rotatingDoor.DOLocalRotate(new Vector3(_rotatingDoor.eulerAngles.x, openAngle, _rotatingDoor.eulerAngles.z), _duration);
    }

    private void Close()
    {
        float currentAngle = Quaternion.Angle(Quaternion.identity, _rotatingDoor.rotation);

        if (currentAngle == 0)
            return;

        _rotatingDoor.DOLocalRotate(new Vector3(_rotatingDoor.eulerAngles.x, 0, _rotatingDoor.eulerAngles.z), _duration);
    }

    // Chad option

    //[SerializeField] private AnimationCurve _animationCurve;
    //private Coroutine _rotateCoroutine;

    //private IEnumerator Rotate(float start, float end)
    //{
    //    Vector3 doorScale = _rotatingDoor.sc

    //    for (float i = 0; i < 1; i += Time.deltaTime / _duration)
    //    {
    //        _rotatingDoor.rotation = Quaternion.Lerp(
    //            Quaternion.Euler(_rotatingDoor.eulerAngles.x, _rotatingDoor.eulerAngles.y, start),
    //            Quaternion.Euler(_rotatingDoor.eulerAngles.x, _rotatingDoor.eulerAngles.y, end),
    //            _animationCurve.Evaluate(i));

    //        yield return null;
    //    }

    //    _rotatingDoor.rotation = Quaternion.Euler(_rotatingDoor.eulerAngles.x, _rotatingDoor.eulerAngles.y, end);
    //    _rotateCoroutine = null;
    //}
}