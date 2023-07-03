using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeGrowthIntensity;

    private readonly float _maxVolume = 1;
    private readonly float _minVolume = 0;

    private AlarmArea[] _areas;
    private AudioSource _audioSource;

    private Coroutine _volumeMoveCoroutine;

    private void Start()
    {
        _areas = GetComponentsInChildren<AlarmArea>();

        foreach (AlarmArea area in _areas)
        {
            area.Entered += StartAlarm;
            area.Exited += StopAlarm;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void StartAlarm()
    {
        if (TryStopPreviousCoroutine() == false)
        {
            _audioSource.Play();
        }

        _volumeMoveCoroutine = StartCoroutine(MoveVolume(_maxVolume));
    }

    private void StopAlarm(Transform player)
    {
        if (_areas.Any(area => area.Bounds.Contains(player.position)))
                return;

        TryStopPreviousCoroutine();

        _volumeMoveCoroutine = StartCoroutine(MoveVolume(_minVolume));
    }

    private bool TryStopPreviousCoroutine()
    {
        if (_volumeMoveCoroutine != null)
        {
            StopCoroutine(_volumeMoveCoroutine);
            return true;
        }

        return false;
    }

    private IEnumerator MoveVolume(float toValue)
    {
        while (_audioSource.volume != toValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, toValue, _volumeGrowthIntensity * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == 0)
        {
            _audioSource.Stop();
        }

        _volumeMoveCoroutine = null;
    }
}
