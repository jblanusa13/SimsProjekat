using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Interactivity;

namespace ProjectSims.Commands
{
    public class PreviewKeyDownWithArgsBehaviour : Behavior<UIElement>
    {
        public ICommand PreviewKeyDownCommand
        {
            get { return (ICommand)GetValue(PreviewKeyDownCommandProperty); }
            set { SetValue(PreviewKeyDownCommandProperty, value); }
        }

        public static readonly DependencyProperty PreviewKeyDownCommandProperty =
            DependencyProperty.Register("PreviewKeyDownCommand", typeof(ICommand), typeof(PreviewKeyDownWithArgsBehaviour), new UIPropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += new KeyEventHandler(AssociatedObjectPreviewKeyDown);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= new KeyEventHandler(AssociatedObjectPreviewKeyDown);
            base.OnDetaching();
        }

        private void AssociatedObjectPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (PreviewKeyDownCommand != null)
            {
                PreviewKeyDownCommand.Execute(e.Key);
            }
        }
    }
}
