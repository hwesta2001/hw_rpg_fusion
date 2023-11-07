using UnityEngine;
using UnityEngine.EventSystems;
using Fusion;

public class PlayerMovement : MonoBehaviour{
  [Networked] byte GlobalTurnId {get;set;} // globalTurnId all same
  byte playerId; // network playerId ya da charnw-id
  List<Hex> avaliableHexes=new();
  enum State {begin, waitForMovement, moving, final}
  State state
  byte moveCount;
  public byte MoveCount{
    get{
      return moveCount;
    }
    set{
      moveCount=value;
      if(moveCount>=0){
        MovableHexes();
      }
      else{
        MoveTurnEnd();
      }
    }
  }
  
  void MovableHexes(){
    state=State.begin;
    avaliableHexes.Clear();
    //sapherecast and get hexes
    //remove onhex
    //remove nonmoveable hexes
    avaliableHexes.Add(Hex);
    state=State.waitForMovement;
  }
  
  void Update(){
    if(Input.GetMouseDown(0)){
      if(state!=State.waitForMovement) return;
      //Raycast and get hittedhex
      if (avaliableHexes.Contains(hittedhex)){
        state=State.moving;
        MoveBegin();
      }
    }
  }

  void MoveBegin(){
    state=State.moving;
    //tr.DOMove...
    .OnComplete(()=> 
      state=State.final;
      MoveCount--;
    );
  }

  void MoveTurnEnd(){
    print("Player"+playerId+" move turn ending.");
  }
}
