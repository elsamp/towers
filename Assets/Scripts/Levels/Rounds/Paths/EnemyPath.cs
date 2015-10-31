using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

	public EnemyPathNode[] pathNodes;


	public EnemyPathNode getNextNode(int currentNodeIndex){

		if(pathNodes.Length > currentNodeIndex + 1){
			return pathNodes[currentNodeIndex + 1];
		}

		return null;
	}
}
