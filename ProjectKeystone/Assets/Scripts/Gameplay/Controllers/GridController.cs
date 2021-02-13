using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    PuzzleGrid currentPuzzleGrid;
    PuzzleState currentPuzzleState;
    PuzzleCursor currentPuzzleCursor;

    // Start is called before the first frame update
    void Start()
    {
        currentPuzzleGrid = FindObjectOfType<PuzzleGameObject>().currentGameplayGrid;
        currentPuzzleState = FindObjectOfType<PuzzleGameObject>().currentPuzzleState;
        currentPuzzleCursor = FindObjectOfType<PuzzleGameObject>().currentPuzzleCursor;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(EInputAxis.Fire1.ToString()))
        {
            ShiftGrid();
        }
        else
        {
            MoveCursor();
        }

    }

    private void ShiftGrid()
    {
        if (Input.GetButtonDown(EInputAxis.Horizontal.ToString()))
        {
            float axisVal = Input.GetAxis(EInputAxis.Horizontal.ToString());
            if (axisVal > 0)
            {
                currentPuzzleGrid.ShiftHorizontal(currentPuzzleCursor.gridPosY, 1);
            }
            if (axisVal < 0)
            {
                currentPuzzleGrid.ShiftHorizontal(currentPuzzleCursor.gridPosY, -1);
            }

        }
        if (Input.GetButtonDown(EInputAxis.Vertical.ToString()))
        {
            float axisVal = Input.GetAxis(EInputAxis.Vertical.ToString());
            if (axisVal > 0)
            {
                currentPuzzleGrid.ShiftVertical(currentPuzzleCursor.gridPosY, 1);
            }
            if (axisVal < 0)
            {
                currentPuzzleGrid.ShiftVertical(currentPuzzleCursor.gridPosY, -1);

            }
        }
    }

    private void MoveCursor()
    {
        if (Input.GetButtonDown(EInputAxis.Horizontal.ToString()))
        {
            currentPuzzleCursor.MoveHorizontal(Input.GetAxis(EInputAxis.Horizontal.ToString()));
        }
        if (Input.GetButtonDown(EInputAxis.Vertical.ToString()))
        {
            currentPuzzleCursor.MoveVertical(Input.GetAxis(EInputAxis.Vertical.ToString()));
        }

        if (Input.GetButtonDown(EInputAxis.Submit.ToString()))
        {
            //currentPuzzleCursor.CursorSelectToggle();
            Debug.Log("Submit Hit");
        }
    }


}
