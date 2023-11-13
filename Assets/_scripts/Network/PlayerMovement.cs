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
    State state;

    byte moveCount;
    Collider[] cols = new Collider[7];
    Transform tr;
    void OnEnable()
    {
        if (!HasStateAuthority) return;
        tr = transform;
        Turn.OnTurnChanged += MoveTurnControl;
    }

    void OnDisable()
    {
        if (!HasStateAuthority) return;
        Turn.OnTurnChanged -= MoveTurnControl;
    }


    void MoveTurnControl(TurnState turnState)
    {
        if (turnState == TurnState.moveStart)
        {
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
            if (moveCount >= 0)
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

        //sapherecast and get hexes
        //remove onhex
        //remove nonmoveable hexes
        //avaliableHexes.Add(Hex);

        int count = Physics.OverlapSphereNonAlloc(transform.position, 1.5f, cols, hexesLayer);
        if (cols.Length > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Hex colhex = cols[i].GetComponentInChildren<Hex>();
                if (colhex.moveable)
                {
                    avaliableHexes.Add(colhex);
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
        print("Player" + playerId + " move turn ending.");
    }

    void CheckUnderHexMoveable()
    {

    }
}
