using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public AudioSource intro;
    public AudioSource siren;
    public AudioSource munch1;
    public AudioSource munch2;
    public AudioSource munchGhost;
    public AudioSource cleared;
    public AudioSource Gameover;
    public AudioSource pacmandeath;
    public int currentMunch = 0;

    public TMP_Text highScoreT;
    public TMP_Text Score;
    public TMP_Text gameOver;
    public TMP_Text livesTxt;

    public int score { get; private set;}
    public int hiScore { get; private set; }
    public int lives { get; private set;}
    public int ghostMultiplier { get; private set;}

    private void Awake()
    {
        currentMunch = 0;
    }

    private void Start()
    {
        intro.Play();
        NewGame();
        highScoreT.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        siren.Play();
        livesTxt.text = ("3");
    }

    private void NewRound()
    {
        gameOver.enabled = false;
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        
        ResetState();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }

    private void GameOver()
    {
        gameOver.enabled = true;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
        Gameover.Play();
        siren.Pause();
        livesTxt.text = "0";
    }

    public void SetScore(int score)
    {
        this.score = score;
        Score.text = score.ToString();

        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreT.text = score.ToString();
        }
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore((int)this.score + points);
        this.ghostMultiplier++;
        munchGhost.Play();
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives((int)this.lives - 1);
        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
            livesTxt.text = this.lives.ToString();
            pacmandeath.Play();
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(this.score + pellet.points);
        pellet.gameObject.SetActive(false);

        if(currentMunch == 0)
        {
            munch1.Play();
            currentMunch = 1;
        }
        else
        {
            if(currentMunch == 1)
            {
                munch2.Play();
                currentMunch = 0;
            }
        }

        if (!HasRemainningPellets())
        {
            Invoke(nameof(NewRound), 6.0f);
            cleared.Play();
            siren.Pause();
            this.pacman.gameObject.SetActive(false);
        }
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for(int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerPellet.duration);
        }
        
        PelletEaten(powerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
    }

    private bool HasRemainningPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1; 
    }
}
