using UnityEngine;

public class MobleInput : MonoBehaviour
{
    //public
    public static MobleInput Instance { get; private set; }
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public Vector2 startTouch, endTouch, swipeDelta;

    //private
    private const float deadZone = 100f;

    //enum
    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    private void Awake()
    {
        Instance = this;
    }
    // thuat toan //
    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouch = Input.mousePosition;
            swipeDelta = endTouch - startTouch;

            if (swipeDelta.magnitude > deadZone)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                }
                else
                {
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }
            }
        }
    }

    // lấy vị trí dựa theo giá trị enum
    public Vector3 GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                return Vector3.zero;
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
            default:
                return Vector3.zero;
        }
    }
    public Direction GetSwipeDirection()
    {
        if (swipeDelta == Vector2.zero)
            return Direction.None;

        float x = swipeDelta.x;
        float y = swipeDelta.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x < 0)
                return Direction.Left;
            else
                return Direction.Right;
        }
        else
        {
            if (y < 0)
                return Direction.Down;
            else
                return Direction.Up;
        }
    }
}
