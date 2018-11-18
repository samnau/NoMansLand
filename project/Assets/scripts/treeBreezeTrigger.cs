using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBreezeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            //var tree = GameObject.FindGameObjectWithTag("tree");
            var treeArray = GameObject.FindGameObjectsWithTag("tree");
            foreach(GameObject treeItem in treeArray)
            {
                var breezeController = treeItem.GetComponent<TreeBreezeController>();
                breezeController.startWind = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
