using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public abstract class BasePiece : EventTrigger
{
    [HideInInspector]
    public Color Piece_Color = Color.clear;
    public bool Is_FirstMove = true;

    protected Cell Original_Cell = null;
    protected Cell Current_Cell = null;

    protected RectTransform Rect_Transform = null;
    protected PieceManager Piece_Manager;

    protected Cell Target_Cell = null;

    protected Vector3Int Movement = Vector3Int.one;
    protected List<Cell> Highlighted_Cells = new List<Cell>();

    public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        Piece_Manager = newPieceManager;

        Piece_Color = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        Rect_Transform = GetComponent<RectTransform>();
    }

    public virtual void Place(Cell newCell)
    {
        Current_Cell = newCell;
        Original_Cell = newCell;
        Current_Cell.Current_Piece = this;


        transform.position = newCell.transform.position;
        gameObject.SetActive(true);
    }

    public void Reset()
    {
        Kill();

        Place(Original_Cell);
    }

    public virtual void Kill()
    {
        Current_Cell.Current_Piece = null;

        gameObject.SetActive(false);
    }

    #region Movement
    private void CreateCellPath(int xDirection, int yDirection, int movement)
    {

        int current_X = Current_Cell.Board_Position.x;
        int current_Y = Current_Cell.Board_Position.y;

        for(int i = 1; i <= movement; i++)
        {
            current_X += xDirection;
            current_Y += yDirection;


            Highlighted_Cells.Add(Current_Cell.Board.All_Cells[current_X, current_Y]);
        }

    }

    protected virtual void CheckPathing()
    {

        CreateCellPath(1, 0, Movement.x);
        CreateCellPath(-1, 0, Movement.x);

        CreateCellPath(0, 1, Movement.y);
        CreateCellPath(0, -1, Movement.y);


        CreateCellPath(1, 1, Movement.z);
        CreateCellPath(-1, 1, Movement.z);


        CreateCellPath(-1, -1, Movement.z);
        CreateCellPath(1, -1, Movement.z);

        Debug.Log("Move");

    }

    protected void ShowCells()
    {
        foreach(Cell cell in Highlighted_Cells)
        {
            cell.Outline_Image.enabled = true;
        }
    }

    protected void ClearCells()
    {
        foreach (Cell cell in Highlighted_Cells)
        {
            cell.Outline_Image.enabled = false;

            Highlighted_Cells.Clear();
        }
    }

    protected virtual void Move()
    {
        Target_Cell.RemovePiece();


        Current_Cell.Current_Piece = null;

        Current_Cell = Target_Cell;
        Current_Cell.Current_Piece = this;

        transform.position = Current_Cell.transform.position;
        Target_Cell = null;
    }
    #endregion

    #region Events
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        CheckPathing();

        ShowCells();


        Debug.Log("Move1");

    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        transform.position += (Vector3)eventData.delta;


        foreach(Cell cell in Highlighted_Cells)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(cell.Rect_Transform, Input.mousePosition))
            {
                Target_Cell = cell;
                break;
            }

            Target_Cell = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        ClearCells();
        if (!Target_Cell)


        {
            transform.position = Current_Cell.gameObject.transform.position;
            return;
        }

        Move();
    }
    #endregion
}
