using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PipePiece : MonoBehaviour,  IPointerClickHandler//, IPointerEnterHandler,IPointerExitHandler
{
    // (script referenced from a series of youtube tutorials: https://www.youtube.com/playlist?list=PLQzQtnB2ciXTv60MCjghfyet-h3RNhjDG)
    // (scratch that, script ACTUALLY referenced from a series of youtube tutorials: https://www.youtube.com/watch?v=ltmfKjvYmww)

    public float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;

    public int PossibleRots = 1;

    public bool ignoreThis;

    public bool isPlaced = false;

    public Image currentImage;
    public Sprite correctImage;
    public Sprite incorrectImage;

    public PipeMiniGame pipeMiniGame;


    //Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (PossibleRots == 2)
        {
            Debug.Log(transform.eulerAngles.z + " = " + correctRotation[0] + " or " + correctRotation[1]);

            if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                currentImage.sprite = correctImage;
                pipeMiniGame.CorrectMove();
            }
            else if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[1] && isPlaced == false)
            {
                isPlaced = true;
                currentImage.sprite = correctImage;
                pipeMiniGame.CorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                currentImage.sprite = incorrectImage;
                pipeMiniGame.WrongMove();
            }
        }
        else if (PossibleRots == 1)
        {
            Debug.Log(transform.eulerAngles.z + " = " + correctRotation[0]);

            if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                currentImage.sprite = correctImage;
                pipeMiniGame.CorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                currentImage.sprite = incorrectImage;
                pipeMiniGame.WrongMove();
            }
        }
    }

    private void OnEnable()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (PossibleRots == 2)
        {
            Debug.Log(transform.eulerAngles.z + " = " + correctRotation[0] + " or " + correctRotation[1]);

            if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                currentImage.sprite = correctImage;
                pipeMiniGame.CorrectMove();
            }
            else if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[1] && isPlaced == false)
            {
                isPlaced = true;
                currentImage.sprite = correctImage;
                pipeMiniGame.CorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                currentImage.sprite = incorrectImage;
                pipeMiniGame.WrongMove();
            }
        }
        else if (PossibleRots == 1)
        {
            Debug.Log(transform.eulerAngles.z + " = " + correctRotation[0]);

            if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                currentImage.sprite = correctImage;
                pipeMiniGame.CorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                currentImage.sprite = incorrectImage;
                pipeMiniGame.WrongMove();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.Rotate(new Vector3(0, 0, 90));

        if (ignoreThis == false )
        {
            if (PossibleRots == 2)
            {
                if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
                {
                    isPlaced = true;
                    currentImage.sprite = correctImage;
                    pipeMiniGame.CorrectMove();
                }
                else if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[1] && isPlaced == false)
                {
                    isPlaced = true;
                    currentImage.sprite = correctImage;
                    pipeMiniGame.CorrectMove();
                }
                else if (isPlaced == true)
                {
                    isPlaced = false;
                    currentImage.sprite = incorrectImage;
                    pipeMiniGame.WrongMove();
                }
            }
            //else if (PossibleRots == 4)
            //{
            //    if (Mathf.Abs(transform.rotation.eulerAngles.z) == correctRotation[0] || Mathf.Abs(transform.rotation.eulerAngles.z) == correctRotation[1] 
            //        || Mathf.Abs(transform.rotation.eulerAngles.z) == correctRotation[2] || Mathf.Abs(transform.rotation.eulerAngles.z - correctRotation[3]) < 0.001 
            //        && isPlaced == false)
            //    {
            //        isPlaced = true;
            //        currentImage.sprite = correctImage;
            //        pipeMiniGame.CorrectMove();
            //    }
            //    //else if (isPlaced == true)
            //    //{
            //    //    isPlaced = false;
            //    //    currentImage.sprite = incorrectImage;
            //    //    pipeMiniGame.WrongMove();
            //    //}
            //}
            else if (PossibleRots == 1)
            {
                if (Mathf.Abs(transform.eulerAngles.z) == correctRotation[0] && isPlaced == false)
                {
                    isPlaced = true;
                    currentImage.sprite = correctImage;
                    pipeMiniGame.CorrectMove();
                }
                else if (isPlaced == true)
                {
                    isPlaced = false;
                    currentImage.sprite = incorrectImage;
                    pipeMiniGame.WrongMove();
                }
            }
        }
        else
        {
            isPlaced = false;
            currentImage.sprite = incorrectImage;
        }
    }
}
