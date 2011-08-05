using System;
using System.Windows.Media;
using ChameleonCoder.Resources.Interfaces;
using ChameleonCoder.LanguageModules;
using IF = ChameleonCoder.Interaction.InformationProvider;

namespace AHKModule
{
    public abstract class ModuleBase : ILanguageModule
    {
        public virtual string Author { get { return "maul.esel"; } }
        public virtual string Version { get { return "0.01"; } }
        public virtual string About { get { return "Copyright (c) 2011 maul.esel"; } }
        public virtual int APIVersion { get { return 1; } }
        public abstract Guid Identifier { get; }
        public abstract string Name { get; }
        public bool IsBusy { get { return busy; } }
        public abstract ImageSource Icon { get; }
        public abstract string Description { get; }
        
        protected bool busy;

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

        protected IResource AbsoluteResolve(IResource resource)
        {
            IResolvable link;

            while ((link = resource as IResolvable) != null && link.shouldResolve)
                resource = link.Resolve();

            return resource;
        }

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

        internal void MsgBoxCreator_InsertCode()
        {
            IF.InsertCode(MsgBoxCreator.Code);
        }
        #endregion
    }
}
