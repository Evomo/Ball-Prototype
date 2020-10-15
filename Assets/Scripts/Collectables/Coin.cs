using Character;
using Manager;
using Util.Pool;

namespace Collectables {
	public class Coin : AbstractCollectable {
		public override void ApplyCollectable() {
			GameManager.Instance.totalCoins += (int) value;
			PoolManager.ReleaseObject(gameObject);
		}
	}
}