using UnityEngine;
using Util;

namespace Collectables {
	[CreateAssetMenu(menuName = "Slime/Collectable")]
	public class CollectableAsset : ScriptableObject {
		public AbstractCollectable collectable;

		[Min(1)] public float collectableValue;
		
		
	}
}