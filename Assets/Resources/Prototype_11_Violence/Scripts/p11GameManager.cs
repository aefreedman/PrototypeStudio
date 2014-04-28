using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p11GameManager : GameManagerBase
{

    private static p11GameManager instance;
    public p11FloorTile[] floorTiles;
    public p11TileCounter[] counter;
    private P12ViewBox[] viewBox;
    public GameObject[] eyeCube;
    public bool triggerGameEnd;
    public bool inBox;
    private bool triggerFade;
    public float fadeOutTime;

    public static p11GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<p11GameManager>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        floorTiles = GameObject.FindObjectsOfType<p11FloorTile>();
        counter = GameObject.FindObjectsOfType<p11TileCounter>();
        viewBox = GameObject.FindObjectsOfType<P12ViewBox>();
        //eyeCube = GameObject.FindGameObjectsWithTag("P11EyeCube");
    }

    public void ResetTiles()
    {
        for (int i = 0; i < floorTiles.Length; i++)
        {
            floorTiles[i].gameObject.renderer.material = floorTiles[i].materials[1];
        }
    }

    protected override void Update()
    {
        base.Update();
        int count = 0;
        for (int i = 0; i < counter.Length; i++)
        {
            if (counter[i].count == counter[i].target)
            {
                count++;
            }
        }
        if (count == counter.Length)
        {
            for (int i = 0; i < viewBox.Length; i++)
            {
                viewBox[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < eyeCube.Length; i++)
            {
                //if (eyeCube[i].activeInHierarchy == false)
                //{
                eyeCube[i].SetActive(true);
                //}
                eyeCube[i].GetComponent<SinWaveMove>().enabled = false;
                eyeCube[i].transform.position = Vector3.Lerp(eyeCube[i].transform.position, eyeCube[i].transform.up * 200.0f, 0.001f * Time.deltaTime);
            }

            if (!triggerFade)
            {
                CameraFade.StartAlphaFade(Color.black, false, fadeOutTime);
                triggerFade = true;
                StartCoroutine(EndGame(fadeOutTime/2));
            }
            gameState = State.GameOver;
        }

    }

    private IEnumerator EndGame(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(0);
    }
    

}