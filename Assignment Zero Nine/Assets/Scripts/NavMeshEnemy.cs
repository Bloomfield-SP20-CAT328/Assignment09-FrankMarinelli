using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshEnemy : Enemy
{
	static protected List<GameObject> navPatrolPoints = null;

	protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    override protected void Start()
	{
		print("starting NavEnemy. Hope it works.");
        if(navPatrolPoints == null)
		{
			navPatrolPoints = new List<GameObject>();
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("NavPatrolPoints"))
			{
				Debug.Log("Adding NavEnemy Patrol Point at: " + go.transform.position);
				navPatrolPoints.Add(go);
			}
		}

		GameObject hair = GameObject.Find("Hair");
		hair.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		ChangeToPatrolling();
	}

    override protected void OnPatrollingUpdate()
	{
        if(MoveTowardsTarget(walkingSpeed, patrollingInterestPoint) <= navMeshAgent.stoppingDistance)
		{
			SelectRandomPatrolPoint();
		}
	}

    override protected float MoveTowardsTarget(float speed, Vector3 target)
	{
		navMeshAgent.SetDestination(target);
		return Vector3.Distance(transform.position, targert);
	}

    override protected void SelectRandomPatrolPoint()
	{
		int choice = Random.Range(0, navPatrolPoints.Count);
		patrollingInterestPoint = navPatrolPoints[choice];
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
        Debug.Log("Nav Eenmy is navigating to patrol at point " + patrollingInterestPoint.name + " at this location " + patrollingInterestPoint.transform.position.ToString())
	}
}
