using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayByInstancing : MonoBehaviour {

	public SkinnedMeshRenderer skinnedMeshRenderer;
	public GameObject refGameObject;
	public Transform[] instances;
	public float instanceScale = 1;

	Mesh bakedSkinnedMesh;

	Vector3[] positionOffset;
	Matrix4x4[] translateMats;

	int instanceCount = 0;
	Vector3 scaleVec = Vector3.one;

	Transform refTrans;

	// Use this for initialization
	void Start()
	{
		Debug.Log("<color=#FF0000>材质是否支持 instancing " + skinnedMeshRenderer.sharedMaterial.enableInstancing + "</color>");

		Debug.Log("<color=#FF0000>系统是否支持 instancing " + SystemInfo.supportsInstancing + "</color>");

		InitComponent();
	}

	// Update is called once per frame
	void Update()
	{

		skinnedMeshRenderer.BakeMesh(bakedSkinnedMesh);

		UpdateMatrix();

		Graphics.DrawMeshInstanced(bakedSkinnedMesh, 0, skinnedMeshRenderer.sharedMaterial, translateMats, translateMats.Length, null, UnityEngine.Rendering.ShadowCastingMode.Off, false);

	}

	void InitComponent()
	{
		refTrans = refGameObject.transform;
		instanceCount = instances.Length;

		scaleVec = instanceScale * Vector3.one;

		translateMats = new Matrix4x4[instanceCount];
		positionOffset = new Vector3[instanceCount];

		for (int i = 0; i < instanceCount; i++)
		{
			positionOffset[i] = instances[i].position - refTrans.position;
		}

		bakedSkinnedMesh = new Mesh();

	}
		
	void UpdateMatrix()
	{
		for (int i = 0; i < instanceCount; i++)
		{
			translateMats[i] = Matrix4x4.TRS(positionOffset[i] + refTrans.position, refTrans.rotation, scaleVec);
		}
	}
}
