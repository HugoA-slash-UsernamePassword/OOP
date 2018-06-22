using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10, height = 10;
    public float spacing = .155f;
    private Tile[,] tiles;

    private void Start()
    {
        GenerateTiles();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectATile();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SelectATile(true);
        }
    }
    // Use this for initialization
    Tile SpawnTile(Vector3 pos)
    {
        GameObject clone = Instantiate(tilePrefab);
        clone.transform.position = pos;
        Tile currentTile = clone.GetComponent<Tile>();
        return currentTile;
    }

    // Update is called once per frame
    void GenerateTiles()
    {
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Vector2 halfSize = new Vector2(width, height) / 2;
                Vector2 pos = new Vector2(x, y) - halfSize;

                Vector2 offset = new Vector2(.5f, .5f);
                pos += offset;

                pos *= spacing;

                Tile tile = SpawnTile(pos);
                tile.transform.SetParent(transform);
                tile.x = x;
                tile.y = y;

                tiles[x, y] = tile;
            }
        }
    }
    void SelectATile(bool tileMode = false)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);

        if (hit.collider != null)
        {
            Tile hitTile = hit.collider.GetComponent<Tile>();

            if (hitTile != null)
            {
                SelectTile(hitTile, tileMode);
            }
        }
    }
    public int GetAdjacentMineCount(Tile tile)
    {
        int count = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                int desiredX = tile.x + x;
                int desiredY = tile.y + y;

                if (desiredX < 0 || desiredX >= width || 
                    desiredY < 0 || desiredY >= height)
                {
                    continue;
                }

                Tile currentTile = tiles[desiredX, desiredY];

                if (currentTile.isMine)
                {
                    count++;
                }
            }
        }
        return count;
    }

    void FFuncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && 
            x < width && y < height)
        {
            if (visited[x, y])
                return;

            Tile tile = tiles[x, y];
            int adjacentMines = GetAdjacentMineCount(tile);
            tile.Reveal(adjacentMines);

            if (adjacentMines == 0)
            {
                visited[x, y] = true;

                for (int xx = -1; xx <= 1; xx++)
                {
                    for (int yy = -1; yy <= 1; yy++)
                    {
                        FFuncover(x + xx, y + yy, visited);
                    }
                }
            }
        }
    }

    void UncoverMines(int mineState = 0)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = tiles[x, y];
                if (tile.isMine)
                {
                    int adjacentMines = GetAdjacentMineCount(tile);
                    tile.Reveal(adjacentMines, mineState);
                }
            }
        }
    }
    bool NoMoreEmptyTiles()
    {
        int emptyTileCount = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = tiles[x, y];
                if (!tile.isRevealed && 
                    !tile.isMine)
                {
                    emptyTileCount++;
                }
            }
        }
        return emptyTileCount == 0;
    }

    void SelectTile(Tile selected, bool tileMode = false)
    {
        if (!tileMode && !selected.isFlag)
        {
            int adjacentMines = GetAdjacentMineCount(selected);
            selected.Reveal(adjacentMines);

            if (selected.isMine)
            {
                UncoverMines();
                selected.Reveal(0, 1);
            }
            else if (adjacentMines == 0)
            {
                int x = selected.x;
                int y = selected.y;

                FFuncover(x, y, new bool[width, height]);
            }
            if (NoMoreEmptyTiles())
            {
                UncoverMines(1);
            }
        }
        else if (!selected.isRevealed)
        {
            selected.Capture();
        }
    }
}
