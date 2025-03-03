
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public Camera cam;
    public GameObject dot;
    public float time_between_spawns = 1f;
    public float dot_timer = 0.0f;
    private int score = 0;
    private int points = 10;
    public TMP_Text score_text;
    private float game_timer = 60;
    public TMP_Text game_timer_text;
    public GameObject restart_button;


    // Start is called before the first frame update
    void Start()
    {
        dot_timer = 0.5f;
        score_text.text = "Score 0";
        restart_button.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        game_timer -= Time.deltaTime;
        if ( game_timer < 0 )
        {
            game_timer = 0;
            game_timer_text.text = "Time:0";
            restart_button.SetActive(true);
            return;
        }
        game_timer_text.text = "Time:" + game_timer.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse time");
            Vector2 worldpoint = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldpoint, Vector2.zero);


            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
                score += points;
            }

            score_text.text = "Score:" + score.ToString();

        }

        dot_timer -= Time.deltaTime;

        if (dot_timer < 0.0f)
        {
            dot_timer = time_between_spawns;

            SpawnDot();
        }


    }

    void SpawnDot()
    {
        GameObject new_dot = Instantiate(dot);


        var x_pos = Random.Range(0, cam.scaledPixelHeight);
        var y_pos = Random.Range(0, cam.scaledPixelWidth);

        Vector3 spawn_point = new Vector3(x_pos, y_pos, 0);
        spawn_point = cam.ScreenToWorldPoint(spawn_point);
        spawn_point.z = 0;

        new_dot.transform.position = spawn_point;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
