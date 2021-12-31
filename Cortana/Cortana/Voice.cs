using System;
using System.Speech.Recognition;
using System.Threading;

namespace Cortana
{
    class Voice
    {

        // Indicate whether the asynchronous emulate recognition  
        // operation has completed.  
        static bool completed;

        static Voice()
        {

            // Initialize an instance of the shared recognizer.  
            using (SpeechRecognizer recognizer = new SpeechRecognizer())
            {

                // Create and load a sample grammar.  
                Grammar testGrammar =
                  new Grammar(new GrammarBuilder("testing testing"));
                testGrammar.Name = "Test Grammar";
                recognizer.LoadGrammar(testGrammar);

                // Attach event handlers for recognition events.  
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(
                    SpeechRecognizedHandler);
                recognizer.EmulateRecognizeCompleted +=
                  new EventHandler<EmulateRecognizeCompletedEventArgs>(
                    EmulateRecognizeCompletedHandler);

                completed = false;

                // Start asynchronous emulated recognition.   
                // This matches the grammar and generates a SpeechRecognized event.  
                recognizer.EmulateRecognizeAsync("testing testing");

                // Wait for the asynchronous operation to complete.  
                while (!completed)
                {
                    Thread.Sleep(333);
                }

                completed = false;

                // Start asynchronous emulated recognition.  
                // This does not match the grammar or generate a SpeechRecognized event.  
                recognizer.EmulateRecognizeAsync("testing one two three");

                // Wait for the asynchronous operation to complete.  
                while (!completed)
                {
                    Thread.Sleep(333);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Handle the SpeechRecognized event.  
        static void SpeechRecognizedHandler(
          object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)
            {
                Console.WriteLine("Recognition result = {0}",
                  e.Result.Text ?? "<no text>");
            }
            else
            {
                Console.WriteLine("No recognition result");
            }
        }

        // Handle the SpeechRecognizeCompleted event.  
        static void EmulateRecognizeCompletedHandler(
          object sender, EmulateRecognizeCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                Console.WriteLine("No result generated.");
            }

            // Indicate the asynchronous operation is complete.  
            completed = true;
        }
    }
}