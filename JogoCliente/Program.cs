using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        UdpClient cliente = new UdpClient();
        IPEndPoint servidorEP = new IPEndPoint(IPAddress.Loopback, 5000);

        Console.Write("Digite seu nome: ");
        string nome = Console.ReadLine();

        Enviar(cliente, servidorEP, $"ENTRAR:{nome}");

        bool jogando = true;
        while (jogando)
        {
            IPEndPoint remotoEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] dados = cliente.Receive(ref remotoEP);
            string resposta = Encoding.UTF8.GetString(dados);
            Console.WriteLine($"Servidor: {resposta}");

            if (resposta.StartsWith("RESULTADO:"))
            {
                string[] partes = resposta.Split(':');
                string status = partes[1];
                int pontos = int.Parse(partes[2]);

                if (status == "ganhou")
                {
                    int falta = 21 - pontos;
                    Console.WriteLine($"Você ganhou com {pontos} pontos!");
                    Console.WriteLine($"Faltaram {falta} pontos para fazer 21.");
                }
                else
                {
                    int passou = pontos - 21;
                    Console.WriteLine($"Você perdeu com {pontos} pontos.");
                    Console.WriteLine($"Você passou de 21 por {passou} pontos.");
                }

                jogando = false;
            }
            else if (resposta.StartsWith("CARTA"))
            {
                Console.Write("Deseja pedir outra carta? (s/n): ");
                string escolha = Console.ReadLine();
                if (escolha.ToLower() == "s")
                {
                    Enviar(cliente, servidorEP, "PEDIR_CARTA");
                }
                else
                {
                    Enviar(cliente, servidorEP, "PARAR");
                }
            }
        }

        Console.WriteLine("Fim do jogo.");
    }

    static void Enviar(UdpClient cliente, IPEndPoint destino, string msg)
    {
        byte[] dados = Encoding.UTF8.GetBytes(msg);
        cliente.Send(dados, dados.Length, destino);
    }
}
