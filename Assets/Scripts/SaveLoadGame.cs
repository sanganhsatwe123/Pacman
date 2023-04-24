using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveLoadGame : MonoBehaviour
{
    public Transform pacman;
    public Transform pellets;
    public Transform[] ghosts;
    public TMP_Text score;

    public void SaveGame()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < pellets.childCount; i++)
        {
            Transform pellet = pellets.GetChild(i);
            int isActive = pellet.gameObject.activeSelf ? 1 : 0;
            sb.Append(isActive).Append(",");
            sb.Append(pellet.position.x).Append(",");
            sb.Append(pellet.position.y).Append(",");
            PlayerPrefs.SetString("Pellets" + i, sb.ToString());
            sb.Clear();
        }

        sb.Append(pacman.position.x).Append(",");
        sb.Append(pacman.position.y).Append(",");
        sb.Append(pacman.position.z).Append(",");
        PlayerPrefs.SetString("Pacman", sb.ToString());
        sb.Clear();

        for (int i = 0; i < ghosts.Length; i++)
        {
            Transform ghost = ghosts[i];
            sb.Append(ghost.position.x).Append(",");
            sb.Append(ghost.position.y).Append(",");
            sb.Append(ghost.position.z).Append(",");
            PlayerPrefs.SetString("Ghosts" + i, sb.ToString());
            sb.Clear();
        }

        PlayerPrefs.SetInt("Score", int.Parse(score.text));
        PlayerPrefs.SetInt("lives", FindObjectOfType<GameManager>().lives);
        PlayerPrefs.Save();
    }
    public void LoadGame()
    {
        for (int i = 0; i < pellets.childCount; i++)
        {
            string data = PlayerPrefs.GetString("Pellets" + i);
            string[] values = data.Split(',');
            int isActive = int.Parse(values[0]);
            if (isActive == 0)
            {
                pellets.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                pellets.GetChild(i).gameObject.SetActive(true);
                float x = float.Parse(values[1]);
                float y = float.Parse(values[2]);
                pellets.GetChild(i).position = new Vector3(x, y, 0);
            }
        }

        string pacmandata = PlayerPrefs.GetString("Pacman");
        string[] pacmanValues = pacmandata.Split(',');
        float pacman_x = float.Parse(pacmanValues[0]);
        float pacman_y = float.Parse(pacmanValues[1]);
        float pacman_z = float.Parse(pacmanValues[2]);
        pacman.position = new Vector3(pacman_x, pacman_y, pacman_z);

        for (int i = 0; i < ghosts.Length; i++)
        {
            string ghostdata = PlayerPrefs.GetString("Ghosts" + i);
            string[] ghostValues = ghostdata.Split(',');
            float x = float.Parse(ghostValues[0]);
            float y = float.Parse(ghostValues[1]);
            float z = float.Parse(ghostValues[2]);
            ghosts[i].position = new Vector3(x, y, z);
        }

        FindObjectOfType<GameManager>().SetScore(PlayerPrefs.GetInt("Score"));
        score.text = PlayerPrefs.GetInt("Score").ToString();
        FindObjectOfType<GameManager>().SetLives(PlayerPrefs.GetInt("lives"));
    }
}