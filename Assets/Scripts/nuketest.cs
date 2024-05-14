using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuketest : MonoBehaviour
{
    public int maxNumLevels = 12;
	int maxLevels;
	public float expansionAngle = .25f;
	public float topAddition = .9f;
	public int delay = 1;
	public int decayTime = 4;
	public float timeBetweenFrames = .005f;
	public float lift = .001f;
	
	float deltaY;
	float lastFrameTime;
	
	int windUp;
	int delayCount;
	int frameCount;
	
	Vector3[] profile;
	int levels;
	
	
	void CalcGeom() {
		MeshFilter mf = (MeshFilter) GetComponent("MeshFilter");
		Vector3[] verts = mf.mesh.vertices;
		Vector2[] uv = mf.mesh.uv;
		
		verts[0] = new Vector3(0, profile[0].y, 0);
		verts[levels * 8 + 1] = new Vector3(0, profile[levels - 1].y, 0);		
		
		for (int level = 0; level < levels; level++) {
			for (int sector = 0; sector < 8; sector++) {
				float x = Mathf.Cos(sector * Mathf.PI / 4) * profile[level].x;
				float y = profile[level].y;
				float z = Mathf.Sin(sector * Mathf.PI / 4) * profile[level].x;
				
				verts[level * 8 + sector + 1] = new Vector3(x, y, z);
				uv[level * 8 + sector + 1] = new Vector2(level, sector);
			}
		}
		
		int[] triangles = new int[(8 * 3 * 2) + ((levels - 1) * 8 * 3 * 2)];
		
		for (int i = 0; i < 8; i++) {
			triangles[i * 3 + 0] = 0;
			triangles[i * 3 + 1] = 1 + i;
			triangles[i * 3 + 2] = ((1 + i) % 8) + 1;
		}
		
		int prevLevel = 8 * 3;
		
		for (int level = 0; level < (levels - 1); level++) {
			for (int i = 0; i < 8; i++) {
				int a = 1 + (level * 8) + i;
				int b = 1 + ((level + 1) * 8) + i;
				int c = 1 + (level * 8) + ((i + 1) % 8);
				int d = 1 + ((level + 1) * 8) + ((i + 1) % 8);
				
				triangles[prevLevel + (level * 8 * 6) + (i * 6) + 0] = a;
				triangles[prevLevel + (level * 8 * 6) + (i * 6) + 1] = b;
				triangles[prevLevel + (level * 8 * 6) + (i * 6) + 2] = c;
				triangles[prevLevel + (level * 8 * 6) + (i * 6) + 3] = c;
				triangles[prevLevel + (level * 8 * 6) + (i * 6) + 4] = b;
				triangles[prevLevel + (level * 8 * 6) + (i * 6) + 5] = d;
			}
		}
		
		prevLevel += (levels - 1) * 6 * 8;
		int topIndex = 1 + levels * 8;
		
		for (int i = 0; i < 8; i++) {
			int a = 1 + ((levels - 1) * 8) + ((i + 1) % 8);
			int b = 1 + ((levels - 1) * 8) + i;
			int c = topIndex;
	
			triangles[prevLevel + (i * 3) + 0] = a;
			triangles[prevLevel + (i * 3) + 1] = b;
			triangles[prevLevel + (i * 3) + 2] = c;
		}
		
		mf.mesh.vertices = verts;
		mf.mesh.uv = uv;
		mf.mesh.triangles = triangles;
	}
	
	// Use this for initialization
	void Start () {
		maxLevels = maxNumLevels;
		profile = new Vector3[maxLevels];
		profile[0] = new Vector3(.1f, 0, 0);
		profile[1] = new Vector3(.2f, .1f, 0);
		profile[2] = new Vector3(.2f, .2f, 0);
		profile[3] = new Vector3(.1f, .3f, 0);
		levels = 4;
		
		CalcGeom();
		delayCount = delay;
		frameCount = 0;
		lastFrameTime = Time.time;
	}
	
	void ShuffleDown(int num) {
		if (num > 0) {
			for (int i = num; i < levels; i++) {
				profile[i - num] = profile[i];
			}
			
			levels = levels - num;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (delayCount > 0) {
			delayCount--;
			return;
		}
		
		float currTime = Time.time;
		
		if (currTime < (lastFrameTime + timeBetweenFrames)) {
			return;
		} else {
				lastFrameTime = currTime;
		}
		
		transform.Translate(Vector3.up * deltaY);
		deltaY += lift;
		
		float rotAngle = -expansionAngle;
		float rotCos = Mathf.Cos(rotAngle);
		float rotSin = Mathf.Sin(rotAngle);
		
		for (int i = 0; i < levels; i++) {
			profile[i] = new Vector3(
						(rotCos * profile[i].x) - (rotSin * profile[i].y),
						(rotSin * profile[i].x) + (rotCos * profile[i].y),
						0);
		}
		
		
		if (profile[0].x < 0) {
			ShuffleDown(1);
		}
		
		if (frameCount > decayTime) {
			if (levels >= 4) {
				ShuffleDown(1);
			}
		}
		
		if (levels < maxLevels) {
			profile[levels] = new Vector3(0, profile[levels - 1]. y + topAddition, 0);
			levels++;
		}
		
		CalcGeom();
		
		frameCount++;
	}
}
