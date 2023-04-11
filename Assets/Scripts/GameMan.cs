using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMan : MonoBehaviour {
	public static GameMan Instance;

	private GameObject _canvas;

	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		_canvas = GameObject.Find("Canvas");
		_canvas.SetActive(false);
	}

	public void EndGame() {
		_canvas.SetActive(true);
	}

	public void ReloadGame() {
		SceneManager.LoadScene(0);
	}
}
