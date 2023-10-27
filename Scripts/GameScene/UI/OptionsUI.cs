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

    [SerializeField] private TextMeshProUGUI upText;
    [SerializeField] private TextMeshProUGUI downText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI alternateText;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button alternateButton;

    [SerializeField] private Transform pressToRebindingTransform;


    private void Awake()
    {
        instance = this;

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
            SaveData();
            KitchenChaosGameManager.Instance.GameInput_OnGamePaused();
        });

        KitchenChaosGameManager.Instance.OnGameResume += KitchenChaosGameManager_OnGameResume;

        upButton.onClick.AddListener(() =>
        {
            OnKeyButtonPressed(upText, GameInput.KeyBinding.Up);
        });

        downButton.onClick.AddListener(() =>
        {
            OnKeyButtonPressed(downText, GameInput.KeyBinding.Down);
        });

        leftButton.onClick.AddListener(() =>
        {
            OnKeyButtonPressed(leftText, GameInput.KeyBinding.Left);
        });

        rightButton.onClick.AddListener(() =>
        {
            OnKeyButtonPressed(rightText, GameInput.KeyBinding.Right);
        });

        interactButton.onClick.AddListener(() =>
        {
            OnKeyButtonPressed(interactText, GameInput.KeyBinding.Interace);
        });

        alternateButton.onClick.AddListener(() =>
        {
            OnKeyButtonPressed(alternateText, GameInput.KeyBinding.Alternate);
        });
    }

    private void Start()
    {
        Initialize();
    }


    private void UpDateMusicVolumeText()
    {
        musicVolumeText.text = Mathf.Round(SoundManager.instance.GetMusicVolume() * VOLUME_RATE).ToString();
    }

    private void UpdateSoundEffectVolume()
    {
        soundEffectVolumeText.text = Mathf.Round(SoundManager.instance.GetSoundEffectVolume() * VOLUME_RATE).ToString();
    }

    private void UpdateKeyRebindingText(TextMeshProUGUI keyRebindingText, GameInput.KeyBinding keyBinding)
    {
        keyRebindingText.text = GameInput.Instance.GetKeyRebindingString(keyBinding);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        musicSlider.Select();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        HidePressToRebinding();
    }

    private void ShowPressToRebinding()
    {
        pressToRebindingTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebinding()
    {
        pressToRebindingTransform.gameObject.SetActive(false);
    }

    private void Initialize()
    {
        LoadData();

        Hide();
    }

    private void KitchenChaosGameManager_OnGameResume()
    {
        Hide();
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, SoundManager.instance.GetMusicVolume());
        PlayerPrefs.SetFloat(SOUND_EFFECT_VOLUME, SoundManager.instance.GetSoundEffectVolume());
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        musicVolumeText.text = Mathf.Round(PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f) * VOLUME_RATE).ToString();
        soundEffectVolumeText.text = Mathf.Round(PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f) * VOLUME_RATE).ToString();

        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f);
        soundEffectSlider.value = PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f);

        SoundManager.instance.ChangeMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f));
        SoundManager.instance.ChangeSoundEffectVolume(PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f));

        UpdateKeyRebindingText(upText, GameInput.KeyBinding.Up);
        UpdateKeyRebindingText(downText, GameInput.KeyBinding.Down);
        UpdateKeyRebindingText(leftText, GameInput.KeyBinding.Left);
        UpdateKeyRebindingText(rightText, GameInput.KeyBinding.Right);
        UpdateKeyRebindingText(interactText, GameInput.KeyBinding.Interace);
        UpdateKeyRebindingText(alternateText, GameInput.KeyBinding.Alternate);
    }

    private void OnKeyButtonPressed(TextMeshProUGUI text, GameInput.KeyBinding keyBinding)
    {
        GameInput.Instance.KeyReBinding(keyBinding, () =>
        {
            HidePressToRebinding();
            UpdateKeyRebindingText(text, keyBinding);
        });

        ShowPressToRebinding();
    }

}
