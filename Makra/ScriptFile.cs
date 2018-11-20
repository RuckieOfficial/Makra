using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Makra {
    class ScriptFile {
        public string FileName;
        public string Content;
        public string ReturnValue;

        public ScriptFile(string filename, string content) {
            FileName = filename;
            Content = content;
        }

        public async void runScript() {
            var state = await CSharpScript.RunAsync<int>(Content);

            ReturnValue = state.ReturnValue.ToString();
        }
    }
}
