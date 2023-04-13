using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private int maxHealth;

    private DamageFlash _damageFlash;
    public int BloodGauge { get; set; }

    struct Levels
    {
        public int _currentLevel;
        public int _currentXp;
        public int _xpToNextLvl;
    }

    private Levels _levels;

    public int CurrentHealth;

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

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        _levels._currentLevel = 1;
        _levels._currentXp = 0;
        _levels._xpToNextLvl = 1 * 10;

        BloodGauge = 100;

        _damageFlash = GameObject.FindWithTag("PlayerBody").GetComponent<DamageFlash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Debug.Log("Dead");
            GameMan.Instance.EndGame();
        }
    }
}
