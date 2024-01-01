using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    [SerializeField] private Gradient _dirLightGradient;
    [SerializeField] private Gradient _ambLightGradient;
    [SerializeField, Range(1, 3600)] float _timeDayInSeconds = 360;
    [SerializeField, Range(0f, 1f)] float _timeProgress;
    [SerializeField] private Light _dirLight;

    private Vector3 _defaultAngles;
    private bool _nightTimeIsEnabled = false;
    public bool NightTimeIsEnabled {  get { return _nightTimeIsEnabled; } }

    private void Start()
    {
        _defaultAngles = _dirLight.transform.localEulerAngles;
        _timeProgress = 0.2f;
    }

    private void Update()
    {
        _timeProgress += Time.deltaTime / _timeDayInSeconds;

        if (_timeProgress >= 0.8f)
            _nightTimeIsEnabled = true;

        _dirLight.color = _dirLightGradient.Evaluate(_timeProgress);
        RenderSettings.ambientLight = _ambLightGradient.Evaluate(_timeProgress);

        _dirLight.transform.localEulerAngles = new Vector3(360f * _timeProgress - 90, _defaultAngles.x, _defaultAngles.z);
    }
}
