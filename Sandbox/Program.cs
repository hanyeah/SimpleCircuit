﻿using SimpleCircuit;
using SimpleCircuit.Components;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new SimpleCircuitParser();
            var ckt = parser.Parse(@"gnd <u> V <u r> X1
X1 <?> R1 <? ?> R <?> X2
X1 <?> R2 <? ?> R <?> X2
- R1.Angle = 45
- R2.Angle = -45");
            SimpleCircuit.Functions.Minimizer.LogInfo = true;
            var doc = ckt.Render();

            using var sw = new StringWriter();
            using (var xml = XmlWriter.Create(sw, new XmlWriterSettings { OmitXmlDeclaration = true }))
                doc.WriteTo(xml);
            Console.WriteLine(sw.ToString());

            if (File.Exists("tmp.html"))
                File.Delete("tmp.html");
            using (var fw = new StreamWriter(File.OpenWrite("tmp.html")))
            {
                fw.WriteLine("<html>");
                fw.WriteLine("<head>");
                fw.WriteLine("</head>");
                fw.WriteLine("<body>");
                fw.WriteLine(sw.ToString());
                fw.WriteLine("</body>");
                fw.WriteLine("</html>");
            }
            Process.Start(@"""C:\Program Files (x86)\Google\Chrome\Application\chrome.exe""", "\"" + Path.Combine(Directory.GetCurrentDirectory(), "tmp.html") + "\"");
        }
    }
}
