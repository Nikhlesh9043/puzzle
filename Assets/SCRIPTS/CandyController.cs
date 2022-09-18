using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyController : MonoBehaviour
{
    private bool move = false;
    private string direction;
    public float speed = 4f;
    private bool LevelComplete = false;
    
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    private Ray rayUp;
    private Ray rayDown;
    private Ray rayRight;
    private Ray rayLeft;

    private Vector3 PossiblePositionUp;
    private Vector3 PossiblePositionDown;
    private Vector3 PossiblePositionRight;
    private Vector3 PossiblePositionLeft;

    private Vector3 targetPosition;
    private Animator animator;
    public GameObject Graphics;

    public PlatformSpawner platformSpawner;


    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 8 / 100;


        animator = gameObject.GetComponent<Animator>();
        animator.Play("PlayerSpawnAnimation", -1, 0f);

        GetPossiblePositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(transform.position == targetPosition)
            {
                move = false;
                GetPossiblePositions();
                //switch (direction)
                //{
                //    case "up":
                //        Graphics.transform.rotation = Quaternion.Euler(-90,0,0);
                //        break;
                //    case "down":
                //        Graphics.transform.rotation = Quaternion.Euler(-90, 0, 180);
                //        break;
                //    case "right":
                //        Graphics.transform.rotation = Quaternion.Euler(-90, 0, -90);
                //        break;
                //    case "left":
                //        Graphics.transform.rotation = Quaternion.Euler(-90, 0, 90);
                //        break;
                //    default:
                //        break;
                //}
                animator.Play("CollideUpAnimation", -1, 0f);
            }
            //switch (direction)
            //{
            //    case "up":
            //        gameObject.transform.Translate(Vector3.up * -speed * Time.deltaTime);
            //        break;
            //    case "down":
            //        gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
            //        break;
            //    case "right":
            //        gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            //        break;
            //    case "left":
            //        gameObject.transform.Translate(Vector3.right * -speed * Time.deltaTime);
            //        break;
            //    default:
            //        break;
            //}
        }

        //Touch Controls
        if (!move) {
            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                if (direction != "right")
                                {
                                    targetPosition = PossiblePositionRight;
                                    gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                                    move = true;
                                    direction = "right";
                                }
                            }
                            else
                            {   //Left swipe
                                if (direction != "left")
                                {
                                    targetPosition = PossiblePositionLeft;
                                    gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                                    move = true;
                                    direction = "left";
                                }
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                if (direction != "up")
                                {
                                    targetPosition = PossiblePositionUp;
                                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                                    move = true;
                                    direction = "up";
                                }
                            }
                            else
                            {   //Down swipe
                                if (direction != "down")
                                {
                                    targetPosition = PossiblePositionDown;
                                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                                    move = true;
                                    direction = "down";
                                }
                            }
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                    }
                }
            }
        }
        //Button controlls
        if (!move)
        {
            if (Input.GetKeyDown("w"))
            {
                targetPosition = PossiblePositionUp;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                move = true;
                direction = "up";
            }
            if (Input.GetKeyDown("s"))
            {
                targetPosition = PossiblePositionDown;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                move = true;
                direction = "down";
            }
            if (Input.GetKeyDown("a"))
            {
                targetPosition = PossiblePositionLeft;
                gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                move = true;
                direction = "left";
            }
            if (Input.GetKeyDown("d"))
            {
                targetPosition = PossiblePositionRight;
                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                move = true;
                direction = "right";
            }
        }

        if (LevelComplete)
        {
            
        }

    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "home")
        {
            move = false;
            animator.Play("CelebrateAnimation", -1, 0f);
            //LevelComplete = true;
            transform.position = other.gameObject.transform.position;
            Invoke("ChangePlatform", 0.5f);
            //ChangePlatform();
        }
    }

    private void ChangePlatform()
    {
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().LevelComplete();
    }
    
    
    private void GetPossiblePositions()
    {
        rayUp = new Ray(transform.position, Vector3.forward);
        rayDown = new Ray(transform.position, -Vector3.forward);
        rayRight = new Ray(transform.position, Vector3.right);
        rayLeft = new Ray(transform.position, -Vector3.right);
        //RaycastHit hit;

        if (Physics.Raycast(rayUp, out RaycastHit hitUp))
        {
            if (hitUp.collider.gameObject.tag == "platform")
            {
                PossiblePositionUp = hitUp.point + new Vector3(0,0,-0.5f);
            }
        }
        if (Physics.Raycast(rayDown, out RaycastHit hitDown))
        {
            if (hitDown.collider.gameObject.tag == "platform")
            {
                PossiblePositionDown = hitDown.point + new Vector3(0, 0, 0.5f);
            }
        }
        if (Physics.Raycast(rayRight, out RaycastHit hitRight))
        {
            if (hitRight.collider.gameObject.tag == "platform")
            {
                PossiblePositionRight = hitRight.point + new Vector3(-0.5f, 0, 0);
            }
        }
        if (Physics.Raycast(rayLeft, out RaycastHit hitLeft))
        {
            if (hitLeft.collider.gameObject.tag == "platform")
            {
                PossiblePositionLeft = hitLeft.point + new Vector3(0.5f, 0, 0);
            }
        }
    }
}
