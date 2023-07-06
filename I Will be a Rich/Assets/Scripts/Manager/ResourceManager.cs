using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

    public void Init()
    {
    }

    public T Load<T>(string path) where T : Object
	{
		if (typeof(T) == typeof(Sprite))
		{
			//Dictionary에 있으면 Key값인 Path를 통해서 Sprite 가져오기
			if (_sprites.TryGetValue(path, out Sprite sprite))
				return sprite as T;

			//없으면 Resources 폴더에서 경로를 통해 가져오고, Dictionary에 추가한다.
			Sprite sp = Resources.Load<Sprite>(path);
			_sprites.Add(path, sp);
			return sp as T;
		}

		//Sprite가 아닌 경우 -> Resoucres.Load 기본 함수를 Override 한 것임.
		//Dictionary를 통해 빠르게 찾아 가져오기 위해.
		return Resources.Load<T>(path);
	}

	public GameObject Instantiate(string path, Transform parent = null)
	{
		//Resources 폴더에서 만들어 둔 Prefab을 경로를 통해 가져오는 함수
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if (prefab == null)
		{
			Debug.Log($"Failed to load prefab : {path}");
			return null;
		}

		return Instantiate(prefab, parent);
	}

	public GameObject Instantiate(GameObject prefab, Transform parent = null)
	{
		//기존 Instantiate 함수 Override
		//이름을 프리팹 이름과 동일하게 한다는 점이 다르다.
		GameObject go = Object.Instantiate(prefab, parent);
		go.name = prefab.name;
		return go;
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
