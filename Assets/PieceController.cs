using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{

    //    public CardPlacerController cardPlacerController;

    [HideInInspector]
    public GameObject lastSquare;
    [HideInInspector]
    public GameObject startingSquare;
    [HideInInspector]
    public GameObject hoveredSquare;

    private string thisColor;

    //public PiecePlacerController piecePlacerController;

    void Start()
    {
        thisColor = gameObject.name.Split("_")[0];
        startingSquare = gameObject.transform.parent.gameObject;    // keep the initial square (should be parent of this piece)
        lastSquare = startingSquare;
    }

    public void OnGrabEnd()
    {
        Debug.Log("onGrabEnd has been been called");

        
        if (hoveredSquare == null)  // place the piece to the currently hovered square, else retrun to last sqaure
        {
            Debug.Log("failed to place " + gameObject.name);
            ReturnToLastSquare();
        } else if (hoveredSquare.transform.childCount > 1)  // in case the hovered square is occupied
        {
            Debug.Log("Trying to place on an occupied square.");
            for (int i = 0; i < hoveredSquare.transform.childCount; ++i)
            {
                GameObject hoveredSquarePiece = hoveredSquare.transform.GetChild(i).gameObject;
                string hoveredSquarePieceColor = hoveredSquarePiece.name.Split("_")[0];
                Debug.Log("hoveredSquarePieceColor = " + hoveredSquarePieceColor);
                if ((hoveredSquarePieceColor == "Light" || hoveredSquarePieceColor == "Dark") && hoveredSquarePiece.activeSelf == true) // if there's an active piece on the hovered square
                {
                    if (hoveredSquarePieceColor == thisColor)
                    {
                        Debug.Log("failed to place " + gameObject.name + ", because the square was occupied by same color: " + hoveredSquarePiece.name);
                        ReturnToLastSquare();
                    }
                    else
                    {
                        Debug.Log("Successfuly captured piece: " + hoveredSquare.transform.GetChild(i).name);
                        hoveredSquare.transform.GetChild(i).gameObject.SetActive(false);    // de-activate the captured piece
                        MoveToHoveredSquare();
                    }
                    break;  // eixt loop after the piece on the sqaure has been found
                } else if (i == hoveredSquare.transform.childCount-1)   // in case this was the last child of the square and none of the pieces were active, allow the movement
                {
                    Debug.Log("succesfuly placed " + gameObject.name + " on square: " + hoveredSquare.name + "; there's an inactive piece here");
                    MoveToHoveredSquare();
                }
            }

        } else
        {
            Debug.Log("succesfuly placed " + gameObject.name + " on square: " + hoveredSquare.name);
            MoveToHoveredSquare();
        }
    }

    public void OnGrabBegin()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.parent.name == "BoardSquares")
        {
            Debug.Log("Entered square: " + other.name);
            other.gameObject.transform.Find("SelectionCylinder").gameObject.SetActive(true);  // activate highlight
            SetHoveredSquare(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.parent.name == "BoardSquares")
        {
            Debug.Log("Exited " + other.name);
            other.gameObject.transform.Find("SelectionCylinder").gameObject.SetActive(false);  // deactivate highlight

            if (hoveredSquare.gameObject == other.gameObject)
            {
                RemoveHoveredSquare();
                Debug.Log(gameObject.name + " exited square: " + other.gameObject.name);
            } else
            {
                Debug.Log(gameObject.name + " failed to exit actual previous square: " + hoveredSquare.name);
            }
        }
    }

    private void SetHoveredSquare(GameObject square)
    {
        hoveredSquare = square;
    }

    private void RemoveHoveredSquare()
    {
        hoveredSquare = null;
    }

    private void ReturnToLastSquare()
    {
        if (thisColor == "Dark")
        {
            //gameObject.transform.SetPositionAndRotation(lastSquare.transform.position, lastSquare.transform.rotation);
            gameObject.transform.position = lastSquare.transform.position;
            gameObject.transform.eulerAngles = lastSquare.transform.eulerAngles = new Vector3(
                lastSquare.transform.eulerAngles.x,
                lastSquare.transform.eulerAngles.y + 180,
                lastSquare.transform.eulerAngles.z
            );
        } else
        {
            gameObject.transform.SetPositionAndRotation(lastSquare.transform.position, lastSquare.transform.rotation);
        }
        
    }

    private void MoveToHoveredSquare()
    {
        gameObject.transform.parent = hoveredSquare.transform;  // change this piece's parent square to the hovered square

        if (thisColor == "Dark")
        {
            //gameObject.transform.SetPositionAndRotation(lastSquare.transform.position, lastSquare.transform.rotation);
            gameObject.transform.position = hoveredSquare.transform.position;
            gameObject.transform.eulerAngles = hoveredSquare.transform.eulerAngles = new Vector3(
                hoveredSquare.transform.eulerAngles.x,
                hoveredSquare.transform.eulerAngles.y + 180,
                hoveredSquare.transform.eulerAngles.z
            );
        } else
        {
            gameObject.transform.SetPositionAndRotation(hoveredSquare.transform.position, hoveredSquare.transform.rotation);
        }
        
        lastSquare = hoveredSquare;
    }

    public void MoveToStartingtSquare()
    {
        gameObject.SetActive(true);
        gameObject.transform.parent = startingSquare.transform;  // change this piece's parent square to the hovered square

        if (thisColor == "Dark")
        {
            //gameObject.transform.SetPositionAndRotation(lastSquare.transform.position, lastSquare.transform.rotation);
            gameObject.transform.position = startingSquare.transform.position;
            gameObject.transform.eulerAngles = startingSquare.transform.eulerAngles = new Vector3(
                startingSquare.transform.eulerAngles.x,
                startingSquare.transform.eulerAngles.y + 180,
                startingSquare.transform.eulerAngles.z
            );
        }
        else
        {
            gameObject.transform.SetPositionAndRotation(startingSquare.transform.position, startingSquare.transform.rotation);
        }

        lastSquare = startingSquare;
    }
}
