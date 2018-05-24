using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    [SerializeField]
    private float min_dist = 10f;
    [SerializeField]
    private float max_dist = 20f;
    private float step;
    public float scale = 5f;
    private float height;
    [SerializeField]
    public GameObject[] boxs;

    bool setPosBall = false;

    public int min_between_bon;
    public int max_between_bon;
    public GameObject[] bonus;

    private Vector3 posBoll;
    public Vector3 PosBoll{ set { posBoll = value; } get { return posBoll; } }
    public GameManager gm;
    private GameObject walls;
    int wall = 0;

    void Awake() {
        step = boxs[0].transform.localScale.x;
        height = boxs[0].transform.localScale.y / 2f;
        posBoll = new Vector3();

        gm = GetComponent<GameManager>();
    }

	void Update () {
		
	}

    public void BuildMap(float randomePos) {

        if (walls != null)
        {
            walls.gameObject.SetActive(false);
            Destroy(walls.transform.gameObject);
        }

        walls = new GameObject();

        setPosBall = false;
        int pow = 1;
        float y = 0f;
        float delta;
        Vector3 pos_down = new Vector3();
        int bonus_length_bet = Random.Range(min_between_bon, max_between_bon);
        int j = 0;
        int count_bonus = bonus.Length;
        Vector3 bonus_pos = new Vector3();

        for (int i = 0; i < 500; i++)
        {
            if (i > 30 && i < 60) pow = 2;
            if (i > 60) pow = 3;

            float pos_y = Mathf.PerlinNoise(0, randomePos + i / 35f * scale);

            y = height - Mathf.Abs((0.5f - (Mathf.Pow(pos_y * 3f, pow))));
            pos_down = new Vector3(i * step, y - Random.Range(min_dist, max_dist), 0f);
            GameObject line_down = Instantiate(boxs[i % 2], pos_down, Quaternion.identity) as GameObject;
            line_down.transform.parent = walls.transform;

            y = pos_down.y + (height * 2f) + Random.Range(min_dist, max_dist);
            pos_down = new Vector3(i * step, y, 0f);

            GameObject line_up = Instantiate(boxs[i % 2], pos_down, Quaternion.identity) as GameObject;
            line_up.transform.parent = walls.transform;
            if (setPosBall == false && i == 20)
            {
                setPosBall = true;
                delta = (line_down.transform.position.y + line_up.transform.position.y) / 2f;

                posBoll = new Vector3(line_down.transform.position.x, delta, 0f);
            }


            if (j == bonus_length_bet - 1)
            {
                bonus_pos = new Vector3(line_down.transform.position.x, (line_down.transform.position.y + line_up.transform.position.y) / 2f, 0f);
                GameObject _bonus = Instantiate(bonus[Random.Range(0, count_bonus - 1)], bonus_pos, Quaternion.identity) as GameObject;
                _bonus.GetComponentInChildren<BunusRing>().gm = gm;
                _bonus.transform.parent = walls.transform;
                j = 0;
                bonus_length_bet = Random.Range(min_between_bon, max_between_bon);
            }
            j++;
        }
    }

    public void SpawnBonus() {

    }
}
