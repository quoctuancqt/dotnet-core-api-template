using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wizard
{
    public class ChildWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            throw new NotImplementedException();
        }

        public void ProjectFinishedGenerating(Project project)
        {
            throw new NotImplementedException();
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            throw new NotImplementedException();
        }

        public void RunFinished()
        {
            throw new NotImplementedException();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // Add custom parameters.
            replacementsDictionary.Add("$saferootprojectname$",
                RootWizard.GlobalDictionary["$saferootprojectname$"]);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
