﻿using ChrismasStory.Components;
using NewHorizons.Handlers;
using NewHorizons.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ChrismasStory.Components
{
    internal class SolanumAnimationController : MonoBehaviour
    {
        private SolanumAnimController _animator;
        private NomaiConversationStone _conversationStone;
        private NomaiWallText _nomaiText;
        private OWAudioSource _audioSource;
        private SharedStone _sharedStone;

        public static SolanumAnimationController Instance;
        public void Start()
        {
            Instance = this;

            _animator = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/Nomai_ANIM_SkyWatching_Idle").GetComponent<SolanumAnimController>();
            _conversationStone = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/NomaiConversation/Prefab_QM_SolanumRocks/Structure_QM_SolanumRocks/WordStone_You").GetComponent<NomaiConversationStone>();
            _nomaiText = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/QMResponseText").GetComponent<NomaiWallText>();
            _audioSource = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/Audio_Solanum/AudioSource_Symbols").GetComponent<OWAudioSource>();
            _sharedStone = HeldItemHandler.Instance._sharedStone;

            _nomaiText.HideTextOnStart();

			StreamingHandler.SetUpStreaming(_conversationStone.gameObject, null);
		}
        public void WriteWallText()
        {
            Instance._animator.StartWritingMessage();
        }
        public void RevealWallText()
        {
            Instance._nomaiText.Show();
        }
        public void StopWriteWallText()
        {
            Instance._animator.StopWritingMessage(gestureToText: true);
        }
        public void GestureToStone()
        {
            Instance._animator.PlayGestureToWordStones();
        }
        public void RevealStone()
        {
            Instance._conversationStone.Reveal();           
            Instance._audioSource.PlayOneShot(AudioType.SolanumSymbolReveal, 0.5f);
        }
        public void StopSound()
        {
            Instance._audioSource.Stop();
        }
        public void TransformWatchingTarget()
        {
            Instance._animator.StartWatchingPlayer();
            Instance._animator._playerCameraTransform = _sharedStone.transform;
        }
        public void StopWatching()
        {
            Instance._animator.StopWatchingPlayer();
        }
        public void PlayWholeAnimation()
        {
            Invoke("WriteWallText", 1f);
            Invoke("RevealWallText", 5f);
            Invoke("StopWriteWallText", 6f);
            Invoke("GestureToStone", 7f);
            Invoke("RevealStone", 10.5f);
            Invoke("StopSound", 11.4f);
            Invoke("StopWatching", 20f);
        }
        public void SolanumAnimEvent()
        {
            Invoke("TransformWatchingTarget", 2f);
            Invoke("PlayWholeAnimation", 2f);
        }

        

    }
}
