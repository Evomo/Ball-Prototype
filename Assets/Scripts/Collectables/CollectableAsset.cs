using NaughtyAttributes;
using UnityEngine;
using Util;

namespace Collectables {
	[CreateAssetMenu(menuName = "Slime/Collectable")]
	public class CollectableAsset : ScriptableObject {
		public AbstractCollectable collectable;

		[Range(1,10)] public float collectableValue;
		
		
	}
}