using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutOfBounds
}

public class Board : MonoBehaviour
{
    public GameObject Cell_Prefab;

    [HideInInspector]
    public Cell[,] All_Cells = new Cell[6, 6];

    public void Create()
    {
        #region Create


        for(int y = 0; y < 6; y++)
        {
            for(int x = 0; x < 6; x++)
            {
                //create cell
                GameObject new_Cell = Instantiate(Cell_Prefab, transform);

                //position
                RectTransform rectTransform = new_Cell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                //setup
                All_Cells[x, y] = new_Cell.GetComponent<Cell>();
                All_Cells[x, y].Setup(new Vector2Int(x, y), this);

            }
        }
        #endregion

        #region color

        for (int x = 0; x < 6; x += 2)
        {
            for (int y = 0; y < 6; y++)
            {
                int offset = (y % 2 != 0) ? 0 : 1;
                int final_x = x + offset;

                All_Cells[final_x, y].GetComponent<Image>().color = new Color32(230, 220, 187, 255);
            }

        }


        #endregion
    }

    public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
    {   
        return CellState.Free;
    }
}
