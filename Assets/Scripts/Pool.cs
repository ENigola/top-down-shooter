using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour{
	// Dead objects at the start, alive at the end.
	// Index points at the last dead object.

	public GameObject prefab;
	public int size;

	private List<GameObject> pool;
	private int index;

	void Start() {
		pool = new List<GameObject>();
		for (int i = 0; i < size; i++) {
			GameObject go = Instantiate(prefab);
			go.SetActive(false);
			pool.Add(go);
		}
		index = size - 1;
	}

	public GameObject GetObject() {
		GameObject returnObject = pool[index];
		returnObject.SetActive(true);
		index--;
		return returnObject;
	}

	public void ReleaseObject(GameObject obj) {
		obj.SetActive(false);
		int objIndex = pool.IndexOf(obj);
		if (objIndex != index + 1) {
			GameObject tmp = pool[index + 1];
			pool[index + 1] = obj;
			pool[objIndex] = tmp;
		}
		index++;
	}
}
