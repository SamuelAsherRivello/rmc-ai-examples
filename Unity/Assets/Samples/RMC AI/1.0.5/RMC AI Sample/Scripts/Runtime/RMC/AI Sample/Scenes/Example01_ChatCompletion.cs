using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using RMC.AI;
using RMC.AI_Sample.Helpers;
using RMC.AI_Sample.View;
using UnityEngine;

#pragma warning disable CS4014, CS1998
namespace RMC.AI_Sample.Scenes
{
    /// <summary>
    /// Controller for <see cref="Example01_ChatCompletionView"/>
    /// </summary>
    public class Example01_ChatCompletion : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------

        //  Fields ----------------------------------------
        [SerializeField] 
        private Example01_ChatCompletionView _view;
        private ArtificialIntelligence _artificialIntelligence;

        
        //  Unity Methods  --------------------------------
        protected async void Start()
        {
            // Header
            _view.HeaderTextFieldUI.IsVisible = true;
            
            // Body
            _view.InputPanelUI.IsVisible = true;
            _view.OutputPanelUI.IsVisible = true;

            _view.InputPanelUI.BodyTextInputFieldUI.InputField.text =
                $"Tell me a joke with less than 30 words.\n" +
                $"(Cuéntame un chiste con menos de 30 palabras)";
            
            // Footer
            _view.SubmitButtonUI.IsVisible = true;
            _view.ClearButtonUI.IsVisible = true;
            
            _view.SubmitButtonUI.Text.text = "Submit";
            _view.ClearButtonUI.Text.text = "Clear";

            _view.SubmitButtonUI.Button.onClick.AddListener(() => 
                SubmitButtonUI_OnClicked());
            _view.ClearButtonUI.Button.onClick.AddListener(() => 
                ClearButtonUI_OnClicked());
            _view.InputPanelUI.BodyTextInputFieldUI.InputField.onValueChanged.AddListener( (message) => 
                InputField_OnValueChanged());
            _view.InputPanelUI.BodyTextInputFieldUI.InputField.onSubmit.AddListener((message) =>
                SubmitButtonUI_OnClicked());
            
            // Create AI
            _artificialIntelligence = new ArtificialIntelligence();
            
            // Refresh before and after authentication
            await RefreshUIAsync();
            await _artificialIntelligence.InitializeAsync();
            await _artificialIntelligence.AuthenticateAsync("Sample/ArtificialIntelligenceData");
            await RefreshUIAsync();
        }




        //  Methods ---------------------------------------
        private async UniTask RefreshUIAsync()
        {
            _view.SubmitButtonUI.IsInteractable = _artificialIntelligence.IsAuthenticated &&
                                                  !string.IsNullOrEmpty(_view.InputPanelUI.BodyTextInputFieldUI
                                                      .InputField.text);
            
            _view.ClearButtonUI.IsInteractable = _artificialIntelligence.IsAuthenticated;

        }
        
        /// <summary>
        /// Passes a prompt. Returns a completion.
        /// </summary>
        private async Task<string> GetCompletionAsync(string inputFieldText)
        {
            var message = new List<Message>
            {
                new Message(Role.Assistant,
                    inputFieldText
                )
            };
            
            var chatRequest = new ChatRequest(message, Model.GPT4_Turbo);
            
            var result = await _artificialIntelligence.OpenAIClient.ChatEndpoint.
                GetCompletionAsync(chatRequest);
            
            return result.FirstChoice;
        }
        
        
        //  Event Handlers --------------------------------
        private async void InputField_OnValueChanged()
        {
            await RefreshUIAsync();
        }
        
        
        private async UniTask SubmitButtonUI_OnClicked()
        {
            AISampleHelper.PlayAudioClipClick01();
            
            AISampleHelper.ShowDialogAsync(_view.DialogSystem, "GetCompletionAsync",
                async () =>
                {

                    string result = await GetCompletionAsync(_view.InputPanelUI.BodyTextInputFieldUI.InputField.text);
                    _view.OutputPanelUI.BodyTextAreaUI.Text.text = result;
                    
                },
                async () =>
                {
                    await RefreshUIAsync();
                });
        }

        private async UniTask ClearButtonUI_OnClicked()
        {
            AISampleHelper.PlayAudioClipClick02();
            
            _view.InputPanelUI.BodyTextInputFieldUI.InputField.text = "";
            _view.OutputPanelUI.BodyTextAreaUI.Text.text = "";
            await RefreshUIAsync();
        }
    }
}

