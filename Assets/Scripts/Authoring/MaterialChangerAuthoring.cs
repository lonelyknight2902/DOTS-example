// using System.Collections;
// using System.Collections.Generic;
// using Component;
// using Unity.Entities;
// using Unity.Rendering;
// using UnityEngine;
//
//
// [DisallowMultipleComponent]
// public class MaterialChangerAuthoring : MonoBehaviour
// {
//     public Material material0;
//     public Material material1;
//     [RegisterBinding(typeof(MaterialChanger), "frequency")]
//     public uint frequency;
//     [RegisterBinding(typeof(MaterialChanger), "frame")]
//     public uint frame;
//     [RegisterBinding(typeof(MaterialChanger), "active")]
//     public uint active;
//
//     class MaterialChangerBaker : Baker<MaterialChangerAuthoring>
//     {
//         public override void Bake(MaterialChangerAuthoring authoring)
//         {
//             // var component = new MaterialChanger();
//             // component.material0 = GetEntity(authoring.material0);
//             // component.material1 = authoring.material1;
//             // component.frequency = authoring.frequency;
//             // component.frame = authoring.frame;
//             // component.active = authoring.active;
//             var entity = GetEntity(TransformUsageFlags.Dynamic);
//             // AddComponentObject(entity, component);
//             AddComponent(entity,new MaterialChanger
//             {
//                 material0 = GetEntity(authoring.material0, TransformUsageFlags.Dynamic),
//                 material1 = GetEntity(authoring.material1, TransformUsageFlags.Dynamic),
//                 frequency = authoring.frequency,
//                 frame = authoring.frame,
//                 active = authoring.active,
//             });
//         }
//     }
// }
