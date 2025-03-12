using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

class Program
{
    static void Main()
    {
        string pipeName = @"\\.\pipe\MyPipe";
        NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);

        try
        {
            // Подключаемся к серверу
            pipeClient.Connect();
            Console.WriteLine("Connected to server.");

            using (BinaryWriter writer = new BinaryWriter(pipeClient))
            {
                Random random = new Random();

                while (true)
                {
                    int number = random.Next(1, 101); // Случайное число от 1 до 100
                    writer.Write(number);
                    Console.WriteLine($"Sent {number} to server.");

                    // Пауза в 1 секунду перед отправкой следующего числа
                    Thread.Sleep(1000);
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Server disconnected: " + e.Message);
        }
    }
}
