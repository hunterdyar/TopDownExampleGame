using System;
using Agents;
using UnityEngine;

namespace Input
{
	[RequireComponent(typeof(AgentMovement))]
	[RequireComponent(typeof(PlayerWeaponHandler))]
	public class PlayerInput : MonoBehaviour
	{
		private AgentMovement _agent;
		private PlayerWeaponHandler _playerWeapon;
		private Camera _camera;
		private void Awake()
		{
			_camera = Camera.main;
			_agent = GetComponent<AgentMovement>();
			_playerWeapon = GetComponent<PlayerWeaponHandler>();
		}

		void Update()
		{
			//Check for movement
			Vector2 direction = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
			direction.Normalize();

			//Check for rotation
			Vector2 mousePos = (Vector2)_camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			var facingDir = mousePos- (Vector2)transform.position;
			facingDir.Normalize();
			_agent.Move(direction);
			_agent.RotateTo(facingDir);
			
			//fire weapon
			if (UnityEngine.Input.GetMouseButton(0))
			{
				//should use _agent.facingDir, but that isn't being updated after the collision event.
				_playerWeapon.Fire(transform.right);
			}

			if (UnityEngine.Input.GetButtonDown("Dash"))
			{
				_agent.Dash(transform.right);
			}

			if (UnityEngine.Input.GetButtonDown("NextWeapon"))
			{
				_playerWeapon.SwitchToNextWeapon();
			}
		}
	}
}