using UnityEngine;
using System.Collections;

public class ArcBall : MonoBehaviour
{

	
	/*We initialize Arcball's width height center and radius of screen coords and radius for Transalting target*/
	int arcWidth;            
	int arcHeight;            
	float arcRadius;          
	float arcRadiusTranslation;
	Vector2 arcCenter;

	/* We initialize matrices for Arcball's postions and orientation*/
	Matrix4x4 arcRotation;        
	Matrix4x4 arcPosition;      
	Matrix4x4 arcPositionDelta; 
	
	/* Define quaternions for up, down and initialize dragging */
	Quaternion qDown;        
	Quaternion qNow;         
	bool mDrag;              
	/*Define mouse points last, starting and current of rotation arc*/
	Vector2 m_ptLastMouse;   
	Vector3 arcDownPt;         
	Vector3 arcCurrentPt;    

	

	void SetWindow(int nWidth, int nHeight)
	{

		float fRadius = 0.9f;

		arcWidth = nWidth;
		arcHeight = nHeight;
		arcRadius = fRadius;
		arcCenter = new Vector2(arcWidth / 2.0f, arcHeight / 2.0f);
	}

	void Reset()
	{

		mDrag = false;
		arcRadiusTranslation = 1.0f;
		qNow = Quaternion.identity;
		qDown = Quaternion.identity; ;
		arcRotation = Matrix4x4.identity;
		arcPosition = Matrix4x4.identity;
		arcPositionDelta = Matrix4x4.identity;
		arcRadius = 1.0f;

	}
	Vector3 ScreenToVector(float fScreenPtX, float fScreenPtY)
	{

		// Scale to screen
		float x = (fScreenPtX - arcWidth / 2) / (arcRadius * arcWidth / 2);
		float y = (fScreenPtY - arcHeight / 2) / (arcRadius * arcHeight / 2);

		float z = 0.0f;
		float mag = x * x + y * y;

		if (mag > 1.0)
		{
			float scale = 1.0f / Mathf.Sqrt(mag);
			x *= scale;
			y *= scale;
		}
		else
			z = Mathf.Sqrt(1.0f - mag);

		// Return vector
		return new Vector3(x, y, z);

	}

	Quaternion QuatFromBallPoints(Vector3 vFrom, Vector3 vTo)
	{
		float fDot = Vector3.Dot(vFrom, vTo);
		Vector3 vPart = Vector3.Cross(vFrom, vTo);

		return new Quaternion(vPart.x, vPart.y, vPart.z, fDot);
	}

	void OnBegin(int nX, int nY)
	{
		mDrag = true;
		arcDownPt = ScreenToVector(nX, nY);
	}

	void OnMove(int nX, int nY)
	{
		if (mDrag)
		{
			arcCurrentPt = ScreenToVector(nX, nY);
			qNow = qDown * QuatFromBallPoints(arcDownPt, arcCurrentPt);
		}
	}

	void OnEnd()
	{
		mDrag = false;
		qDown = qNow;
	}

	// Use this for initialization
	void Start()
	{
		Reset();
		arcDownPt = new Vector3(0, 0, 0);
		arcCurrentPt = new Vector3(0, 0, 0);

		SetWindow(Screen.width, Screen.height);
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse Down:" + Time.frameCount);
			OnBegin((int)Input.mousePosition.x, (int)Input.mousePosition.y);
		}

		OnMove((int)Input.mousePosition.x, (int)Input.mousePosition.y);

		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Mouse Up:" + Time.frameCount);
			OnEnd();
		}

		transform.rotation = Quaternion.Inverse(qNow);

	}
}