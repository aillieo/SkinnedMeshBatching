using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayByMeshSharing : MonoBehaviour {

	public SkinnedMeshRenderer skinnedMeshRenderer;
	public GameObject[] instances;

	Material matForArray;
	Mesh meshForArray;

	// Use this for initialization
	void Start()
	{
		if(skinnedMeshRenderer != null)
		{
			InitComponent();
		}
	}

	// Update is called once per frame
	void Update()
	{
		meshForArray.Clear();
		skinnedMeshRenderer.BakeMesh(meshForArray);
	}

	void InitComponent()
	{
		matForArray = skinnedMeshRenderer.material;
		meshForArray = new Mesh();

		// 强行把instance的scale改为和mainObj一样大
		Vector3 scale = skinnedMeshRenderer.gameObject.transform.lossyScale;
		Vector3 localScale = new Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z);

		for (int i = 0; i < instances.Length; i++)
		{
			if(instances[i] != null)
			{
				InitInstance(instances[i], meshForArray, matForArray);
				instances[i].transform.localScale = localScale;
			}
		}
	}


	void InitInstance(GameObject instance, Mesh mesh, Material mat)
	{
		MeshFilter mf = instance.GetComponent<MeshFilter>();
		if (mf == null)
		{
			mf = instance.AddComponent<MeshFilter>();
		}
		mf.sharedMesh = meshForArray;

		MeshRenderer mr = instance.GetComponent<MeshRenderer>();
		if (mr == null)
		{
			mr = instance.AddComponent<MeshRenderer>();
		}
		mr.sharedMaterial = matForArray;
	}
}
