using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToLocation : MonoBehaviour
{
    [SerializeField] bool tagertPosIsLocalSpace;
    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed = 2;
    [Space, SerializeField] Transform tr;
    [SerializeField] bool loop = false;
    [SerializeField] Ease ease = Ease.InQuad;
    public JumpToLocation(Transform tr, Vector3 targetPos, bool? tagertPosIsLocalSpace = null, float? jumpSpeed = null, float? speed = null)
    {
        this.tr = tr;
        this.targetPos = targetPos;
        this.speed = speed ?? .5f;
        this.tagertPosIsLocalSpace = tagertPosIsLocalSpace ?? true;
        this.jumpSpeed = jumpSpeed ?? 1;
        this.ease = Ease.InQuad;
        DoStartJump();
    }

    private void OnValidate()
    {
        if (tr == null) tr = transform;
    }



    [ContextMenu("DoJumpStart")]
    void DoStartJump()
    {
        if (tagertPosIsLocalSpace)
        {
            if (loop)
            {
                tr.DOLocalJump(tr.position + targetPos, jumpSpeed, 1, speed, false).SetLoops(2, LoopType.Yoyo)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        DoStartJump();
                    });
            }
            else
            {
                tr.DOLocalJump(tr.position + targetPos, jumpSpeed, 1, speed, false).SetEase(ease);
            }
        }
        else
        {
            if (loop)
            {
                tr.DOLocalJump(targetPos, jumpSpeed, 1, speed, false).SetLoops(2, LoopType.Yoyo)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        DoStartJump();
                    });
            }
            else
            {
                tr.DOLocalJump(targetPos, jumpSpeed, 1, speed, false).SetEase(ease);
            }
        }
    }
}
