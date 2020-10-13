using MotionAI.Core.Controller;
using MotionAI.Core.Models.Generated;
using MotionAI.Core.POCO;
using UnityEngine;
using Util;

namespace Character {
	[RequireComponent(typeof(Slime))]
	public class ElmoSlimeController : MotionAIController {
		private ElementalMovement _lastElmo;

		public OnElmoEvent jump, duck, left, right;

		private Slime slime;

		void Start() {
			slime = GetComponent<Slime>();

			jump.AddListener(elmo => ChangeSlimeDirection(TunnelDirections.NORTH));
			duck.AddListener(elmo => ChangeSlimeDirection(TunnelDirections.SOUTH));
			left.AddListener(elmo => ChangeSlimeDirection(TunnelDirections.WEST));
			right.AddListener(elmo => ChangeSlimeDirection(TunnelDirections.EAST));
		}

		private void ChangeSlimeDirection(TunnelDirections s) {
			if (slime.IsGrounded) {
				slime.currGravityDirection = s;
			}
		}

		protected override void HandleMovement(EvoMovement msg) {
			msg.elmos.ForEach(HandleElmo);
		}

		private void HandleElmo(ElementalMovement elementalMovement) {
			if (!elementalMovement.rejected) {
				switch (elementalMovement.typeID) {
					case ElmoEnum.hop_single_up:
						jump.Invoke(elementalMovement);
						break;
					case ElmoEnum.duck_down:
						duck.Invoke(elementalMovement);
						break;
					case ElmoEnum.side_step_left_up:
						left.Invoke(elementalMovement);
						break;
					case ElmoEnum.side_step_right_up:
						right.Invoke(elementalMovement);
						break;
				}


				// rescue rejected elmo
				if (_lastElmo != null) {
					if (_lastElmo.rejected && elementalMovement.rejected == false) {
						switch (_lastElmo.typeID) {
							case ElmoEnum.duck_down:
								RecoverElmo(elementalMovement, duck);
								break;
							case ElmoEnum.side_step_left_down:
								RecoverElmo(elementalMovement, right);
								break;
							case ElmoEnum.side_step_right_down:
								RecoverElmo(elementalMovement, left);
								break;
							case ElmoEnum.hop_single_down:
								RecoverElmo(elementalMovement, jump);
								break;
						}
					}
				}
			}

			_lastElmo = elementalMovement;
		}

		private void RecoverElmo(ElementalMovement elementalMovement, OnElmoEvent callback) {
			ElmoEnum lastOpposite = DownOpposite(_lastElmo.typeID);
			if (elementalMovement.typeID == lastOpposite) {
				callback.Invoke(elementalMovement);
			}
		}

		private ElmoEnum DownOpposite(ElmoEnum e) {
			switch (e) {
				case ElmoEnum.duck_down:
					return ElmoEnum.duck_up;
				case ElmoEnum.hop_single_down:
					return ElmoEnum.hop_single_up;
				case ElmoEnum.side_step_left_down:
					return ElmoEnum.side_step_left_up;
				case ElmoEnum.side_step_right_down:
					return ElmoEnum.side_step_right_up;
				default:
					return ElmoEnum.heartUp;
			}
		}
	}
}