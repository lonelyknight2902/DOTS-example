// using System.Collections.Generic;
// using Component;
// using Unity.Entities;
// using Unity.Rendering;
// using UnityEngine;
// using UnityEngine.Rendering;
//
// namespace System
// {
//     [RequireMatchingQueriesForUpdate]
//     public partial struct MaterialChangerSystem : ISystem, ISystemStartStop
//     {
//         private Dictionary<Material, BatchMaterialID> m_MaterialMapping;
//         private void RegisterMaterial(EntitiesGraphicsSystem hybridRenderSystem, Material material)
//         {
//             if (!m_MaterialMapping.ContainsKey(material))
//             {
//                 m_MaterialMapping[material] = hybridRenderSystem.RegisterMaterial(material);
//             }
//         }
//
//         private void UnregisterMaterial(ref SystemState state)
//         {
//             var hybridRenderer = state.World.GetExistingSystemManaged<EntitiesGraphicsSystem>();
//             if (hybridRenderer == null)
//             {
//                 return;
//             }
//
//             foreach (var kv in m_MaterialMapping)
//             {
//                 hybridRenderer.UnregisterMaterial(kv.Value);
//             }
//         }
//         
//         
//
//         public void OnStartRunning(ref SystemState state)
//         {
//             var hybridRenderer = state.World.GetOrCreateSystemManaged<EntitiesGraphicsSystem>();
//             m_MaterialMapping = new Dictionary<Material, BatchMaterialID>();
//         
//             foreach (var changer in SystemAPI.Query<RefRO<MaterialChanger>>())
//             {
//                 RegisterMaterial(hybridRenderer, changer.ValueRO.material0);
//                 RegisterMaterial(hybridRenderer, changer.ValueRO.material1);
//             }
//         }
//
//         public void OnStopRunning(ref SystemState state)
//         {
//             
//         }
//
//         public void OnUpdate(ref SystemState state)
//         {
//             
//             foreach (var (changer, mmi) in SystemAPI.Query<RefRW<MaterialChanger>, RefRW<MaterialMeshInfo>>())
//             {
//                 changer.ValueRW.frame = changer.ValueRO.frame + 1;
//                 if (changer.ValueRO.frame >= changer.ValueRO.frequency)
//                 {
//                     changer.ValueRW.frame = 0;
//                     changer.ValueRW.active = changer.ValueRO.active == 0 ? 1u : 0u;
//                     var material = changer.ValueRO.active == 0 ? changer.ValueRO.material0 : changer.ValueRO.material1;
//                     mmi.ValueRW.MaterialID = m_MaterialMapping[material];
//                 }
//             }
//         }
//     }
// }