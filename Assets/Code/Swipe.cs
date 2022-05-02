using UnityEngine;

public class Swipe : MonoBehaviour
{
    public Vector2 SwipeDelta { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }

    Vector2 startTouch;    
    bool isDraging;

    private void Update()
    {        
        SwipeLeft = false;
        SwipeRight = false;
        SwipeUp = false;
        SwipeDown = false;

        #region PC
        if (Input.GetMouseButtonDown(0))
        {     
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {            
            Reset();
        }
        #endregion

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {         
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {               
                Reset();
            }
        }

        SwipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touchCount > 0)
            {
                SwipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                SwipeDelta = (Vector2)Input.mousePosition - startTouch;                
            }
        }

        if (SwipeDelta.magnitude > 90)
        {
            float x = SwipeDelta.x;
            float y = SwipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y)) //left or right dir
            {
                if (x < 0)
                    SwipeLeft = true;
                else
                    SwipeRight = true;
            }
            else //up or down dir
            {
                if (y < 0)
                    SwipeDown = true;
                else
                    SwipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        SwipeDelta = Vector2.zero;
        isDraging = false;
    }

}
