using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

namespace Hw.Dice
{
    public class DiceRoll : MonoBehaviour
    {
        [field: SerializeField] public int RolledDice { get; private set; }
        [Tooltip("Must Writte Dice Face Count")] public int faceCount = 6;
        [SerializeField, Tooltip("element 0 d1, element 1 d2 ... yani RolledDice=index+1 ")] Vector3[] uppserFaceRotations;
        [SerializeField] Transform gfxMeshTransform;

        [Space]
        [SerializeField, Range(.05f, 3f)] float _time = .5f;
        [SerializeField, Range(10f, 360f)] float _str = 120;
        [SerializeField, Range(1, 20)] int _vibrate = 7;
        [SerializeField, Range(10, 180)] float _randm = 90;
        [Space]
        [SerializeField, Range(.05f, 1f)] float _lasttime = .5f;
        [HideInInspector] public bool rollingNow;
        public Action OnDiceRollAnimEnd;
        void OnValidate()
        {
            if (gfxMeshTransform == null) gfxMeshTransform = GetComponentInChildren<MeshRenderer>().transform;
            if (faceCount <= 0) return;
        }

        public void RollDice(int randomDice)  //dice onceden belirlenir.
        {
            RolledDice = randomDice;
            rollingNow = true;
            gfxMeshTransform.DOShakeRotation(_time, _str, _vibrate, _randm, false, ShakeRandomnessMode.Harmonic)
            .OnComplete(() => gfxMeshTransform.DOLocalRotate(uppserFaceRotations[randomDice - 1], _lasttime).SetEase(Ease.OutElastic)
            .OnComplete(() =>
            {
                rollingNow = false;
                DebugText.ins.AddText("Rolled:" + RolledDice);
                OnDiceRollAnimEnd?.Invoke();
            }));
        }


    }
}
