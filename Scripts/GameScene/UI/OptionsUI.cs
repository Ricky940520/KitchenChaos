using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance { get; private set; }

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundEffectVolumeText;
    [SerializeField] private Button colseButton;

    private const float VOLUME_RATE = 10F;

    private const string MUSIC_VOLUME = "Music_Volume";
    private const string SOUND_EFFECT_VOLUME = "Sound_Effect_Volume";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Initialize();

        musicSlider.onValueChanged.AddListener((x) =>
        {
            SoundManager.instance.ChangeMusicVolume(x);
            UpDateMusicVolumeText();
        });

        soundEffectSlider.onValueChanged.AddListener((x) =>
        {
            SoundManager.instance.ChangeSoundEffectVolume(x);
            UpdateSoundEffectVolume();
        });

        colseButton.onClick.AddListener(() =>
        {
            Hide();
            Save();
            KitchenChaosGameManager.Instance.GameInput_OnGamePaused();
        });

        KitchenChaosGameManager.Instance.OnGamePaused += KitchenChaosGameManager_OnGamePased;

        Hide();
    }


    private void UpDateMusicVolumeText()
    {
        musicVolumeText.text = Mathf.Round(SoundManager.instance.GetMusicVolume() * VOLUME_RATE).ToString();
    }

    private void UpdateSoundEffectVolume()
    {
        soundEffectVolumeText.text = Mathf.Round(SoundManager.instance.GetSoundEffectVolume() * VOLUME_RATE).ToString();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void Initialize()
    {
        musicVolumeText.text = Mathf.Round(PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f) * VOLUME_RATE).ToString();
        soundEffectVolumeText.text = Mathf.Round(PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f) * VOLUME_RATE).ToString();

        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f);
        soundEffectSlider.value = PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f);

        SoundManager.instance.ChangeMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f));
        SoundManager.instance.ChangeSoundEffectVolume(PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f));
    }

    private void KitchenChaosGameManager_OnGamePased(bool isGamePasued)
    {
        Hide();
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, SoundManager.instance.GetMusicVolume());
        PlayerPrefs.SetFloat(SOUND_EFFECT_VOLUME, SoundManager.instance.GetSoundEffectVolume());
        PlayerPrefs.Save();
    }
}
