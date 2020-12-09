using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BP.Almighty
{
    public class Minion : MonoBehaviour
    {
        [SerializeField] private float dmg = 100f;
        private Player enemyPlayer;
        private bool isActivated = false;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (isActivated && enemyPlayer)
            {
                CheckGoalDistance();
            }
        }

        public void SetEnemy(Player newEnemy)
        {
            enemyPlayer = newEnemy;
            ApproachGoal(newEnemy.transform.position);
            isActivated = true;
        }

        private void ApproachGoal(Vector3 goalPosition)
        {
            agent.destination = goalPosition;
        }

        private void CheckGoalDistance()
        {
            if(agent.remainingDistance < 0.9f)
            {
                enemyPlayer.DoDmg(dmg);
                Death();
            }
        }

        private void Death()
        {
            //TODO replace with repool
            isActivated = false;
            Destroy(gameObject);
        }
    }
}


