using System;
using System.Windows.Media;
using ChameleonCoder.Plugins;
using ChameleonCoder.Resources.Interfaces;
using IF = ChameleonCoder.Interaction.InformationProvider;
using ICSharpCode.AvalonEdit.Highlighting;

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
        
        
        public virtual void Initialize()
        {
        }

        public virtual void Shutdown() { }

        public virtual void Load()
        {
            IF.RegisterCodeGenerator(this.LoadMsgBoxCreator, null, "MsgBoxCreator"); // add delegate and icon
            IF.RegisterCodeGenerator(delegate(IResource sender, CodeGeneratorEventArgs e)
            {
                (new CompileAHK_NET(sender as ICompilable)).ShowDialog();
            }, null, "compile");
        }

        public virtual void Unload() { }

        public virtual void Compile(ICompilable resource) 
        {
            
        }

        public virtual void Execute(IExecutable resource)
        {

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
                    IF.AddResource(null, sender);
                    // add new file to resource
                });

            MsgBoxCreator.ShowDialog();
        }
        #endregion        
    }
}
