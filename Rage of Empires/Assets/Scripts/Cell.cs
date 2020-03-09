using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Image Outline_Image;

    [HideInInspector]
    public Vector2Int Board_Position = Vector2Int.zero;
    [HideInInspector]
    public Board Board = null;
    [HideInInspector]
    public RectTransform Rect_Transform = null;

    [HideInInspector]
    public BasePiece Current_Piece = null;

    public void Setup(Vector2Int newBoardPosition, Board newBoard)
    {
        Board_Position = newBoardPosition;
        Board = newBoard;


        Rect_Transform = GetComponent<RectTransform>();
    }

    public void RemovePiece()
    {
        if(Current_Piece != null)
        {
            Current_Piece.Kill();
        }
    }
}
