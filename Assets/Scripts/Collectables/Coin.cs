using Character;
using Manager;
using UnityEngine;
using Util;

namespace Collectables {
	[RequireComponent(typeof(ParticleSystem))]
	public class Coin : AbstractCollectable {
		protected override void ApplyCollectable(Slime slime) {
			GameManager.Instance.totalCoins += (int) value;
		}


		public override void Init(CollectableAsset hc, TunnelDirection direction) {
			base.Init(hc, direction);

			ParticleSystem ps = GetComponent<ParticleSystem>();
			ParticleSystem.EmissionModule emissionModule = ps.emission;

			emissionModule.SetBurst(0, new ParticleSystem.Burst(0, _count: (short) hc.collectableValue));
		}
	}
}