using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;




public class UseJson : MonoBehaviour
{
    
    [SerializeField] public int _i;
    [SerializeField] public float _f;
    [SerializeField] public bool _b;
    [SerializeField] public Vector3 _v;
    [SerializeField] public string _str;
    [SerializeField] public int[] _arr;
    [SerializeField] public List<string> _list;
    //GameObject는 Json직렬화 하면 안된다.
    //write read가 안 되는 것은 아니지만 가지고 있는 데이터가 의미없어지는 경우가 생긴다?

    private void Start()
    {     
        //Debug.Log(jsonData);   
    }


	private void Update()
	{
        if (Input.GetKeyDown("w"))
        {
            _arr = new int[3]; _arr[0] = 2; _arr[1] = 3; _arr[2] = 5;
			JTestClass jtc = new JTestClass(_i, _f, _b, _v, _str, _arr, _list);
			string jsonData = ObjectToJson(jtc);
            Debug.Log("json 직렬화 수행");
			CreateJsonFile(Application.persistentDataPath, "JTestClass", jsonData);
		}
        if (Input.GetKeyDown("r"))
        {
			Debug.Log("json 직렬화 파일 해제");
			var openedJtc = LoadJsonFile<JTestClass>(Application.persistentDataPath, "JTestClass");
			openedJtc.Print();
		}
	}
	string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
}

[System.Serializable]
public class JTestClass {
    public int i;
    public float f;
    public Vector3 v;
    public bool b;
    public string str;
    public int[] arr;
    public List<string> list = new List<string>();

    public JTestClass() { }

    public JTestClass(int _i, float _f, bool _b, Vector3 _v, string _str, int[] _arr, List<string> _list)
    {
        i = _i;
        f = _f;
        b = _b;
        v = _v;
        str = _str;
        arr = _arr;
        list = _list;
    }


 
    public void Print()
    {
        Debug.Log($"i : {i}\nf : {f}\nb : {b}\nv : {v}str : {str}\narr[2] : {arr[2]}\nlist[3] : {list[3]}\n");
    }
}
