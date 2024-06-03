using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using OpenAI.Images;
using RMC.AI;
using RMC.AI_Sample.Helpers;
using RMC.AI_Sample.View;
using UnityEngine;

#pragma warning disable CS4014, CS1998
namespace RMC.AI_Sample.Scenes
{
    /// <summary>
    /// Controller for <see cref="Example02_GenerateImageView"/>
    /// </summary>
    public class Example02_GenerateImage : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------

        //  Fields ----------------------------------------
        [SerializeField] 
        private Example02_GenerateImageView _view;
        private ArtificialIntelligence _artificialIntelligence;

        
        //  Unity Methods  --------------------------------
        protected async void Start()
        {
            // Header
            _view.HeaderTextFieldUI.IsVisible = true;
            
            // Body
            _view.InputPanelUI.IsVisible = true;

            _view.InputPanelUI.BodyTextInputFieldUI.InputField.text =
                $"A Greek god riding a horse on the clouds.\n" + 
                $"(Un dios griego montado a caballo sobre las nubes.)";
            
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
        /// Passes a prompt. Returns an image.
        /// </summary>
        private async Task<Texture2D> GenerateImageAsync(string inputFieldText)
        {
            ImageGenerationRequest imageGenerationRequest = new ImageGenerationRequest(
                inputFieldText, 
                null, 
                1, null);

            var results =
                await _artificialIntelligence.OpenAIClient.ImagesEndPoint.GenerateImageAsync(
                    imageGenerationRequest);

            return results.First().Texture;
        }

        
        
        //  Event Handlers --------------------------------
        private async void InputField_OnValueChanged()
        {
            await RefreshUIAsync();
        }
        
        private async UniTask SubmitButtonUI_OnClicked()
        {
            AISampleHelper.PlayAudioClipClick01();
            
            AISampleHelper.ShowDialogAsync(_view.DialogSystem, "GenerateImageAsync",
                async () =>
                {
                    Texture2D texture2D =
                        await GenerateImageAsync(_view.InputPanelUI.BodyTextInputFieldUI.InputField.text);
         
                    _view.OutputRawImage.texture = texture2D;
                    _view.OutputRawImage.SetNativeSize();
                    
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
            _view.OutputRawImage.texture = new Texture2D(10, 10);
            await RefreshUIAsync();
        }
    }
}

