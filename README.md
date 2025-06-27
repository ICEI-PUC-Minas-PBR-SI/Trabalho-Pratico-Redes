# Jogo de Cartas “21” com Sockets UDP

Este projeto foi desenvolvido como Trabalho Prático da disciplina de **Redes de Computadores** do curso de Sistemas de Informação da PUC Minas. A proposta foi implementar o jogo de cartas “21” (Blackjack) utilizando sockets UDP para comunicação entre um servidor e múltiplos clientes.

---

## Informações Gerais

- **Curso:** Sistemas de Informação – PUC Minas  
- **Disciplina:** Redes de Computadores  
- **Semestre:** 3º Período  

---

## Participantes

- Hugo Ferreira  
- Ítalo Angelo  
- Izadora Helena  

---

## 📁 Estrutura do Projeto

O projeto está organizado em duas pastas principais:
Blackjack/
├── JogoServidor/ → Código-fonte do servidor UDP
├── JogoCliente/ → Código-fonte do cliente UDP

---

## Sobre os Códigos

### JogoServidor

O servidor é responsável por:
- Receber conexões de jogadores via protocolo UDP
- Sortear cartas de 1 a 10 e controlar a pontuação
- Avaliar se o jogador venceu, perdeu ou estourou 21 pontos
- Enviar mensagens de resultado para cada cliente

As mensagens são todas em formato textual, como `CARTA:7`, `RESULTADO:ganhou`, etc.

### JogoCliente

O cliente:
- Envia o nome do jogador ao servidor (`ENTRAR:<nome>`)
- Recebe cartas e decide se quer pedir mais (`PEDIR_CARTA`) ou parar (`PARAR`)
- Exibe a pontuação e o resultado final da rodada no terminal

---

## Como foi testado localmente

Durante os testes:
- O **servidor** foi executado em um terminal com:
  ```bash
  cd JogoServidor
  dotnet run
- O cliente foi executado em outro terminal com:
  cd JogoCliente
  dotnet run
  
A comunicação foi feita usando localhost (127.0.0.1) e porta 5000.
No código do cliente, a linha responsável pela conexão está assim:

- IPEndPoint servidorEP = new IPEndPoint(IPAddress.Loopback, 5000);





