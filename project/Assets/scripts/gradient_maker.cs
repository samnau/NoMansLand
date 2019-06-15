using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gradient_maker : MonoBehaviour {
	public string sortingLayer;
    public int sortOrder = 0;
    public Color GradientBottom = new Color(0.2078431373f, 0.4274509804f, 0.2078431373f, 1.0f);
    public Color GradientTop = new Color(0.137254902f, 0.1215686275f, 0.1254901961f, 1.0f);

    void Awake(){
		for(int i = 0; i < SortingLayer.layers.Length; i++){
			if(SortingLayer.layers[i].name == sortingLayer)
				this.GetComponent<Renderer>().sortingLayerName = sortingLayer;
               this.GetComponent<Renderer>().sortingOrder = sortOrder;
		}
	}
	// Use this for initialization
	void Start () {
		MeshFilter viewedModelFilter = (MeshFilter)GetComponent("MeshFilter");
		Mesh mesh = viewedModelFilter.mesh;
		//Color GrassGreen = new Color (0.2078431373f, 0.4274509804f, 0.2078431373f, 1.0f);
		Color[] colors = new Color[mesh.vertices.Length];
		//Color TopColor = new Color(0.137254902f,0.1215686275f,0.1254901961f,1.0f);
		//Color BottomColor = new Color (0.1607843137f, 0.333f, 0.1607843137f, 1.0f);
		colors[0] = GradientBottom;
		colors[1] = GradientTop;
		colors[2] = GradientBottom;
		colors[3] = GradientTop;
		mesh.colors = colors;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
