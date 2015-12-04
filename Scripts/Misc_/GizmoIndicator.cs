using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class GizmoIndicator : MonoBehaviour {
	/// <summary>
	/// The centers of wired cubes
	/// </summary>
	public List<Vector3> Centers;
	/// <summary>
	/// The sizes of wired cubes
	/// </summary>
	public List<Vector3> Sizes;


	public Pair<List<Vector3>,List<Vector3> > _list = new Pair<List<Vector3>,List<Vector3> >();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDrawGizmos(){
	//Drawing Wired Cubes
		_list.First = Centers;
		_list.Second = Sizes;
		for(int i = 0;i<Sizes.Count;i++) {
			Gizmos.DrawWireCube(_list.First[i],_list.Second[i]);
				}
			
			
	}
}
