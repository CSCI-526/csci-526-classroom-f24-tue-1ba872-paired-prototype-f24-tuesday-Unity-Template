using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Game management and AI
public class GameSys : MonoBehaviour
{
    // All walls
    public List<Transform> WallList;

    public List<EnemyAI> EnemyList = new List<EnemyAI>();
    public GameObject Player;

    public List<Vector3Int> RoadPointList = new List<Vector3Int>();
    public GameObject GameWinUI, GameLoseUI;

    public static GameSys Ins;

    // Switch to a specified scene
    public void GameSceneChange(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Ins = this;
        // Calculate walkable paths
        var wallpoint = WallList.Select(v => v.position.ToInt()).ToList();

        for (int i = 0; i < 9; i++)
        {
            for (int t = 0; t < 23; t++)
            {
                bool add = true;
                // Everything except walls is considered a walkable path
                wallpoint.ForEach(v =>
                {
                    if (v.x == t && v.z == i)
                    {
                        add = false;
                    }
                });
                if (add)
                    RoadPointList.Add(new Vector3Int(t, 0, i));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Check for victory
    void FixedUpdate()
    {
        if (EnemyList.FindAll(v => v.gameObject.activeSelf).Count == 1)
        {
            EnemyAI firstEnemy = GameSys.Ins.EnemyList[0];
            if (firstEnemy.GetComponent<Renderer>().material.color == Color.green)
            {
                Player.GetComponent<Renderer>().material.color = Color.red;
            }
            else if(firstEnemy.GetComponent<Renderer>().material.color == Color.red)
            {
                Player.GetComponent<Renderer>().material.color = Color.green;
            }
        }

        if (EnemyList.FindAll(v => v.gameObject.activeSelf).Count <= 0)
        {
            GameWinUI.SetTrue();
            Player.SetFalse();
        }
    }
}
