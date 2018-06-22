using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public int x, y;
    public bool isMine = false;
    public bool isRevealed = false;
    public bool isFlag = false;
    [Header("References")]
    public Sprite[] emptySprites;
    public Sprite[] mineSprites;
    public Sprite flagSprite;
    public Sprite defaultSprite;
    private SpriteRenderer rend;
    // Use this for initialization
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        defaultSprite = rend.sprite;
    }

    // Update is called once per frame
    void Start()
    {
        isMine = Random.value < .15f;
    }
    public void Reveal(int adjacentMines, int mineState = 0)
    {
        isRevealed = true;

        if (isMine)
        {
            if (isFlag)
            {
                rend.sprite = mineSprites[2];
            }
            else
            {
                rend.sprite = mineSprites[mineState];
            }
        }
        else
        {
            rend.sprite = emptySprites[adjacentMines];
        }
    }

    public void Capture()
    {
        isFlag = !isFlag;

        if (isFlag)
        {
            rend.sprite = flagSprite;
        }
        else
        {
            rend.sprite = defaultSprite;
        }
    }
}
