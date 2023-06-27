// using System;
// using System.Collections.Generic;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Jobs;
// using Unity.Mathematics;
// using Unity.Rendering;
// using Unity.Transforms;
// using UnityEngine;
// using UnityEngine.Rendering;
//
// namespace Authoring
// {
//     public class AddComponentExample : MonoBehaviour
//     {
//         public Mesh mesh;
//         public Material m_material;
//         public bool m_differentMaterial = false;
//
//         public int m_w = 30;
//         public int m_h = 30;
//
//         public struct SpawnJob : IJobParallelFor
//         {
//             public Entity Prototype;
//             public int w;
//             public int h;
//             public bool singleMat;
//             public EntityCommandBuffer.ParallelWriter ecb;
//
//             public void Execute(int index)
//             {
//                 var e = ecb.Instantiate(index, Prototype);
//
//                 int matIndex = singleMat ? 0 : index;
//                 ecb.SetComponent(index, e, MaterialMeshInfo.FromRenderMeshArrayIndices(matIndex, 0));
//                 ecb.SetComponent(index, e, new LocalToWorld
//                 {
//                     Value = ComputeTransform(index),
//                 });
//             }
//
//             public float4x4 ComputeTransform(int index)
//             {
//                 int y = index / w;
//                 int x = index % h;
//                 float3 pos = new float3(x - (float)w * 0.5f, 0, y - (float)h * 0.5f);
//
//                 return float4x4.Translate(pos);
//             }
//         }
//
//         private void Start()
//         {
//             var world = World.DefaultGameObjectInjectionWorld;
//             var entityManager = world.EntityManager;
//
//             EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
//             
//              int objCount = m_w * m_h;
//             var matList = new List<Material>();
//             if ( m_differentMaterial )
//             {
//                 for (int i=0;i<objCount;i++)
//                 {
//                     var mat = new Material(m_material);
//                     Color col = Color.HSVToRGB(((float)(i * 10) / (float)objCount) % 1.0f, 0.7f, 1.0f);
//                     //                Color col = Color.HSVToRGB(Random.Range(0.0f,1.0f), 1.0f, 1.0f);
//                     mat.SetColor("_Color", col);              // set for LW
//                     mat.SetColor("_BaseColor", col);          // set for HD
//                     matList.Add(mat);
//                 }
//             }
//             else
//             {
//                 matList.Add(m_material);
//             }
//
//             // Create a RenderMeshDescription using the convenience constructor
//             // with named parameters.
//             var desc = new RenderMeshDescription(
//                 shadowCastingMode: ShadowCastingMode.Off,
//                 receiveShadows: false);
//
//             var renderMeshArray = new RenderMeshArray(matList.ToArray(), new[] { mesh });
//
//             // Create empty base entity
//             var prototype = entityManager.CreateEntity();
//
//             // Call AddComponents to populate base entity with the components required
//             // by Entities Graphics
//             RenderMeshUtility.AddComponents(
//                 prototype,
//                 entityManager,
//                 desc,
//                 renderMeshArray,
//                 MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));
//             entityManager.AddComponentData(prototype, new LocalToWorld());
//
//             // Spawn most of the entities in a Burst job by cloning a pre-created prototype entity,
//             // which can be either a Prefab or an entity created at run time like in this sample.
//             // This is the fastest and most efficient way to create entities at run time.
//             var spawnJob = new SpawnJob
//             {
//                 Prototype = prototype,
//                 ecb = ecb.AsParallelWriter(),
//                 w = m_w,
//                 h = m_h,
//                 singleMat = !m_differentMaterial
//             };
//
//             var spawnHandle = spawnJob.Schedule(m_h*m_w,128);
//             spawnHandle.Complete();
//
//             ecb.Playback(entityManager);
//             ecb.Dispose();
//             entityManager.DestroyEntity(prototype);
//         }
//     }
// }