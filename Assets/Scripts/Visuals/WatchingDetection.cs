using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LudumDare50.Client.Game;

namespace LudumDare50.Client.Visuals
{
    public class WatchingDetection : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.GetComponent<Coworker>().StartWatching();
        }
    }
}

