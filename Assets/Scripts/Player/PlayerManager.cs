using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private int maxHealth;
    [SerializeField] private Volume healthScreenEffectVol;
    public int BloodGauge { get; set; }

    struct Levels
    {
        public int _currentLevel;
        public int _currentXp;
        public int _xpToNextLvl;
    }

    private Levels _levels;
    private Vignette _healthScreenEffectVignette;

    public int CurrentHealth;
    public bool IsInvisible;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
        IsInvisible = false;
        _levels._currentLevel = 1;
        _levels._currentXp = 0;
        _levels._xpToNextLvl = 1 * 10;

        BloodGauge = 100;
        healthScreenEffectVol.profile.TryGet(out _healthScreenEffectVignette);
    }

    private void Update()
    {
        float healthPercentage = (float) CurrentHealth / maxHealth;
        UpdateScreenEffect(healthPercentage);

        if (CurrentHealth > 0) return;
        Debug.Log("Dead");
        GameMan.Instance.EndGame();
    }
    
    private void UpdateScreenEffect(float healthPercentage)
    {
        _healthScreenEffectVignette.intensity.value = 1 - healthPercentage;
    }
}
