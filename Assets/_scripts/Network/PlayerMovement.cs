using UnityEngine;
using Fusion;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerMovement : NetworkBehaviour
{

    byte playerId; // network playerId ya da charnw-id
    [SerializeField] List<Hex> avaliableHexes = new();
    [SerializeField] LayerMask hexesLayer;
    enum State { begin, waitForMovement, moving, final }
    [SerializeField] State state;
    [SerializeField] byte moveCount;
    Transform tr;
    public override void Spawned()
    {
        if (!HasStateAuthority) return;
        tr = transform;
        Turn.OnTurnChanged += MoveTurnControl;
    }

    public void Despawned()
    {
        if (!HasStateAuthority) return;
        Turn.OnTurnChanged -= MoveTurnControl;
    }


    void MoveTurnControl(TurnState turnState)
    {
        if (!HasStateAuthority) return;
        if (turnState == TurnState.moving)
        {
            SetMoveCount();
        }
        else
        {
            HexHighlights.ins.DisableHexHighlights();
        }
    }

    void SetMoveCount()
    {
        if (DiceControl.ins.RolledDice == 0)
        {
            DiceControl.ins.RollButton();
        }
        MoveCount = DiceControl.ins.RolledDice;
    }

    public byte MoveCount
    {
        get
        {
            return moveCount;
        }
        set
        {
            moveCount = value;
            if (moveCount > 0)
            {
                MovableHexes();
            }
            else
            {
                MoveTurnEnd();
            }
        }
    }

    void MovableHexes()
    {
        state = State.begin;
        avaliableHexes.Clear();
        HexHighlights.ins.DisableHexHighlights();

        Collider[] cols = Physics.OverlapSphere(transform.position, 3.25f, hexesLayer);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                Hex colhex = cols[i].GetComponentInChildren<Hex>();
                if (colhex.moveable)
                {
                    if (!avaliableHexes.Contains(colhex))
                    {
                        avaliableHexes.Add(colhex);
                        HexHighlights.ins.MoveHexHighlight(i, colhex.pos);
                    }

                }
            }
        }
        state = State.waitForMovement;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && HasStateAuthority)
        {
            //if (MouseDeltaisNotZero()) return;
            if (state != State.waitForMovement) return;
            if (RayCastEvent.ins.SelectionCast())
            {
                if (RayCastEvent.ins.HittedObject.TryGetComponent(out Hex hex))
                {
                    state = State.moving;
                    if (avaliableHexes.Contains(hex))
                    {
                        MoveBegin(hex);
                    }
                }
            }
        }
    }

    void MoveBegin(Hex hex)
    {
        HexHighlights.ins.DisableHexHighlights();
        if (MoveCount == 1) state = State.final;
        tr.DOLocalJump(hex.pos, 3, 1, duration: Random.Range(.5f, 1f), false).SetEase(Ease.InQuart).OnComplete(() =>
        {
            MoveCount--;
        });
        ;
    }

    void MoveTurnEnd()
    {
        if (!HasStateAuthority) return;
        if (state == State.final)
        {
            Turn.ins.TURN_STATE = TurnState.events;
        }
    }

    bool MouseDeltaisNotZero()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse Y")) > Mathf.Epsilon
            || Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Epsilon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
