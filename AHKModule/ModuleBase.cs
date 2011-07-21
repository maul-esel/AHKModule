using System;
using System.Windows.Media;
using ChameleonCoder.Resources.Interfaces;
using ChameleonCoder.LanguageModules;

namespace AHKModule
{
    public abstract class ModuleBase : ILanguageModule
    {
        public virtual string Author { get { return "maul.esel"; } }
        public virtual string Version { get { return "0.01"; } }
        public virtual string About { get { return "Copyright (c) 2011 maul.esel"; } }
        public virtual int APIVersion { get { return 1; } }
        public abstract Guid Language { get; }
        public abstract string LanguageName { get; }
        public bool IsBusy { get { return busy; } }
        public abstract ImageSource Icon { get; }
        
        protected ILanguageModuleHost host;
        protected bool busy;

        public virtual void Initialize(ILanguageModuleHost host)
        {
            this.host = host;
        }

        public virtual void Shutdown() { }

        public virtual void Load()
        {
            this.host.RegisterCodeGenerator(this.LoadMsgBoxCreator, null, "MsgBoxCreator"); // add delegate and icon
            this.host.RegisterCodeGenerator(delegate (IResource sender, CodeGeneratorEventArgs e)
            {
                (new CompileAHK_NET(host.GetResource(sender.GUID) as ICompilable)).ShowDialog();
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
            MsgBoxCreator = new MsgBoxCreator(delegate
                {
                    e.Handled = false;
                    e.Code = MsgBoxCreator.Code;
                },
                delegate
                {
                    e.Handled = true;
                    host.AddResource(null, sender.GUID);
                    // add new file to resource
                });

            MsgBoxCreator.ShowDialog();
        }

        internal void MsgBoxCreator_InsertCode()
        {
            this.host.InsertCode(MsgBoxCreator.Code);
        }
        #endregion
    }
}
