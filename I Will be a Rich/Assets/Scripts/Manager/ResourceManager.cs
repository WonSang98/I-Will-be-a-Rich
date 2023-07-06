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
			//Dictionary�� ������ Key���� Path�� ���ؼ� Sprite ��������
			if (_sprites.TryGetValue(path, out Sprite sprite))
				return sprite as T;

			//������ Resources �������� ��θ� ���� ��������, Dictionary�� �߰��Ѵ�.
			Sprite sp = Resources.Load<Sprite>(path);
			_sprites.Add(path, sp);
			return sp as T;
		}

		//Sprite�� �ƴ� ��� -> Resoucres.Load �⺻ �Լ��� Override �� ����.
		//Dictionary�� ���� ������ ã�� �������� ����.
		return Resources.Load<T>(path);
	}

	public GameObject Instantiate(string path, Transform parent = null)
	{
		//Resources �������� ����� �� Prefab�� ��θ� ���� �������� �Լ�
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
		//���� Instantiate �Լ� Override
		//�̸��� ������ �̸��� �����ϰ� �Ѵٴ� ���� �ٸ���.
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
