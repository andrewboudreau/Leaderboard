using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour 
{
	public bool alive = true;
	Vector3 direction = new Vector3(1, 0, 0);
	public System.Action<Bug> Offscreen;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(alive)
		{
			Fly();
		}
		else
		{
			Fall();
		}

		if(this.transform.position.magnitude > 11 && Offscreen != null)
		{
			Offscreen(this);
		}
	}

	public void SetRandomDirection()
	{
		float angle = UnityEngine.Random.Range(0, 360);

		angle = (angle / 360) * 2 * Mathf.PI;

		//x = r * cos(theta),y = r * sin(theta), we want our magnitude to be 1.0 so r isnt needed.
		float x = Mathf.Cos(angle);
		float y = Mathf.Sin(angle);

		direction = new Vector3(x, y, 0);
	}
	
	public void Smash()
	{
		//Disable collider
		BoxCollider collider = this.GetComponent<BoxCollider>();
		collider.enabled = false;

		//Make bug fall off screen
		alive = false;
	}

	private void Fall()
	{
		float speed = 0.2f;
		float currentSpeed = Time.deltaTime * speed;

		Vector3 pos = this.transform.localPosition;
		pos.y -= speed;
		this.transform.localPosition = pos;
	}

	private void Fly()
	{
		float speed = 3.0f;
		float currentSpeed = Time.deltaTime * speed;

		Vector3 toAdd = direction * currentSpeed;

		Vector3 pos = this.transform.localPosition;
		pos += toAdd;
		this.transform.localPosition = pos;
	}

	public static Bug AddRandomBug(GameObject parent)
	{
		//Load up the bug prefab
		string path = "Bug";
		GameObject prefab = (GameObject)Resources.Load(path, typeof(GameObject));
		GameObject obj = (GameObject)GameObject.Instantiate(prefab);
		
		//Set the instantiated object's parent, making sure not to rescale/reposition it.
		Vector3 scale = obj.transform.localScale;
		Vector3 pos = obj.transform.localPosition;
		obj.transform.parent = parent.transform;
		obj.transform.localScale = scale;
		obj.transform.localPosition = pos;
		
		//Grab a handle to our bug script
		Bug bugScript = obj.GetComponent<Bug>();
		bugScript.SetRandomDirection();
		
		return bugScript;
	}
}
