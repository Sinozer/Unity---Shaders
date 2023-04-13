using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private Canvas _canvas;

	private void Awake()
	{
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

        SetCanvas();
    }

	private void SetCanvas() => _canvas.gameObject.SetActive(false);

	public void EndGame() => _canvas.gameObject.SetActive(true);

	public void ReloadGame()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		op.completed += operation => SetCanvas();
		PlayerManager.Instance.CurrentHealth = PlayerManager.Instance.MaxHealth;
        PlayerManager.Instance.BloodGauge = 0;
    }
}
