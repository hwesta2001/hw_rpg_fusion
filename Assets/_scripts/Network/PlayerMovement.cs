using UnityEngine;
using UnityEngine.EventSystems;
using Fusion;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [Networked] byte GlobalTurnId { get; set; } // globalTurnId all same
    byte playerId; // network playerId ya da charnw-id
    [SerializeField] List<Hex> avaliableHexes = new();
    [SerializeField] LayerMask hexesLayer;
    enum State { begin, waitForMovement, moving, final }
    State state;

    byte moveCount;
    Collider[] cols = new Collider[7];

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
        if (Input.GetMouseButtonDown(0))
        {
            if (state != State.waitForMovement) return;
            //Raycast and get hittedhex
            //if (avaliableHexes.Contains(hittedhex))
            //{
            //    state = State.moving;
            MoveBegin();
            //}
        }
    }

    void MoveBegin()
    {
        state = State.moving;
        ////tr.DOMove...
        //.OnComplete(() =>
        //  state = State.final;
        //    MoveCount--;
        //);
    }

    void MoveTurnEnd()
    {
        print("Player" + playerId + " move turn ending.");
    }

    void CheckUnderHexMoveable()
    {

    }
}
