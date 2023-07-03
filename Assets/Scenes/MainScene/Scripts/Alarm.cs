using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeGrowthIntensity;

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
        if (_volumeMoveCoroutine != null)
        {
            StopCoroutine(_volumeMoveCoroutine);
        }
        else
        {
            _audioSource.Play();
        }

        _volumeMoveCoroutine = StartCoroutine(MoveVolume(1));
    }

    private void StopAlarm(Transform player)
    {
        foreach (AlarmArea area in _areas)
        {
            if (area.Bounds.Contains(player.position))
                return;
        }

        if (_volumeMoveCoroutine != null)
        {
            StopCoroutine(_volumeMoveCoroutine);
        }

        _volumeMoveCoroutine = StartCoroutine(MoveVolume(0));
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
