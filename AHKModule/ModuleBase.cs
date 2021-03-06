﻿using System;
using System.Windows.Media;
using ChameleonCoder.Plugins;
using ChameleonCoder.Resources;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using IF = ChameleonCoder.Shared.InformationProvider;

namespace AhkModule
{
    public abstract class ModuleBase : ILanguageModule
    {
        public virtual string Author { get { return "maul.esel"; } }

        public virtual string About { get { return "© 2011 maul.esel"; } }

        public abstract string Description { get; }

        public virtual IHighlightingDefinition Highlighting { get { return null; } }

        public abstract ImageSource Icon { get; }

        public abstract Guid Identifier { get; }

        public bool IsBusy
        {
            get { return busy; }
            protected set { busy = value; }
        }

        public abstract string Name { get; }

        public virtual string Version { get { return "0.01"; } }
        
        
        public virtual void Initialize(ChameleonCoder.IChameleonCoderApp app)
        {
            App = app;

            App.ResourceMan.ResourceLoaded += AddFolding;
            App.ResourceMan.ResourceUnload += RemoveFolding;
        }

        public ChameleonCoder.IChameleonCoderApp App
        {
            get;
            private set;
        }

        public virtual void Shutdown()
        {
            App = null;

            App.ResourceMan.ResourceLoaded -= AddFolding;
            App.ResourceMan.ResourceUnload -= RemoveFolding;
        }

        public virtual void Load()
        {
            /*
            IF.RegisterCodeGenerator(this.LoadMsgBoxCreator, null, "MsgBoxCreator"); // add delegate and icon
            IF.RegisterCodeGenerator(delegate(IResource sender, CodeGeneratorEventArgs e)
            {
                (new CompileAHK_NET(sender)).ShowDialog();
            }, null, "compile");
            */
        }

        public virtual void Unload() { }

        public virtual void Compile(IResource resource)
        {
            throw new NotImplementedException();
        }

        public virtual void Execute(IResource resource)
        {
            throw new NotImplementedException();
        }

        public virtual bool CanCompile(IResource resource)
        {
            return false; // compilation not yet implemented
        }

        public virtual bool CanExecute(IResource resource)
        {
            return false; // execution not yet implemented
        }

        private bool busy;

        #region MsgBoxCreator
        static MsgBoxCreator MsgBoxCreator;

        public void LoadMsgBoxCreator(IResource sender, CodeGeneratorEventArgs e)
        {
            MsgBoxCreator = new MsgBoxCreator(() =>
                {
                    e.Handled = false;
                    e.Code = MsgBoxCreator.Code;
                },
                () =>
                {
                    e.Handled = true;
                    App.ResourceMan.Add(sender, null);
                    //IF.AddResource(null, sender);
                    // add new file to resource
                });

            MsgBoxCreator.ShowDialog();
        }
        #endregion        

        #region folding

        private FoldingManager foldingManager;

        private AbstractFoldingStrategy foldingStrategy;

        private void AddFolding(object sender, IResourceEventArgs e)
        {
            if (IF.CurrentPage == ChameleonCoder.Shared.CCTabPage.ResourceEdit
                && App.PluginMan.GetModule((sender as ILanguageResource).Language) is ModuleBase)
            {
                /*
                foldingManager = FoldingManager.Install(IF.GetEditor().TextArea);
                foldingStrategy = new XmlFoldingStrategy();
                foldingStrategy.UpdateFoldings(foldingManager, IF.GetEditor().Document);

                IF.GetEditor().TextChanged += UpdateFolding;
                */
            }
        }

        private void RemoveFolding(object sender, IResourceEventArgs e)
        {
            /*
            var editor = IF.GetEditor();
            if (editor != null)
                editor.TextChanged -= UpdateFolding;
            */
        }

        private void UpdateFolding(object sender, EventArgs e)
        {
            foldingStrategy.UpdateFoldings(foldingManager, (sender as ICSharpCode.AvalonEdit.TextEditor).Document);
        }

        #endregion
    }
}
