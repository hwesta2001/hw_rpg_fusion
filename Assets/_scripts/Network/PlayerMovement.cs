using UnityEngine;
using Fusion;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;

public class PlayerMovement : NetworkBehaviour
{

    byte playerId; // network playerId ya da charnw-id
    [SerializeField] List<Hex> avaliableHexes = new();
    [SerializeField] LayerMask hexesLayer;
    enum State { begin, waitForMovement, moving, final }
    State state;
    [SerializeField]
    byte moveCount;
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
        if (turnState == TurnState.moving)
        {
            Debug.Log(" turn state moving??");
            MoveCount = DiceControl.ins.RolledDice;
        }
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
            if (state != State.waitForMovement) return;
            if (RayCastEvent.ins.SelectionCast())
            {
                if (RayCastEvent.ins.HittedObject.TryGetComponent(out Hex hex))
                {
                    if (avaliableHexes.Contains(hex))
                    {
                        state = State.moving;
                        MoveBegin(hex);
                    }
                }
            }
        }
    }

    void MoveBegin(Hex hex)
    {
        HexHighlights.ins.DisableHexHighlights();
        state = State.moving;
        tr.DOLocalJump(hex.pos, 2, 1, 1, false).SetEase(Ease.InQuart)
        .OnComplete(() =>
        {
            state = State.final;
            MoveCount--;
        });
    }

    void MoveTurnEnd()
    {
        Debug.Log("Player" + playerId + " move turn ending.");
        Turn.ins.TURN_STATE = TurnState.events;
    }
}
