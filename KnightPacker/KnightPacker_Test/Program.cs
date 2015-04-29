using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace KnightPacker_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("\nBegin WPF UIAutomation test run\n");
                //Launch Application
                Console.WriteLine("Launching Knight Packer Application");
                Process p = null;
                int ct = 0;
                do
                {
                    Console.WriteLine("Looking for KnightPacker process...");
                    p = Process.Start("C:/Users/jared.ramey/Documents/GitHub/SpritePacker/KnightPacker/KnightPacker/bin/Debug/KnightPacker.exe");
                    ct++;
                    Thread.Sleep(100);
                } while (p == null && ct < 50);

                if (p == null)
                    throw new Exception("Failed to find KnightPacker process");
                else
                    Console.WriteLine("Found KnightPacker process");

                //Next fetch a reference to the host machine's desktop as an AutomationElement object

                Console.WriteLine("\nGetting Desktop");
                AutomationElement aeDesktop = null;
                aeDesktop = AutomationElement.RootElement;
                if (aeDesktop == null)
                    throw new Exception("Unable to get Desktop");
                else
                    Console.WriteLine("Found Desktop\n");
                
                //Get Reference to main Window Control

                AutomationElement aeKnightPacker = null;
                int numWaits = 0;
                Console.WriteLine("Looking for KnightPacker main window...");
                do
                { 
                    aeKnightPacker = aeDesktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Knight Packer"));
                    numWaits++;
                    Thread.Sleep(200);
                } while (aeKnightPacker == null && numWaits < 50);
                if (aeKnightPacker == null)
                    throw new Exception("Failed to Find KnightPacker main Window");
                else
                    Console.WriteLine("Found KnightPacker main Window");
                //Get Refrences to user controls

                Console.WriteLine("\nGetting all user controls");
                int count = 0;
                AutomationElement aeButton = null;
                do{
                    aeButton = aeKnightPacker.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Browser"));
                    count++;
                    Thread.Sleep(200);
                } while (aeButton == null && count < 50);
                if (aeButton == null)
                    throw new Exception("No HomePage");
                else
                    Console.WriteLine("Got Home Page");
                

                //Manipulate application
                //Check Resulting state and determine pass/ fail
                Console.WriteLine("\nEnd Automation");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Fatal: "+ ex.Message);
            }
        }
    }
}
