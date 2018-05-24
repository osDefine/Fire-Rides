using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class GameManager : MonoBehaviour {

    public GameObject ball;
    private GameObject _ball;
    Rigidbody ball_rb;
    public MapGeneration map;

    public GameObject box1;
    public GameObject box2;
    public GameObject rope;
    private GameObject _rope;

    public Text t_score;
    private float ball_x;
    public Camera camera;
    private float counter = 0;
    public Button btn_start;
    public Button btn_reset;

    void Awake() {
        map = GetComponent<MapGeneration>();
        camera = Camera.main;
        _rope = Instantiate(rope, Vector3.zero, Quaternion.identity) as GameObject;
    }

    void Start() {
        StartGame();
    }

    public void StartGame() {
        ball_x = 0;
        counter = 0f;

        btn_start.gameObject.SetActive(false);
        map.BuildMap(Random.Range(0f, 0f));

        _ball = Instantiate(ball, map.PosBoll, Quaternion.identity) as GameObject;
        _ball.GetComponent<Ball>().gm = this;
        _ball.GetComponent<Ball>().rope = _rope;
        _ball.GetComponent<Ball>().Connect(new Vector3(0.3f, 1f, 0f));
        ball_rb = _ball.GetComponent<Rigidbody>();
        ball_x = _ball.transform.position.x;
    }

    public void NewGame() {
        btn_start.gameObject.SetActive(true);
    }

    public void AddScore(float score) {

        counter += score;
        t_score.text = string.Format("{0:N1}", counter).ToString();
    }
}
