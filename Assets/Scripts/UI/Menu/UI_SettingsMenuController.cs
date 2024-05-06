using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_SettingsMenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Slider commonSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundsSlider;
    [SerializeField]
    private Button closeButton;

    [Space]
    [Header("Other")]
    [SerializeField]
    private AudioMixer audioMixer;

    protected virtual void Start()
    {
        gameObject.SetActive(false);

        closeButton.onClick.AddListener(OnCloseButtonClicked);

        commonSlider.onValueChanged.AddListener((value) => SetVolumeBySliderValue(value, "Master"));
        musicSlider.onValueChanged.AddListener((value) => SetVolumeBySliderValue(value, "Music"));
        soundsSlider.onValueChanged.AddListener((value) => SetVolumeBySliderValue(value, "Sounds"));

        commonSlider.value = PlayerPrefs.GetFloat("Master");
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        soundsSlider.value = PlayerPrefs.GetFloat("Sounds");
    }

    private void SetVolumeBySliderValue(float sliderValue, string paramName)
    {
        audioMixer.SetFloat(paramName, sliderValue);
        PlayerPrefs.SetFloat(paramName, sliderValue);
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}