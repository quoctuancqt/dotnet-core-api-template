using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace Wizard
{
    public class RootWizard : IWizard
    {
        public static Dictionary<string, string> GlobalDictionary = new Dictionary<string, string>();

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            throw new System.NotImplementedException();
        }

        public void ProjectFinishedGenerating(Project project)
        {
            throw new System.NotImplementedException();
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            throw new System.NotImplementedException();
        }

        public void RunFinished()
        {
            throw new System.NotImplementedException();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // Place "$saferootprojectname$ in the global dictionary.
            // Copy from $safeprojectname$ passed in my root vstemplate
            GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"];
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
