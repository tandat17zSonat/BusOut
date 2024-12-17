using UnityEngine;

public class DirectionController : MonoBehaviour
{
    [SerializeField] GameObject objTop;
    [SerializeField] GameObject objBottom;
    [SerializeField] GameObject objLeft;
    [SerializeField] GameObject objRight;

    public Vector2 GetDirectionVector(Vector2 oldDirection, GameObject collision, Vector2 collisionPosition, Vector2 targetPosition)
    {
        if (collision == objBottom)
        {
            if (oldDirection.x >= 0)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }

        else if (collision == objRight || collision == objLeft)
        {
            return Vector2.up;
        }

        else if (collision == objTop)
        {
            if (collisionPosition.x <= targetPosition.x)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }
        Debug.LogWarning("DirectionController: don't find direction vector");
        return Vector2.zero;
    }
}
