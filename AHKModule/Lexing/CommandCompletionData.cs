using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;


namespace AHKModule.Lexing
{
    class CommandCompletionData : ICompletionData
    {
        internal CommandCompletionData(string text, string description)
        {
            this._text = text;
            this._description = description;
        }


        readonly string _description;

        readonly string _text;


        object ICompletionData.Content { get { return _text; } }

        object ICompletionData.Description { get { return _description; } }

        ImageSource ICompletionData.Image { get { return null; } } // todo: add image

        double ICompletionData.Priority { get { return 0; } }

        string ICompletionData.Text { get { return _text; } }


        void ICompletionData.Complete(TextArea textArea, ISegment segment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(segment, (this as ICompletionData).Text);
        }
    }
}
