using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

	public EnemyPathNode[] pathNodes;

	private static EnemyPath instance;


	public static EnemyPath Instance
	{
		get{return instance;}
	}

	public void Init () {
	
		if(instance == null){
			instance = this;
		}
	}

	public EnemyPathNode getNextNode(int currentNodeIndex){

		if(pathNodes.Length > currentNodeIndex + 1){
			return pathNodes[currentNodeIndex + 1];
		}

		return null;
	}
}
