using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class com : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject target;
    public Button but1;
    public Button but2;
    void Start()
    {
        but1.onClick.AddListener(Combine);
        but2.onClick.AddListener(Combine1);
    }
    void Combine()
    {
        Transform root=transform;
        float startTime = Time.realtimeSinceStartup;
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        List<Transform> boneList = new List<Transform>();
        Transform[] transforms = root.GetComponentsInChildren<Transform>();
        List<Texture2D> textures = new List<Texture2D>();
        List<Material> maters= new List<Material>();
        int width = 0;
        int height = 0;

        int uvCount = 0;

        List<Vector2[]> uvList = new List<Vector2[]>();

        // 遍历所有蒙皮网格渲染器，以计算出所有需要合并的网格、UV、骨骼的信息
        foreach (SkinnedMeshRenderer smr in root.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.name == "F_Base_2_0_Hair")
            {
                maters.AddRange(smr.materials);
            }
            else if(smr.name== "F_Base_2_0_Hair2")
            {
                continue;
            }
            else 
            {
                maters.AddRange(smr.materials);
            }
            for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++)
            {
                CombineInstance ci = new CombineInstance();
                ci.mesh = smr.sharedMesh;
                ci.subMeshIndex = sub;
                combineInstances.Add(ci);
            }
            uvList.Add(smr.sharedMesh.uv);
            uvCount += smr.sharedMesh.uv.Length;
            if (smr.material.mainTexture != null)
            {
                textures.Add(smr.GetComponent<Renderer>().material.mainTexture as Texture2D);
                width += smr.GetComponent<Renderer>().material.mainTexture.width;
                height += smr.GetComponent<Renderer>().material.mainTexture.height;
            }

            foreach (Transform bone in smr.bones)
            {
                foreach (Transform item in transforms)
                {
                    if (item.name != bone.name) continue;
                    boneList.Add(item);
                    break;
                }
            }
            if (smr.name != transform.name)
            {
                Destroy(smr.gameObject);
            }
        }

        // 获取并配置角色所有的SkinnedMeshRenderer
        SkinnedMeshRenderer tempRenderer = root.gameObject.GetComponent<SkinnedMeshRenderer>();
        if (!tempRenderer)
        {
            tempRenderer = root.gameObject.AddComponent<SkinnedMeshRenderer>();
        }

        tempRenderer.sharedMesh = new Mesh();

        // 合并网格，刷新骨骼，附加材质
        tempRenderer.sharedMesh.CombineMeshes(combineInstances.ToArray(), false, false);
        tempRenderer.bones = boneList.ToArray();
        // tempRenderer.material = material;
        tempRenderer.materials = maters.ToArray();
  
        Debug.Log("合并耗时 : " + (Time.realtimeSinceStartup - startTime) * 1000 + " ms");
    }
    void Combine1()
    {
        Transform root = transform;
        float startTime = Time.realtimeSinceStartup;
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        List<Transform> boneList = new List<Transform>();
        Transform[] transforms = root.GetComponentsInChildren<Transform>();
        List<Texture2D> textures = new List<Texture2D>();
        List<Material> maters = new List<Material>();
        int width = 0;
        int height = 0;

        int uvCount = 0;

        List<Vector2[]> uvList = new List<Vector2[]>();

        // 遍历所有蒙皮网格渲染器，以计算出所有需要合并的网格、UV、骨骼的信息
        foreach (SkinnedMeshRenderer smr in root.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.name == "F_Base_2_0_Hair")
            {
                continue;
            }
            else if (smr.name == "F_Base_2_0_Hair2")
            {
                maters.AddRange(smr.materials);
            }
            else
            {
                maters.AddRange(smr.materials);
            }
            for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++)
            {
                CombineInstance ci = new CombineInstance();
                ci.mesh = smr.sharedMesh;
                ci.subMeshIndex = sub;
                combineInstances.Add(ci);
            }
            uvList.Add(smr.sharedMesh.uv);
            uvCount += smr.sharedMesh.uv.Length;
            if (smr.material.mainTexture != null)
            {
                textures.Add(smr.GetComponent<Renderer>().material.mainTexture as Texture2D);
                width += smr.GetComponent<Renderer>().material.mainTexture.width;
                height += smr.GetComponent<Renderer>().material.mainTexture.height;
            }

            foreach (Transform bone in smr.bones)
            {
                foreach (Transform item in transforms)
                {
                    if (item.name != bone.name) continue;
                    boneList.Add(item);
                    break;
                }
            }
            if (smr.name != transform.name)
            {
                Destroy(smr.gameObject);
            }
        }

        // 获取并配置角色所有的SkinnedMeshRenderer
        SkinnedMeshRenderer tempRenderer = root.gameObject.GetComponent<SkinnedMeshRenderer>();
        if (!tempRenderer)
        {
            tempRenderer = root.gameObject.AddComponent<SkinnedMeshRenderer>();
        }

        tempRenderer.sharedMesh = new Mesh();

        // 合并网格，刷新骨骼，附加材质
        tempRenderer.sharedMesh.CombineMeshes(combineInstances.ToArray(), false, false);
        tempRenderer.bones = boneList.ToArray();
        // tempRenderer.material = material;
        tempRenderer.materials = maters.ToArray();

        Debug.Log("合并耗时 : " + (Time.realtimeSinceStartup - startTime) * 1000 + " ms");
    }
}
