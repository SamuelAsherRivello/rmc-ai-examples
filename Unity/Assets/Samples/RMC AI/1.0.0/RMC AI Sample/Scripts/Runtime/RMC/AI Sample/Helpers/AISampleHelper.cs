using System;
using Cysharp.Threading.Tasks;
using RMC.Core.Audio;
using RMC.Core.UI.UnityUI.DialogSystem;

namespace RMC.AI_Sample.Helpers
{
    /// <summary>
    /// Helper methods
    /// </summary>
    public static class AISampleHelper
    {
        /// <summary>
        /// Show "Loading..." And Send Transaction
        /// </summary>
        public static async UniTask ShowDialogAsync(
            DialogSystem dialogSystem, 
            string dialogTitle,
            Func<UniTask> transactionCall,
            Func<UniTask> refreshCall)
        {

             DialogData dialogData = new DialogData(
                 "~ {0} ~\nSending...",
                 "~ {0} ~\nSent...",
                 "~ {0} ~\nWaiting..."
             );
                
            // Decorate text
            string functionName = dialogTitle[0].ToString().ToUpperInvariant() + dialogTitle.Substring(1);
            dialogData.SendingMessage = string.Format(dialogData.SendingMessage, functionName);
            dialogData.SentMessage = string.Format(dialogData.SentMessage, functionName);
            dialogData.AwaitingMessage = string.Format(dialogData.AwaitingMessage, functionName);
            
            await ShowDialogAsync(
                dialogSystem, 
                dialogData,
                transactionCall,
                refreshCall);
        }
        
        /// <summary>
        /// Show "Loading..." And Send Transaction
        /// </summary>
        public static async UniTask ShowDialogAsync(
            DialogSystem dialogSystem, 
            DialogData dialogData,
            Func<UniTask> transactionCall, 
            Func<UniTask> refreshingCall)
        {
            await dialogSystem.ShowDialogAsync(
                 dialogData,
                transactionCall,
                refreshingCall);
        }

        public static void PlayAudioClipClick01()
        {
            AudioManager.Instance.PlayAudioClip("AIClick01");
        }
        
        public static void PlayAudioClipClick02()
        {
            AudioManager.Instance.PlayAudioClip("AIClick02");
        }
    }
}