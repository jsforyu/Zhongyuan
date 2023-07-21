using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ïà»ú¸úËæ
/// </summary>
public class CameraMove : MonoBehaviour
{
    public void Start()
    {
        _focusArea = new FocusArea(_target.bounds, _focusSize);
        _targetPos = transform.position;
    }

    public void Update()
    {
        if (_target == null) return;
        _focusArea.Update(_target.bounds);
        if (_focusArea.velocity != Vector2.zero)
        {
            _targetPos += (Vector3)_focusArea.velocity;
        }
        transform.position = Vector3.Lerp(transform.position, _targetPos, _smoothSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 1, 0.5f, 0.5f);
        Gizmos.DrawCube(_focusArea.center, _focusSize);
    }

    public Collider2D _target;
    public Vector2 _focusSize;
    public float _smoothSpeed = 20;

    FocusArea _focusArea;
    Vector3 _targetPos;

}

public class FocusArea
{
    public FocusArea(Bounds focusBounds, Vector2 size)
    {
        left = focusBounds.center.x - size.x / 2;
        right = focusBounds.center.x + size.x / 2;
        top = focusBounds.min.y + size.y;
        bottom = focusBounds.min.y;

        center = new Vector2((left + right) / 2, (top + bottom) / 2);
        velocity = Vector2.zero;
    }

    public void Update(Bounds targetBounds)
    {
        float shiftX = 0;
        if (targetBounds.min.x < left)
        {
            shiftX = targetBounds.min.x - left;
        }
        else if (targetBounds.max.x > right)
        {
            shiftX = targetBounds.max.x - right;
        }
        left += shiftX;
        right += shiftX;

        float shiftY = 0;

        if (targetBounds.max.y > top)
        {
            shiftY = targetBounds.max.y - top;
        }
        else if (targetBounds.min.y < bottom)
        {
            shiftY = targetBounds.min.y - bottom;
        }
        top += shiftY;
        bottom += shiftY;
        center = new Vector2((left + right) / 2, (top + bottom) / 2);
        velocity = new Vector2(shiftX, shiftY);

    }

    public float left;
    public float right;
    public float top;
    public float bottom;
    public Vector2 center;
    public Vector2 velocity;
}