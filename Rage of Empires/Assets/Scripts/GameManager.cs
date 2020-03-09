using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Board Board;
    public PieceManager Piece_Manager;

    void Start()
    {
        Board.Create();

        Piece_Manager.Setup(Board);
    }
}
