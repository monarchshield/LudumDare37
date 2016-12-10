///////////////////////////////////////////////////////////////////////////////
//
//   █████╗ ███████╗████████╗██████╗  ██████╗ ██████╗ ███████╗ █████╗ ██████╗
//  ██╔══██╗██╔════╝╚══██╔══╝██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗██╔══██╗
//  ███████║███████╗   ██║   ██████╔╝██║   ██║██████╔╝█████╗  ███████║██████╔╝
//  ██╔══██║╚════██║   ██║   ██╔══██╗██║   ██║██╔══██╗██╔══╝  ██╔══██║██╔══██╗
//  ██║  ██║███████║   ██║   ██║  ██║╚██████╔╝██████╔╝███████╗██║  ██║██║  ██║
//  ╚═╝  ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝
//
//                ██████╗  █████╗ ███╗   ███╗███████╗███████╗
//               ██╔════╝ ██╔══██╗████╗ ████║██╔════╝██╔════╝
//               ██║  ███╗███████║██╔████╔██║█████╗  ███████╗
//               ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ╚════██║
//               ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗███████║
//                ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝╚══════╝
//
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

namespace AstroBearGames.Helpers
{
	[AddComponentMenu("AstroBear Games/Helpers/Delayed Destroyer")]
	public class DelayedDestroyer : MonoBehaviour
	{
		// >>==================================================
		// Variables
		// >>==================================================
		public float m_Delay = 13.0F;

		public bool m_ShouldFlash = false;
		public float m_FlashTime = 5.0F;

		[Header("Particles")]
		public GameObject m_FinishParticle = null;
		public bool m_UseRotation = false;
		private bool m_UseFinishParticle = true;

		private float m_Timer = 0.0F;
		private bool m_IsFlashing = false;
		private float m_FlashTimer = 0.2F;

		// >>==================================================
		// Unity Functions
		// >>==================================================
		void Update()
		{
			m_Timer += Time.deltaTime;

			if (m_IsFlashing)
			{
				m_FlashTimer -= Time.deltaTime;

				if (m_FlashTimer <= 0.0F)
				{
					m_FlashTimer = 0.2F;
					Renderer render = GetComponent<Renderer>();
					if (render != null) { render.enabled = !render.enabled; }
				}
			}
			else
			{
				if (m_ShouldFlash)
				{
					if ((m_Delay - m_Timer) <= m_FlashTime)
					{
						m_IsFlashing = true;
					}
				}
			}

			if (m_Timer >= m_Delay)
			{
				if (m_FinishParticle != null)
				{
					if (m_UseFinishParticle)
					{
						Instantiate(m_FinishParticle, transform.position, m_UseRotation ? transform.rotation * m_FinishParticle.transform.rotation : m_FinishParticle.transform.rotation);
					}
				}
				Destroy(gameObject);
			}
		}

		// >>==================================================
		// Custom Functions
		// >>==================================================
		public void Cancel()
		{
			// Reenable Renderer
			Renderer render = GetComponent<Renderer>();
			render.enabled = true;

			// Kill the Script
			Destroy(this);
		}

		// >>==================================================
		public void SetDelay(float a_Delay)
		{
			m_Delay = a_Delay;
		}

		// >>==================================================
		public void SetShouldFlash(bool a_ShouldFlash)
		{
			m_ShouldFlash = a_ShouldFlash;
		}

		public bool UseFinishParticle { get { return m_UseFinishParticle; } set { m_UseFinishParticle = value; } }
	}
}

///////////////////////////////////////////////////////////////////////////////
//              _______  __  __  ___     __      __          ___ __
//          /\ /__`||__)/  \|__)|__  /\ |__)    / _` /\ |\/||__ /__`
//         /--\.__/||  \\__/|__)|___/--\|  \    \__>/--\|  ||___.__/
//
///////////////////////////////////////////////////////////////////////////////
