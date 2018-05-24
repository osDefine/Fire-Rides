using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private float speed = 5f;
    private Vector3 direct;
    private Rigidbody rb;
    private CharacterJoint cj;

    private Vector3 velocity;
    [SerializeField] private bool onRope = false;

    public GameObject rope;
    public GameObject explosive;

    Transform tr_camera;
    Vector3 cam_delta;

    LineRenderer lineRope;
    Vector3 conect;
    int lm = 0;
    public GameManager gm;

    private float ball_x;

    void Start () {

        direct = Vector3.right;
        rb.velocity = direct * speed;
        tr_camera.position = new Vector3(0f, 10f, -26f) + transform.position;
        cam_delta = transform.position - tr_camera.position;
        ball_x = transform.position.x;
    }

    private void Awake() {
        tr_camera = Camera.main.transform;
        cj = GetComponent<CharacterJoint>();
        rb = GetComponent<Rigidbody>();
        lm = LayerMask.GetMask("Default");
        lineRope = GetComponent<LineRenderer>();
    }

    void Update () {

        if (onRope == true)
        {
            RopePosition(lineRope, transform.position, rope.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (onRope == true)
            {
                Destroy(transform.GetComponent<CharacterJoint>());
                rope.SetActive(false);
                onRope = false;
                lineRope.enabled = false;
            }
            else {
                Connect(new Vector3(0.3f, 1f, 0f));
            }
        }
	}

    void FixedUpdate() {
        Score();
    }

    void Score() {

        if (ball_x < transform.position.x)
        {
            gm.AddScore((transform.position.x - ball_x) * Time.deltaTime);
            ball_x = transform.position.x;
        }
    }

    public void Connect(Vector3 dir) {
        RaycastHit hit;
        Vector3 ropeConnect;
        if (Physics.Raycast(transform.position, dir, out hit, 100f, lm, QueryTriggerInteraction.Collide))
        {
            onRope = true;
            rope.GetComponent<CharacterJoint>().anchor = Vector3.zero;

            ropeConnect = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - (hit.collider.transform.localScale.y / 2), 0f);
            rope.transform.position = ropeConnect;
            rope.GetComponent<CharacterJoint>().connectedAnchor = ropeConnect;
            rope.GetComponent<CharacterJoint>().connectedMassScale = 1;
            rope.GetComponent<CharacterJoint>().massScale = 1;
            rope.SetActive(true);

            gameObject.AddComponent<CharacterJoint>().connectedBody = rope.GetComponent<Rigidbody>();
            CharacterJoint charJoin = GetComponent<CharacterJoint>();
            charJoin.anchor = Vector3.zero;
            charJoin.connectedAnchor = rope.transform.position;
            charJoin.connectedMassScale = 1;
            charJoin.massScale = 1;

            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.magnitude * 1.05f * Vector3.Cross((transform.position - rope.transform.position), Vector3.back).normalized;

            lineRope.enabled = true;
            RopePosition(lineRope, transform.position, rope.transform.position);
        }
    }

    private void RopePosition(LineRenderer lr, Vector3 start, Vector3 end) {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    private void EnableLineRenderer(LineRenderer lr, bool check) {
        lr.enabled = !check;
    }

    void LateUpdate()
    {
        tr_camera.position = transform.position - cam_delta;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject ex = Instantiate(explosive, transform.position, Quaternion.identity) as GameObject;
        rope.SetActive(false);
        gm.NewGame();
        Destroy(this.gameObject);
    }
}
