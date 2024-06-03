using RMC.AI_Sample.Scenes;
using RMC.AI_Sample.View.Base;
using RMC.Core.UI;
using RMC.Core.UI.UnityUI;
using UnityEngine;

#pragma warning disable CS1998
namespace RMC.AI_Sample.View
{
    /// <summary>
    /// UI for <see cref="Example01_ChatCompletion"/>
    /// </summary>
    public class Example01_ChatCompletionView : Scene_BaseView
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public TextFieldUI HeaderTextFieldUI { get { return _headerTextFieldUI;} }
        
        public TextInputFieldPanelUI InputPanelUI { get { return _inputPanelUI;} }
        
        public TextAreaPanelUI OutputPanelUI { get { return _outputPanelUI;} }
        public ButtonUI SubmitButtonUI { get { return _submitButtonUI;} }
        public ButtonUI ClearButtonUI { get { return _clearButtonUI;} }

        //  Fields ----------------------------------------
        [Header("Child")]
        [SerializeField]
        private TextFieldUI _headerTextFieldUI;

        [SerializeField]
        private TextInputFieldPanelUI _inputPanelUI;

        [SerializeField]
        private TextAreaPanelUI _outputPanelUI;

        [SerializeField]
        private ButtonUI _submitButtonUI;

        [SerializeField]
        private ButtonUI _clearButtonUI;

        //  Unity Methods  --------------------------------
        protected override async void Awake()
        {
            base.Awake();
        }
        
        
        protected override async void Start()
        {
            base.Start();
        }

        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}