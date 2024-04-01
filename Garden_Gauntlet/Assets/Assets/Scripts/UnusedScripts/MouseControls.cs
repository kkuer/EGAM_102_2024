using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    public enum MouseStates
    {
        General,
        FountainMinigame,
        FlowerMinigame
    }
    public MouseStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {

            case MouseStates.General:
                UpdateGeneral();

                break;
            case MouseStates.FountainMinigame:
                UpdateFountainMinigame();

                break;
            case MouseStates.FlowerMinigame:
                UpdateFlowerMinigame();
                break;
        }

        // get mouse position
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void UpdateGeneral()
    {
        // get mouse position
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // switch states when clicking on a thing
    }

    void UpdateFountainMinigame()
    {
        // get mouse position
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // if clicking on a thing with the frog tag, have it follow position while m1 held down
    }

    void UpdateFlowerMinigame()
    {
        // get mouse position
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // if clicking on a thing with the flower tag, have it follow position & snap into place when let go
    }
}
