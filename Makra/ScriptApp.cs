using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makra {
    class ScriptApp {
        public List<ScriptFile> ScriptFiles = new List<ScriptFile>();

        public void AddScriptFiles(List<string> filenames) {
            foreach (var filename in filenames) {
                string fileContent = File.ReadAllText(filename);
                ScriptFiles.Add(new ScriptFile(filename, fileContent));
            }
        }

        public List<string> RunAll() {
            List<string> returnValues = new List<string>();
            foreach (var scriptFile in ScriptFiles) {
                scriptFile.runScript();
                returnValues.Add(scriptFile.ReturnValue);
            }

            return returnValues;
        }
    }
}
