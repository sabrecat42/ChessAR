using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoard : MonoBehaviour
{
    public PieceController[] pieces;

    public void ResetPieces()
    {
        foreach (PieceController piece in pieces)
        {
            piece.MoveToStartingtSquare();
        }
    }
}
