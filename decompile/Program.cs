using System;
using System.IO;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;
using Mono.Cecil;

namespace decompile
{
    class MainClass
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a file path");
                return 1;
            }

            var filePath = args[0];
            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not Exist");
                return 1;
            }

            var modDef = ModuleDefinition.ReadModule(args[0]);
            foreach (var typeDef in modDef.GetTypes())
            {
                Console.WriteLine(Environment.NewLine + typeDef.Name + Environment.NewLine);
                var builder = new AstBuilder(new DecompilerContext(modDef));
                builder.AddType(typeDef);
                builder.RunTransformations();
                var output = new PlainTextOutput();
                builder.GenerateCode(output);
                Console.Write(output.ToString());
            }

            return 0;
        }
    }
}
