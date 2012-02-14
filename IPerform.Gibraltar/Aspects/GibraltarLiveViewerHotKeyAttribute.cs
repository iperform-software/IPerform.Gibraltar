using System;
using System.Collections.Generic;
using System.Text;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using System.Windows.Forms;
using Gibraltar.Agent;

namespace IPerform.Gibraltar.Aspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Class,
                              Inheritance = MulticastInheritance.Strict)]
    public class GibraltarLiveViewerHotKeyAttribute : InstanceLevelAspect
    {
        public readonly Keys keys;

        public GibraltarLiveViewerHotKeyAttribute(Keys keys)
        {
            this.keys = keys;
        }

        public GibraltarLiveViewerHotKeyAttribute()
        {
            this.keys = Keys.OemQuestion;
        }

        [IntroduceMember(Visibility = Visibility.Family, IsVirtual = true,
                    OverrideAction = MemberOverrideAction.OverrideOrFail)]
        public bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == keys)
            {
                Log.ShowLiveViewer();

                return true;
            }

            return false;
        //    return base.ProcessCmdKey(ref msg, keyData); // FIXME :(
        }
    }
}
