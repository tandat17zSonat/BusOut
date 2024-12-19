using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour
{
    [SerializeField] GameObject objTop;
    [SerializeField] GameObject objBottom;
    [SerializeField] GameObject objLeft;
    [SerializeField] GameObject objRight;

    Vector2 pointLT, pointRT, pointLB, pointRB;
    private void Start()
    {
        var boxColliderTop = objTop.GetComponent<BoxCollider2D>();
        var center = boxColliderTop.offset;
        var size = boxColliderTop.size;
        pointLT = ConvertToWorldSpace(objTop, new Vector2(center.x - size.x/2, center.y));
        pointRT = ConvertToWorldSpace(objTop, new Vector2(center.x + size.x/2, center.y));

        var boxColliderBottom = objBottom.GetComponent<BoxCollider2D>();
        center = boxColliderBottom.offset;
        size = boxColliderBottom.size;
        pointLB = ConvertToWorldSpace(objBottom, new Vector2(center.x - size.x/2, center.y));
        pointRB = ConvertToWorldSpace(objBottom, new Vector2(center.x + size.x/2, center.y));

    }

    public List<Vector2> GetListPointToTarget(Vector2 direction, GameObject collision, Vector2 collisionPosition, Vector2 targetPosition)
    {
        var res = new List<Vector2>();
        if (collision == objBottom)
        {
            var yBottom = pointLB.y;
            res.Add(new Vector2(collisionPosition.x, yBottom));
            if (direction.x >= 0)
            {
                res.Add(pointRB);
                res.Add(pointRT);
            }
            else
            {
                res.Add(pointLB);
                res.Add(pointLT);
            }
        }
        else if (collision == objRight)
        {
            var xRight = pointRB.x;
            res.Add(new Vector2(xRight, collisionPosition.y));
            res.Add(pointRT);
        }
        else if (collision == objLeft)
        {
            var xLeft = pointLB.x;
            res.Add(new Vector2(xLeft, collisionPosition.y));
            res.Add(pointLT);
        }
        else if( collision == objTop)
        {
            res.Add(new Vector2(collisionPosition.x, GetYTop()));
        }

        
        res.Add(new Vector2(targetPosition.x, GetYTop()));

        res.Add(targetPosition);
        return res;
    }

    private Vector2 ConvertToWorldSpace(GameObject obj, Vector2 point)
    {
        var pos = obj.transform.TransformPoint(point);
        return pos;
    }

    public float GetYTop()
    {
        return pointLT.y;
    }
}
