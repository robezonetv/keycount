using System.Runtime.InteropServices;

namespace keycount
{
    class Program
    {
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            int prev = 0;
            int keyBoardHitCount = 0;
            int mouseHitCount = 0;

            using StreamWriter keyboardFile = new("keyboard.txt", append: true);
            using StreamWriter mouseFile = new("mouse.txt", append: true);

            while (true) { 
                Thread.Sleep(5);
                for (int i = 0; i < 600; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 32769)
                    {
                        // Pri zmacknuti ALT se zobrazuje i hodnota 18
                        // Pri zmacknuti CTRL se zobrazuje i hodnota 17
                        // Pri zmacknuti SHIFT se zobrazuje i hodnota 16
                        if (i != 18 && i != 17 && i != 16)
                        {
                            
                            // Porovnani jestli mezitim byla stisknuta jina klavesa
                            if (prev != i && i > 6)
                            {
                                keyBoardHitCount++;
                                keyboardFile.WriteLine(keyBoardHitCount);
                                keyboardFile.Flush();
                                Console.WriteLine(i + " - keystate - " + keyState);
                                prev = 0;
                            }

                            // Klepnuti mysi
                            if (i == 1 || i == 2 || i == 4 || i == 5 || i == 6)
                            {
                                mouseHitCount++;
                                mouseFile.WriteLine(mouseHitCount);
                                mouseFile.Flush();
                                Console.WriteLine((char)i + " - mouse - " + keyState);
                            }

                            // CTRL, ALT, SHIFT klavesy a kontrola na jejich drzeni (zapocita se pouze jeden stisk)
                            if (i == 164 || i == 162 || i == 160)
                            {
                                prev = i;
                            }
                        }
                    }
                }
            }
        }
    }
}
