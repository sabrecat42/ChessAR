using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePlacerController : MonoBehaviour
{
    [SerializeField]
    private GameObject hoverObject;

    [HideInInspector]
    public GameObject currentHoveredPlace;

    public bool TryPlacePiece(GameObject piece)
    {
        if (currentHoveredPlace == null) return false;

        piece.transform.position = currentHoveredPlace.transform.position;
        piece.transform.rotation = currentHoveredPlace.transform.rotation;

        piece.GetComponent<PieceController>().lastSquare = currentHoveredPlace;

        this.UnhoverPlace(currentHoveredPlace);

        return true;
    }

    public void HoverPlace(GameObject hoveredArea)
    {
        hoverObject.SetActive(true);
        hoverObject.transform.position = hoveredArea.transform.position;
        currentHoveredPlace = hoveredArea;
    }

    public void UnhoverPlace(GameObject unhoveredArea)
    {
        if (currentHoveredPlace == unhoveredArea)
        {
            currentHoveredPlace = null;
            hoverObject.SetActive(false);
        }
    }

}
