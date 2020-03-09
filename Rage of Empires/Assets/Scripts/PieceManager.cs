using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [HideInInspector]
    public bool Is_King_Alive = true;

    public GameObject Piece_Prefab;

    private List<BasePiece> White_Pieces = null;
    private List<BasePiece> Black_Pieces = null;
    private List<BasePiece> Promoted_Pieces = new List<BasePiece>();

    private string[] Piece_Order = new string[12]
    {
        "P", "P", "P", "P", "P", "P",
        "P", "P", "P", "P", "P", "P", 
    };

    private Dictionary<string, Type> Piece_Library = new Dictionary<string, Type>()
    {
        {"P",  typeof(Pawn)},
        {"R",  typeof(Rook)},
        {"KN", typeof(Knight)},
        {"B",  typeof(Bishop)},
        {"K",  typeof(King)},
        {"Q",  typeof(Queen)}
    };

    public void Setup(Board board)
    {

        White_Pieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255), board);
        Black_Pieces = CreatePieces(Color.white, new Color32(210, 95, 64, 255), board);


        PlacePieces(1, 0, White_Pieces, board);
        PlacePieces(5, 4, Black_Pieces, board);


    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor, Board board)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        for (int i = 0; i < Piece_Order.Length; i++)
        {

            //Creates
            GameObject newPieceObject = Instantiate(Piece_Prefab);
            newPieceObject.transform.SetParent(transform);


            //Set Scale & Pos
            newPieceObject.transform.localScale = new Vector3(1, 1, 1);
            newPieceObject.transform.localRotation = Quaternion.identity;


            //Get type & apply to object
            string key = Piece_Order[i];
            Type piece_type = Piece_Library[key];

            //store new piece
            BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(piece_type);
            newPieces.Add(newPiece);

            //setup piece
            newPiece.Setup(teamColor, spriteColor, this);
        }

        return newPieces;
    }

    private BasePiece CreatePiece(Type pieceType)
    {
        return null;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board)
    {
        for (int i = 0; i < 6; i++)
        {
            pieces[i].Place(board.All_Cells[i, pawnRow]);

            pieces[i + 6].Place(board.All_Cells[i, royaltyRow]);
        }
    }

    private void SetInteractive(List<BasePiece> allPieces, bool value)
    {

    }

    public void SwitchSides(Color color)
    {
  
    }

    public void ResetPieces()
    {

    }

    public void PromotePiece(Pawn pawn, Cell cell, Color teamColor, Color spriteColor)
    {

    }
}
