using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] Rows {  get; private set; }
    public TileCell[] cells { get; private set; }
    public int size => cells.Length;
    public int height => Rows.Length;
    public int width => size / height;
    private void Awake()
    {
        Rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }
    private void Start()
    {
        for (int i = 0; i < Rows.Length; i++)
        {
            for(int j = 0; j < Rows[i].cells.Length; j++) 
            {
                Rows[i].cells[j].coordinates = new Vector2Int(j, i);
            }
        }
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return Rows[y].cells[x];
        }
        else return null;
    }
    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinats = cell.coordinates;
        coordinats.x += direction.x;
        coordinats.y -= direction.y;

        return GetCell(coordinats);
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;
        while (cells[index].occupied)
        {
            index++;

            if(index >= cells.Length)
            {
                index = 0;
            }
            if (index==startingIndex)
            {
                return null;
            }
        }
        return cells[index];
    }

}
