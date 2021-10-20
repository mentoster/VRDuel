//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Makes the hand act as an input module for Unity's event system
//
//=============================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class InputModule : BaseInputModule
	{
		//------------------------------------------------- my code
		public Camera m_Camera;
		public SteamVR_Input_Sources m_TargetSource;
		public SteamVR_Action_Boolean m_ClickAction;

		private GameObject m_CurrentObject = null;
		private PointerEventData m_Data = null;

		protected override void Awake()
        {
			base.Awake();

			m_Data = new PointerEventData(eventSystem);
        }

		public PointerEventData GetData()
        {
			return m_Data;
        }

		private void ProcessPress(PointerEventData data)
        {
			data.pointerPressRaycast = data.pointerCurrentRaycast;

			GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

			if (newPointerPress = null)
				newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

			data.pressPosition = data.position;
			data.pointerPress = newPointerPress;
			data.rawPointerPress = m_CurrentObject;
        }

		private void ProcessRelease(PointerEventData data)
		{
			ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

			GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

			if(data.pointerPress == pointerUpHandler)
            {
				ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
            }

			eventSystem.SetSelectedGameObject(null);

			data.pressPosition = Vector2.zero;
			data.pointerPress = null;
			data.rawPointerPress = null;
		}

		//------------------------------------------------ end my code
		private GameObject submitObject;

		//-------------------------------------------------
		private static InputModule _instance;
		public static InputModule instance
		{
			get
			{
				if ( _instance == null )
					_instance = GameObject.FindObjectOfType<InputModule>();

				return _instance;
			}
		}


		//-------------------------------------------------
		public override bool ShouldActivateModule()
		{
			if ( !base.ShouldActivateModule() )
				return false;

			return submitObject != null;
		}


		//-------------------------------------------------
		public void HoverBegin( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerEnterHandler );
		}


		//-------------------------------------------------
		public void HoverEnd( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			pointerEventData.selectedObject = null;
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerExitHandler );
		}


		//-------------------------------------------------
		public void Submit( GameObject gameObject )
		{
			submitObject = gameObject;
		}


		//-------------------------------------------------
		public override void Process()
		{
			//-----------------------------------------my code
			m_Data.Reset();
			m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

			eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
			m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
			m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

			m_RaycastResultCache.Clear();

			HandlePointerExitAndEnter(m_Data, m_CurrentObject);

            if (m_ClickAction.GetStateDown(m_TargetSource))
				ProcessPress(m_Data);


			if (m_ClickAction.GetStateUp(m_TargetSource))
				ProcessRelease(m_Data);

			//-----------------------------------------end my code
			if ( submitObject )
			{
				BaseEventData data = GetBaseEventData();
				data.selectedObject = submitObject;
				ExecuteEvents.Execute(submitObject, data, ExecuteEvents.submitHandler);

				submitObject = null;
			}
		}
	}
}
